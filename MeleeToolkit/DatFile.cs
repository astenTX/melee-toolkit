using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using libWiiSharp;

namespace MeleeToolkit
{
    static class DatFile
    {

        static DatHeader fileHeader;
        internal static string fileName;
        internal static byte[] file;
        internal static ListBox textureList;
        internal static TreeNode rootNode;
        internal static UInt32 dataOffset;
        internal static UInt32 relocOffset;
        internal static UInt32 rootOffset0;
        internal static UInt32 rootOffset1;
        internal static UInt32 tableOffset;
        internal static bool isFileLoaded = false;


        public static void OpenDatFile(byte[] inFile, string inFileName, ref ListBox texList)
        {
            rootNode = new TreeNode("Header");
            file = inFile;
            fileName = inFileName;
            InitHeader();
            textureList = texList;
            textureList.Items.Clear();
            BuildRootNodes();
            isFileLoaded = true;
        }

        private static void InitHeader()
        {
            fileHeader = new DatHeader(file, 0);

            rootNode.Tag = fileHeader;

            dataOffset  = 0x20;
            relocOffset = dataOffset + fileHeader.DataBlockSize0x04;
            rootOffset0 = relocOffset + (fileHeader.RelocationTableCount0x08 * 4);
            rootOffset1 = rootOffset0 + (fileHeader.RootCount0x0C * 8);
            tableOffset = rootOffset1 + (fileHeader.RootCount0x10 * 8);
        }

        private static UInt32 ReadUInt32(byte[] value, int startIndex)
        {
            byte[] newValue = new byte[4];
            newValue = BitConverter.GetBytes(BitConverter.ToUInt32(value, startIndex));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(newValue);
            return BitConverter.ToUInt32(newValue, 0);
        }

        private static UInt32 ReadUInt32(UInt32 startIndex)
        {
            byte[] newValue = new byte[4];
            newValue = BitConverter.GetBytes(BitConverter.ToUInt32(file, (int) startIndex));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(newValue);
            return BitConverter.ToUInt32(newValue, 0);
        }

        private static UInt16 ReadUInt16(UInt32 startIndex)
        {
            byte[] newValue = new byte[2];
            newValue = BitConverter.GetBytes(BitConverter.ToUInt16(file, (int)startIndex));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(newValue);
            return BitConverter.ToUInt16(newValue, 0);
        }

        private static float ReadFloat(UInt32 startIndex)
        {
            byte[] newValue = new byte[4];
            newValue = BitConverter.GetBytes(BitConverter.ToUInt32(file, (int)startIndex));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(newValue);
            return BitConverter.ToSingle(newValue, 0);
        }

        private static void BuildRootNodes()
        {
            for (int i = 0; i < fileHeader.RootCount0x0C; i++)
            {
                var newRootNode = new RootNode(file, rootOffset0 + (UInt32)(i*0x8));

                var newTreeNode = new TreeNode(ReadString(tableOffset + newRootNode.StringTableOffset0x4));
                newTreeNode.Tag = newRootNode;
                rootNode.Nodes.Add(newTreeNode);
                if (newTreeNode.Text.IndexOf("joint") != -1 && newTreeNode.Text.IndexOf("matanim") == -1 && newTreeNode.Text.IndexOf("shapeanim") == -1)
                    BuildJointNodes(newTreeNode, newRootNode.RootOffset0x0 + dataOffset);
                else if (newTreeNode.Text.IndexOf("ftData") != -1)
                    BuildFighterDataNodes(newTreeNode, newRootNode.RootOffset0x0 + dataOffset);
            }

            for (int i = 0; i < fileHeader.RootCount0x10; i++)
            {
                var newRootNode = new RootNode(file, rootOffset1 + (UInt32)(i * 0x8));

                var newTreeNode = new TreeNode(ReadString(tableOffset + newRootNode.StringTableOffset0x4));
                newTreeNode.Tag = newRootNode;
                rootNode.Nodes.Add(newTreeNode);
                if (newTreeNode.Text.IndexOf("joint") != -1 && newTreeNode.Text.IndexOf("matanim") == -1 && newTreeNode.Text.IndexOf("shapeanim") == -1)
                    BuildJointNodes(newTreeNode, newRootNode.RootOffset0x0 + dataOffset);
            }
        }

        private static void BuildJointNodes(TreeNode parent, UInt32 offset)
        {
            var newJointNode = new JointNode(file, offset);


            var newTreeNode = new TreeNode("JointNode");
            newTreeNode.Tag = newJointNode;
            parent.Nodes.Add(newTreeNode);

            if (newJointNode.jointDataNodeOffset0x10 != 0)
            {
                BuildJointDataNodes(newTreeNode, newJointNode.jointDataNodeOffset0x10 + dataOffset);
            }

            if (newJointNode.childOffset0x08 != 0)
            {
                BuildJointNodes(newTreeNode, newJointNode.childOffset0x08 + dataOffset);
            }
            if (newJointNode.nextOffset0x0C != 0)
            {
                BuildJointNodes(parent, newJointNode.nextOffset0x0C + dataOffset);
            }

        }

        private static void BuildJointDataNodes(TreeNode parent, UInt32 offset)
        {
            var newJointDataNode = new JointDataNode(file, offset);

            var newTreeNode = new TreeNode("JointDataNode");
            newTreeNode.Tag = newJointDataNode;
            parent.Nodes.Add(newTreeNode);

            if (newJointDataNode.materialNodeOffset0x8 != 0)
            {
                BuildMaterialNodes(newTreeNode, newJointDataNode.materialNodeOffset0x8 + dataOffset);
            }

            if (newJointDataNode.nextOffset0x4 != 0)
            {
                BuildJointDataNodes(parent, newJointDataNode.nextOffset0x4 + dataOffset);
            }
        }

        private static void BuildMaterialNodes(TreeNode parent, UInt32 offset)
        {
            var newMaterialNode = new MaterialNode(file, offset);

            var newTreeNode = new TreeNode("MaterialNode");
            newTreeNode.Tag = newMaterialNode;
            parent.Nodes.Add(newTreeNode);

            if (newMaterialNode.MaterialColorNodeOffset0x0C != 0)
            {
                BuildMaterialColorNodes(newTreeNode, newMaterialNode.MaterialColorNodeOffset0x0C + dataOffset);
            }

            if (newMaterialNode.TextureNodeOffset0x08 != 0)
            {
                BuildTextureNodes(newTreeNode, newMaterialNode.TextureNodeOffset0x08 + dataOffset);
            }


        }

        private static void BuildMaterialColorNodes(TreeNode parent, UInt32 offset)
        {
            var newMaterialColorNode = new MaterialColorNode(file, offset);

            var newTreeNode = new TreeNode("MaterialColorNode");
            newTreeNode.Tag = newMaterialColorNode;
            parent.Nodes.Add(newTreeNode);
        }

        private static void BuildTextureNodes(TreeNode parent, UInt32 offset)
        {
            var newTextureNode = new TextureNode(file, offset);

            var newTreeNode = new TreeNode("TextureNode");
            newTreeNode.Tag = newTextureNode;
            parent.Nodes.Add(newTreeNode);


            if (newTextureNode.paletteHeaderOffset0x50 != 0)
            {
                BuildPaletteHeader(newTreeNode, newTextureNode.paletteHeaderOffset0x50 + dataOffset, ref newTextureNode);
            }

            if (newTextureNode.imageHeaderOffset0x4C != 0)
            {
                BuildImageHeader(newTreeNode, newTextureNode.imageHeaderOffset0x4C + dataOffset, ref newTextureNode);

                bool add = true;
                for (int i = 0; i < textureList.Items.Count; i++)
                {
                    if (textureList.Items[i].ToString().IndexOf(newTextureNode.imageHeader.imageOffset0x0.ToString("x8")) != -1 )   //TODO: Make textureList a LL and not a ListBox
                        add = false;
                }
                if (add)
                    textureList.Items.Add(new TextureListObject(newTextureNode.imageHeader.imageOffset0x0, newTextureNode));
            }

        }

        private static void BuildImageHeader(TreeNode parent, UInt32 offset, ref TextureNode texNode)
        {
            var newImageHeader = new ImageHeader(file, offset);

            var newTreeNode = new TreeNode("ImageHeader");
            newTreeNode.Tag = newImageHeader;
            parent.Nodes.Add(newTreeNode);
            texNode.imageHeader = newImageHeader;
        }

        private static void BuildPaletteHeader(TreeNode parent, UInt32 offset, ref TextureNode texNode)
        {
            var newPaletteHeader = new PaletteHeader(file, offset);

            var newTreeNode = new TreeNode("PaletteHeader");
            newTreeNode.Tag = newPaletteHeader;
            parent.Nodes.Add(newTreeNode);
            texNode.paletteHeader = newPaletteHeader;
        }

        private static String ReadString(uint offset)
        {
            String output = "";
            while (file[offset] != 0)
            {
                output += (char) file[offset];
                offset++;
            }
            return output;
        }

        public static void ReplaceTexture(Image newImage, TextureListObject textureListObject)
        {
            ImageHeader imageHeader = textureListObject.imageHeader;
            PaletteHeader paletteHeader = textureListObject.paletteHeader;
            byte[] newPaletteData;
            byte[] newTextureData = TPL.ConvertToTextureMelee(newImage, textureListObject, out newPaletteData);

            if (newTextureData.Length != textureListObject.imageSize)
            {
                MessageBox.Show("Error: Selected image is not the same file size!");
                return;
            }

            if (paletteHeader != null && newPaletteData.Length > paletteHeader.colorCount0xC*2)
            {
                MessageBox.Show(
                    "The selected image contains more colors than the original image. Please use no more than " + paletteHeader.colorCount0xC + " colors.");
                return;
            }

            Array.ConstrainedCopy(newTextureData, 0, file, (int)(imageHeader.imageOffset0x0 + dataOffset), newTextureData.Length);
            if (paletteHeader != null) Array.ConstrainedCopy(newPaletteData, 0, file, (int)(paletteHeader.paletteOffset0x0 + dataOffset), Math.Min((paletteHeader.colorCount0xC * 2), newPaletteData.Length));
            OpenDatFile(file, fileName, ref textureList);
        }

        internal static void ExportImage(Image image, string filePath)
        {
            image.Save(filePath, ImageFormat.Png);
        }

        internal static void SaveDatFile(SaveFileDialog dialog)
        {
            byte[] datFile = file;
            Stream myStream;
            if ((myStream = dialog.OpenFile()) != null)
            {
                myStream.Write(datFile, 0, datFile.Length);
                myStream.Close();

            }
        }

        internal static byte[] OpenDatFile(Stream datStream)
        {
            byte[] outFile = new byte[datStream.Length];
            datStream.Read(outFile, 0, (int)datStream.Length);
            datStream.Close();
            return outFile;
        }

        private static void BuildFighterDataNodes(TreeNode parent, UInt32 offset)
        {
            var newFighterDataNode = new FighterDataNode(file, offset);

            var newTreeNode = new TreeNode("FighterDataNode");
            newTreeNode.Tag = newFighterDataNode;
            parent.Nodes.Add(newTreeNode);

            if (newFighterDataNode.unknownOffset0x20 != 0)
            {
                var nodeOffset = ReadUInt32(newFighterDataNode.unknownOffset0x20 + dataOffset);

                if (nodeOffset != 0)
                {
                    BuildJointNodes(newTreeNode, nodeOffset + dataOffset);
                }
            }

            if (newFighterDataNode.unknownOffset0x5C != 0)
            {
                BuildJointNodes(newTreeNode, newFighterDataNode.unknownOffset0x5C + dataOffset);
            }

        }
    }



}
