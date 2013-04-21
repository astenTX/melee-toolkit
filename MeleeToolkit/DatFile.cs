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
            fileHeader = new DatHeader
                {
                    FileSize0x00 = ReadUInt32(0x0), 
                    DataBlockSize0x04 = ReadUInt32(0x4), 
                    RelocationTableCount0x08 = ReadUInt32(0x8), 
                    RootCount0x0C = ReadUInt32(0xC), 
                    RootCount0x10 = ReadUInt32(0x10), 
                    Unknown0x14 = ReadUInt32(0x14), 
                    Unknown0x18 = ReadUInt32(0x18), 
                    Unknown0x1C = ReadUInt32(0x1C)
                };
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
                var newRootNode = new RootNode
                    {
                        location = rootOffset0 + (UInt32) (i * 0x8),
                        RootOffset0x0 = ReadUInt32(rootOffset0 + (UInt32)(i*0x8)),
                        StringTableOffset0x4 = ReadUInt32(rootOffset0 + (UInt32)(i*0x8) + 0x4)
                    };
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
                var newRootNode = new RootNode
                {
                    location = rootOffset1 + (UInt32)(i * 0x8),
                    RootOffset0x0 = ReadUInt32(rootOffset1 + (UInt32)(i * 0x8)),
                    StringTableOffset0x4 = ReadUInt32(rootOffset1 + (UInt32)(i * 0x8) + 0x4)
                };
                var newTreeNode = new TreeNode(ReadString(tableOffset + newRootNode.StringTableOffset0x4));
                newTreeNode.Tag = newRootNode;
                rootNode.Nodes.Add(newTreeNode);
                if (newTreeNode.Text.IndexOf("joint") != -1 && newTreeNode.Text.IndexOf("matanim") == -1 && newTreeNode.Text.IndexOf("shapeanim") == -1)
                    BuildJointNodes(newTreeNode, newRootNode.RootOffset0x0 + dataOffset);
            }
        }

        private static void BuildJointNodes(TreeNode parent, UInt32 offset)
        {
            var newJointNode = new JointNode
                {
                    location = offset,
                    unknown0x00 = ReadUInt32(offset),
                    flags0x04 = ReadUInt32(offset + 0x4),
                    childOffset0x08 = ReadUInt32(offset + 0x8),
                    nextOffset0x0C = ReadUInt32(offset + 0xC),
                    jointDataNodeOffset0x10 = ReadUInt32(offset + 0x10),
                    rotationX0x14 = ReadFloat(offset + 0x14),
                    rotationY0x18 = ReadFloat(offset + 0x18),
                    rotationZ0x1C = ReadFloat(offset + 0x1C),
                    scaleX0x20 = ReadFloat(offset + dataOffset),
                    scaleY0x24 = ReadFloat(offset + 0x24),
                    scaleZ0x28 = ReadFloat(offset + 0x28),
                    translationX0x2C = ReadFloat(offset + 0x2C),
                    translationY0x30 = ReadFloat(offset + 0x30),
                    translationZ0x34 = ReadFloat(offset + 0x34),
                    transformOffset0x38 = ReadUInt32(offset + 0x38),
                    unknown0x3C = ReadUInt32(offset + 0x3C)
                };

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
            var newJointDataNode = new JointDataNode
                {
                    location = offset,
                    unknown0x0 = ReadUInt32(offset),
                    nextOffset0x4 = ReadUInt32(offset + 0x4),
                    materialNodeOffset0x8 = ReadUInt32(offset + 0x8),
                    meshNodeOffset0xC = ReadUInt32(offset + 0xC)
                };

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
            var newMaterialNode = new MaterialNode
                {
                    location = offset,
                    unknown0x00 = ReadUInt32(offset),
                    unknownFlags0x04 = ReadUInt32(offset + 0x04),
                    TextureNodeOffset0x08 = ReadUInt32(offset + 0x08),
                    MaterialColorNodeOffset0x0C = ReadUInt32(offset + 0x0C),
                    unknown0x10 = ReadUInt32(offset + 0x10),
                    unknown0x14 = ReadUInt32(offset + 0x14)
                };

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
            var newMaterialColorNode = new MaterialColorNode
                {
                    location = offset,
                    unknownColor0x00 = ReadUInt32(offset),
                    unknownColor0x04 = ReadUInt32(offset + 0x04),
                    unknownColor0x08 = ReadUInt32(offset + 0x08),
                    unknown0x0C = ReadFloat(offset + 0x0C),
                    unknown0x10 = ReadFloat(offset + 0x10)
                };

            var newTreeNode = new TreeNode("MaterialColorNode");
            newTreeNode.Tag = newMaterialColorNode;
            parent.Nodes.Add(newTreeNode);
        }

        private static void BuildTextureNodes(TreeNode parent, UInt32 offset)
        {
            var newTextureNode = new TextureNode
                {
                    location = offset,
                    unknown0x00 = ReadUInt32(offset),
                    imageHeaderOffset0x4C = ReadUInt32(offset + 0x4C),
                    paletteHeaderOffset0x50 = ReadUInt32(offset + 0x50),
                    unknown0x54 = ReadUInt32(offset + 0x54),
                    unknownOffset0x58 = ReadUInt32(offset + 0x58)
                };

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
            var newImageHeader = new ImageHeader
                {
                    location = offset,
                    imageOffset0x0 = ReadUInt32(offset),
                    width0x4 = ReadUInt16(offset + 0x4),
                    height0x6 = ReadUInt16(offset + 0x6),
                    imageFormat0x8 = ReadUInt32(offset + 0x8)
                };

            switch (newImageHeader.imageFormat0x8)
            {
                case 0: newImageHeader.imageFormatString = "I4"; break;
                case 1: newImageHeader.imageFormatString = "I8"; break;
                case 2: newImageHeader.imageFormatString = "IA4"; break;
                case 3: newImageHeader.imageFormatString = "IA8"; break;
                case 4: newImageHeader.imageFormatString = "RGB565"; break;
                case 5: newImageHeader.imageFormatString = "RGB5A3"; break;
                case 6: newImageHeader.imageFormatString = "RGBA8"; break;
                case 8: newImageHeader.imageFormatString = "CI4"; break;
                case 9: newImageHeader.imageFormatString = "CI8"; break;
                case 0xa: newImageHeader.imageFormatString = "CI14X2"; break;
                case 0xe: newImageHeader.imageFormatString = "CMPR"; break;
                default: newImageHeader.imageFormatString = "Unknown"; break;
            }

            var newTreeNode = new TreeNode("ImageHeader");
            newTreeNode.Tag = newImageHeader;
            parent.Nodes.Add(newTreeNode);
            texNode.imageHeader = newImageHeader;
        }

        private static void BuildPaletteHeader(TreeNode parent, UInt32 offset, ref TextureNode texNode)
        {
            var newPaletteHeader = new PaletteHeader
                {
                    location = offset,
                    paletteOffset0x0 = ReadUInt32(offset),
                    paletteFormat0x4 = ReadUInt32(offset + 0x4),
                    unknown0x08 = ReadUInt32(offset + 0x8),
                    colorCount0xA = ReadUInt16(offset + 0xC),
                    unknown0xA = ReadUInt16(offset + 0xE)
                };

            switch (newPaletteHeader.paletteFormat0x4)
            {
                case 0:
                    newPaletteHeader.paletteFormatString = "IA8"; break;
                case 1:
                    newPaletteHeader.paletteFormatString = "RGB565"; break;
                case 2:
                    newPaletteHeader.paletteFormatString = "RGB5A3"; break;
                default:
                    newPaletteHeader.paletteFormatString = "Unknown"; break;
            }

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

            if (newPaletteData.Length > paletteHeader.colorCount0xA*2)
            {
                MessageBox.Show(
                    "The selected image contains more colors than the original image. Please use no more than " + paletteHeader.colorCount0xA + " colors.");
                return;
            }

            Array.ConstrainedCopy(newTextureData, 0, file, (int)(imageHeader.imageOffset0x0 + dataOffset), newTextureData.Length);
            Array.ConstrainedCopy(newPaletteData, 0, file, (int)(paletteHeader.paletteOffset0x0 + dataOffset), Math.Min((paletteHeader.colorCount0xA * 2), newPaletteData.Length));
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
            var newFighterDataNode = new FighterDataNode
            {
                location = offset,
                // 0x00
                unknownOffset0x00 = ReadUInt32(offset + 0x00), // 0x184 - fighter attributes
                unknownOffset0x04 = ReadUInt32(offset + 0x04),
                unknownOffset0x08 = ReadUInt32(offset + 0x08), // 0x18 - {uint32; offset; uint32; offset; uint32[2];}
                unknownOffset0x0C = ReadUInt32(offset + 0x0C), // 0x18 - {offset; uint32[2]; offset; uint32[2];}[] --
                // 0x10
                unknownOffset0x10 = ReadUInt32(offset + 0x10), // 0x??
                unknownOffset0x14 = ReadUInt32(offset + 0x14), // 0x18 - {offset; uint32[2]; offset; uint32[2];}[] -- win animation info?
                unknownOffset0x18 = ReadUInt32(offset + 0x18), // 0x1C?x
                unknownOffset0x1C = ReadUInt32(offset + 0x1C), // 0x?? - {offset[];}
                // 0x20
                unknownOffset0x20 = ReadUInt32(offset + 0x20), // 0x04 - {offset;} - JOBJ_DATA
                unknownOffset0x24 = ReadUInt32(offset + 0x24), // 0x18?
                unknownOffset0x28 = ReadUInt32(offset + 0x28),
                unknownOffset0x2C = ReadUInt32(offset + 0x2C), // 0x14 - {uint32; offset; uint32; offset; uint32;}
                // 0x30
                unknownOffset0x30 = ReadUInt32(offset + 0x30), // 0x08 - {uint32; offset;}
                unknownOffset0x34 = ReadUInt32(offset + 0x34), // 0x08?
                unknownOffset0x38 = ReadUInt32(offset + 0x38), // 0x28
                unknownOffset0x3C = ReadUInt32(offset + 0x3C), // 0x18
                // 0x40
                unknownOffset0x40 = ReadUInt32(offset + 0x40), // 0x30
                unknownOffset0x44 = ReadUInt32(offset + 0x44), // 0x1C
                unknownOffset0x48 = ReadUInt32(offset + 0x48), // 0x0C+ - {offset; offset; offset;}
                unknownOffset0x4C = ReadUInt32(offset + 0x4C), // 0x38 - {offset; uint32[6]; offset; offset; uint32[5];}
                // 0x50
                unknownOffset0x50 = ReadUInt32(offset + 0x50), // 0x08 - {uint32; float;}
                unknownOffset0x54 = ReadUInt32(offset + 0x54), // 0x14
                unknownOffset0x58 = ReadUInt32(offset + 0x58), // 0x34
                unknownOffset0x5C = ReadUInt32(offset + 0x5C)  // 0x40 - JOBJ_DATA
            };

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

    struct DatHeader
    {
        public UInt32 FileSize0x00;
        public UInt32 DataBlockSize0x04;
        public UInt32 RelocationTableCount0x08;
        public UInt32 RootCount0x0C;
        public UInt32 RootCount0x10;
        public UInt32 Unknown0x14;
        public UInt32 Unknown0x18;
        public UInt32 Unknown0x1C;
    }

    struct RootNode
    {
        public UInt32 location;
        public UInt32 RootOffset0x0;
        public UInt32 StringTableOffset0x4;
    }

    struct JointNode
    {
        public UInt32 location;
        public UInt32 unknown0x00;
        public UInt32 flags0x04;
        public UInt32 childOffset0x08; // child jobj structure
        public UInt32 nextOffset0x0C; // next jobj structure
        public UInt32 jointDataNodeOffset0x10; // dobj structure - object information?
        public float rotationX0x14;
        public float rotationY0x18;
        public float rotationZ0x1C;
        public float scaleX0x20;                            // scale
        public float scaleY0x24;
        public float scaleZ0x28;
        public float translationX0x2C;                      // translation
        public float translationY0x30;
        public float translationZ0x34;
        public UInt32 transformOffset0x38; // inverse transform
        public UInt32 unknown0x3C;
	    
    };

    struct JointDataNode
    {
        public UInt32 location;
        public UInt32 unknown0x0;
        public UInt32 nextOffset0x4;
        public UInt32 materialNodeOffset0x8;
        public UInt32 meshNodeOffset0xC;
    }

    struct MaterialNode
    {
        public UInt32 location;
        public UInt32 unknown0x00;
        public UInt32 unknownFlags0x04;
        public UInt32 TextureNodeOffset0x08;
        public UInt32 MaterialColorNodeOffset0x0C;
        public UInt32 unknown0x10;
        public UInt32 unknown0x14;
    }

    struct MaterialColorNode
    {
        public UInt32 location;
        public UInt32 unknownColor0x00; // diffuse?
        public UInt32 unknownColor0x04; // ambient?
        public UInt32 unknownColor0x08; // specular?
        public float  unknown0x0C;
        public float  unknown0x10;
    }

    public struct TextureNode
    {
        public UInt32 location;
        public UInt32 unknown0x00;
        public UInt32 imageHeaderOffset0x4C;
        public UInt32 paletteHeaderOffset0x50;
        public UInt32 unknown0x54;
        public UInt32 unknownOffset0x58;
        public ImageHeader imageHeader;
        public PaletteHeader paletteHeader;
    }

    public struct ImageHeader
    {
        public UInt32 location;
        public UInt32 imageOffset0x0;
        public UInt16 height0x6;
        public UInt16 width0x4;
        public UInt32 imageFormat0x8;
        public string imageFormatString;
    }

    public struct PaletteHeader
    {
        public UInt32 location;
        public UInt32 paletteOffset0x0;
        public UInt32 paletteFormat0x4;
        public UInt32 unknown0x08;
        public UInt16 colorCount0xA;
        public UInt16 unknown0xA;
        public string paletteFormatString;
    }

    public struct FighterDataNode
    {
        public UInt32 location;
        // 0x00
        public UInt32 unknownOffset0x00; // 0x184 - fighter attributes
        public UInt32 unknownOffset0x04;
        public UInt32 unknownOffset0x08; // 0x18 - {uint32; offset; uint32; offset; uint32[2];}
        public UInt32 unknownOffset0x0C; // 0x18 - {offset; uint32[2]; offset; uint32[2];}[] --
        // 0x10
        public UInt32 unknownOffset0x10; // 0x??
        public UInt32 unknownOffset0x14; // 0x18 - {offset; uint32[2]; offset; uint32[2];}[] -- win animation info?
        public UInt32 unknownOffset0x18; // 0x1C?x
        public UInt32 unknownOffset0x1C; // 0x?? - {offset[];}
        // 0x20
        public UInt32 unknownOffset0x20; // 0x04 - {offset;} - JOBJ_DATA
        public UInt32 unknownOffset0x24; // 0x18?
        public UInt32 unknownOffset0x28;
        public UInt32 unknownOffset0x2C; // 0x14 - {uint32; offset; uint32; offset; uint32;}
        // 0x30
        public UInt32 unknownOffset0x30; // 0x08 - {uint32; offset;}
        public UInt32 unknownOffset0x34; // 0x08?
        public UInt32 unknownOffset0x38; // 0x28
        public UInt32 unknownOffset0x3C; // 0x18
        // 0x40
        public UInt32 unknownOffset0x40; // 0x30
        public UInt32 unknownOffset0x44; // 0x1C
        public UInt32 unknownOffset0x48; // 0x0C+ - {offset; offset; offset;}
        public UInt32 unknownOffset0x4C; // 0x38 - {offset; uint32[6]; offset; offset; uint32[5];}
        // 0x50
        public UInt32 unknownOffset0x50; // 0x08 - {uint32; float;}
        public UInt32 unknownOffset0x54; // 0x14
        public UInt32 unknownOffset0x58; // 0x34
        public UInt32 unknownOffset0x5C; // 0x40 - JOBJ_DATA
    };

}
