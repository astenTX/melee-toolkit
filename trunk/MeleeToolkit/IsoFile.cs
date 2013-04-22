using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeleeToolkit
{
    static class IsoFile
    {
        private static FileStream _streamFile;
        private const int OffsetID = 0x0;
        private const int OffsetVersion = 0x7;
        private const int OffsetTitle = 0x20;
        private const int OffsetFSTOffset = 0x424;
        private const int OffsetFSTSize = 0x428;

        private static uint FSTOffset;
        private static uint FSTSize;
        private static uint numberOfEntries;
        private static uint stringTableOffset;
        public static TreeNode root;
        public static bool isFileLoaded = false;


        internal static void OpenFile(string path)
        {
            if (_streamFile != null) _streamFile.Close();
            _streamFile = new FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
            InitFST();
            isFileLoaded = true;

        }

        internal static String GetTitle()
        {
            _streamFile.Seek(0x20, SeekOrigin.Begin);
            int buffer = 1;
            string imageTitle = "";
            while (buffer != 0)
            {
                buffer = _streamFile.ReadByte();
                imageTitle += (char)buffer;
            }
            return imageTitle;
        }

        internal static String GetVersion()
        {
            _streamFile.Seek(OffsetVersion, SeekOrigin.Begin);
            string imageVersion = "1.0" + _streamFile.ReadByte();
            return imageVersion;
        }

        internal static String GetID()
        {
            _streamFile.Seek(OffsetID, SeekOrigin.Begin);
            string imageID = "";
            for (int i = 0; i < 6; i++)
            {
                imageID += (char) _streamFile.ReadByte();
            }
            return imageID;
        }

        internal static String GetFilename(uint stringOffset)
        {
            string filename = "";
            if (stringTableOffset == 0) return null;

            long position = _streamFile.Position;

            _streamFile.Seek(stringTableOffset + stringOffset, SeekOrigin.Begin);
            int buffer = 1;
            while (buffer != 0)
            {
                buffer = _streamFile.ReadByte();
                if (buffer != 0) filename += (char)buffer;
            }
            _streamFile.Seek(position, SeekOrigin.Begin);
            return filename;
        }


        internal static void InitFST()
        {
            //Locate FST offset
            _streamFile.Seek(OffsetFSTOffset, SeekOrigin.Begin);
            var buffer = new byte[4];
            _streamFile.Read(buffer, 0, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            FSTOffset = BitConverter.ToUInt32(buffer, 0);

            //Locate FST size (right after the offset, size includes string table)
            _streamFile.Read(buffer, 0, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            FSTSize = BitConverter.ToUInt32(buffer, 0);
            Console.WriteLine("FST Size: " + FSTSize);

            //Locate String Table offset
            _streamFile.Seek(FSTOffset + 0x8, SeekOrigin.Begin);
            _streamFile.Read(buffer, 0, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            numberOfEntries = BitConverter.ToUInt32(buffer, 0);
            Console.WriteLine("Entries:" + numberOfEntries);

            stringTableOffset = FSTOffset + (numberOfEntries * 0xC); //Each entry is 0xC in length, and the string table immediately follows
            Console.WriteLine("String table offset: " + stringTableOffset);

            root = new TreeNode("root");
            root.Tag = new IsoFileInfo("root", 0, 0, FSTOffset, true);
            root.ImageIndex = 4;
            root.SelectedImageIndex = 4;
            BuildNodes(root, stringTableOffset);
        }

        internal static void BuildNodes(TreeNode root, uint endOffset)
        {
            IsoFileInfo rootInfo = (IsoFileInfo) root.Tag;
            uint currentOffset = rootInfo.FSTOffset + 0xC;
            while (currentOffset < endOffset)
            {
                _streamFile.Seek(currentOffset, SeekOrigin.Begin);
                bool isNewNodeFolder = _streamFile.ReadByte() == 1;

                var buffer = new byte[4];
                _streamFile.Read(buffer, 1, 3);
                buffer[0] = 0;
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(buffer);
                uint newNodeStringOffset = BitConverter.ToUInt32(buffer, 0);
                string newNodeName = GetFilename(newNodeStringOffset);

                _streamFile.Read(buffer, 0, 4);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(buffer);
                uint newNodeFileOffset = BitConverter.ToUInt32(buffer, 0);  //If a folder, this is offset to parent

                _streamFile.Read(buffer, 0, 4);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(buffer);
                uint newNodeFileSize = BitConverter.ToUInt32(buffer, 0);    //If a folder, this is the # of files (from FST start) to the next non-child entry

                var newNode = new TreeNode(newNodeName);

                newNode.Tag = new IsoFileInfo(newNodeName, newNodeFileSize, newNodeFileOffset, currentOffset, isNewNodeFolder);
                string fileType = newNodeName.Substring(0, 2);
                switch (fileType)
                {
                    case "Pl":
                        newNode.ImageIndex = 1;
                        newNode.SelectedImageIndex = 1;
                        break;
                    case "Gr":
                        newNode.ImageIndex = 2;
                        newNode.SelectedImageIndex = 2;
                        break;
                    default:
                        if (isNewNodeFolder)
                        {
                            newNode.ImageIndex = 3;
                            newNode.SelectedImageIndex = 3;
                        }
                        break;
                }
                
                root.Nodes.Add(newNode);
                currentOffset += 0xC;

                if (isNewNodeFolder)
                {
                    currentOffset = FSTOffset + (newNodeFileSize*0xC);
                    BuildNodes(newNode, currentOffset);
                }
            }
        }

        internal static void ExportFileFromIso(IsoFileInfo info, SaveFileDialog dialog)
        {
            Stream myStream;
            if ((myStream = dialog.OpenFile()) != null)
            {
                var fileToSave = new byte[info.Size];
                _streamFile.Seek(info.FileOffset, SeekOrigin.Begin);
                _streamFile.Read(fileToSave, 0, (int) info.Size);
                myStream.Write(fileToSave, 0, (int) info.Size);
                myStream.Close();
                
            }
        }

        internal static byte[] OpenDatFile(IsoFileInfo info)
        {
            byte[] outFile = new byte[info.Size];
            _streamFile.Seek(info.FileOffset, SeekOrigin.Begin);
            for (int i = 0; i < info.Size; i++)
            {
                outFile[i] = (byte) _streamFile.ReadByte();
            }
            return outFile;
        }




        internal static IsoFileInfo SearchIso(string filename, TreeNode rootNode)
        {
            foreach (TreeNode currentNode in rootNode.Nodes)
            {
                if (currentNode.Nodes.Count > 0)
                    SearchIso(filename, currentNode);
                else
                {
                    if (currentNode.Text == filename)
                        return (IsoFileInfo) currentNode.Tag;
                }
            }
            return null;
        }

        internal static void ReplaceFileInIso(IsoFileInfo fileToReplace, byte[] newFileData)
        {
            if (fileToReplace.Size != newFileData.Length)
            {
                MessageBox.Show("Error: New file is not the same size!");
                return;
            }
            _streamFile.Seek(fileToReplace.FileOffset, SeekOrigin.Begin);
            _streamFile.Write(newFileData, 0, newFileData.Length);
            MessageBox.Show("File replacement has completed successfully!");

        }

        internal static void ReplaceFileInIso(IsoFileInfo fileToReplace, string pathToNewFile)
        {
            var newFile = new FileStream(pathToNewFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] newFileData = new byte[newFile.Length];
            newFile.Read(newFileData, 0, (int)newFile.Length);
            newFile.Close();
            ReplaceFileInIso(fileToReplace, newFileData);
        }
    }
}
