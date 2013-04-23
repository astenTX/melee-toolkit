using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeleeToolkit
{
    public class DatNode
    {
        public UInt32 location;
        public Values[] values;

        public DatNode(byte[] file, UInt32 offset)
        {
            location = offset;
        }
    }

    public class DatHeader : DatNode
    {
        public UInt32 FileSize0x00;
        public UInt32 DataBlockSize0x04;
        public UInt32 RelocationTableCount0x08;
        public UInt32 RootCount0x0C;
        public UInt32 RootCount0x10;
        public UInt32 Unknown0x14;
        public UInt32 Unknown0x18;
        public UInt32 Unknown0x1C;

        public DatHeader(byte[] file, UInt32 offset) : base(file, offset)
        {
            values = new Values[8];
            values[0] = new Values("File Size", offset, 0x00, Values.Types.UInt32, false, file);
            values[1] = new Values("Data Block Size", offset, 0x04, Values.Types.UInt32, false, file);
            values[2] = new Values("Relocation Table Count", offset, 0x08, Values.Types.UInt32, false, file);
            values[3] = new Values("Root Count 1", offset, 0x0C, Values.Types.UInt32, false, file);
            values[4] = new Values("Root Count 2", offset, 0x10, Values.Types.UInt32, false, file);
            values[5] = new Values("Unknown", offset, 0x14, Values.Types.UInt32, false, file);
            values[6] = new Values("Unknown", offset, 0x18, Values.Types.UInt32, false, file);
            values[7] = new Values("Unknown", offset, 0x1C, Values.Types.UInt32, false, file);

            FileSize0x00 = (UInt32) values[0].value;
            DataBlockSize0x04 = (UInt32) values[1].value;
            RelocationTableCount0x08 = (UInt32) values[2].value;
            RootCount0x0C = (UInt32) values[3].value;
            RootCount0x10 = (UInt32) values[4].value;
            Unknown0x14 = (UInt32) values[5].value;
            Unknown0x18 = (UInt32) values[6].value;
            Unknown0x1C = (UInt32) values[7].value;
        }
    }

    public class RootNode : DatNode
    {
        public UInt32 RootOffset0x0;
        public UInt32 StringTableOffset0x4;

        public RootNode(byte[] file, UInt32 offset) : base(file, offset)
        {
            values = new Values[2];
            values[0] = new Values("Root Offset", offset, 0x00, Values.Types.UInt32, false, file);
            values[1] = new Values("String Table Offset", offset, 0x04, Values.Types.UInt32, false, file);

            RootOffset0x0 = (UInt32) values[0].value;
            StringTableOffset0x4 = (UInt32) values[1].value;
        }
    }

    public class JointNode : DatNode
    {
        public UInt32 unknown0x00;
        public UInt32 flags0x04;
        public UInt32 childOffset0x08; // child jobj public classure
        public UInt32 nextOffset0x0C; // next jobj public classure
        public UInt32 jointDataNodeOffset0x10; // dobj public classure - object information?
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

        public JointNode(byte[] file, UInt32 offset) : base(file, offset)
        {
            values = new Values[16];
            values[0] = new Values("Unknown", offset, 0x00, Values.Types.UInt32, true, file);
            values[1] = new Values("Flags", offset, 0x04, Values.Types.UInt32, true, file);
            values[2] = new Values("Child Offset", offset, 0x08, Values.Types.UInt32, false, file);
            values[3] = new Values("Next Offset", offset, 0x0C, Values.Types.UInt32, false, file);
            values[4] = new Values("Joint Data Node Offset", offset, 0x10, Values.Types.UInt32, false, file);
            values[5] = new Values("Rotation X", offset, 0x14, Values.Types.Float, true, file);
            values[6] = new Values("Rotation Y", offset, 0x18, Values.Types.Float, true, file);
            values[7] = new Values("Rotation Z", offset, 0x1C, Values.Types.Float, true, file);
            values[8] = new Values("Scale X", offset, 0x20, Values.Types.Float, true, file);
            values[9] = new Values("Scale Y", offset, 0x24, Values.Types.Float, true, file);
            values[10] = new Values("Scale Z", offset, 0x28, Values.Types.Float, true, file);
            values[11] = new Values("Translation X", offset, 0x2C, Values.Types.Float, true, file);
            values[12] = new Values("Translation Y", offset, 0x30, Values.Types.Float, true, file);
            values[13] = new Values("Translation Z", offset, 0x34, Values.Types.Float, true, file);
            values[14] = new Values("Transform Offset", offset, 0x38, Values.Types.UInt32, false, file);
            values[15] = new Values("Unknown", offset, 0x3C, Values.Types.UInt32, true, file);

            unknown0x00 = (UInt32) values[0].value;
            flags0x04 = (UInt32) values[1].value;
            childOffset0x08 = (UInt32) values[2].value;
            nextOffset0x0C = (UInt32) values[3].value;
            jointDataNodeOffset0x10 = (UInt32) values[4].value;
            rotationX0x14 = (float) values[5].value;
            rotationY0x18 = (float) values[6].value;
            rotationZ0x1C = (float) values[7].value;
            scaleX0x20 = (float) values[8].value;
            scaleY0x24 = (float) values[9].value;
            scaleZ0x28 = (float) values[10].value;
            translationX0x2C = (float) values[11].value;
            translationY0x30 = (float) values[12].value;
            translationZ0x34 = (float) values[13].value;
            transformOffset0x38 = (UInt32) values[14].value;
            unknown0x3C = (UInt32) values[15].value;
        }

    };

    public class JointDataNode : DatNode
    {
        public UInt32 unknown0x0;
        public UInt32 nextOffset0x4;
        public UInt32 materialNodeOffset0x8;
        public UInt32 meshNodeOffset0xC;

        public JointDataNode(byte[] file, UInt32 offset) : base(file, offset)
        {
            values = new Values[4];
            values[0] = new Values("Unknown", offset, 0x00, Values.Types.UInt32, true, file);
            values[1] = new Values("Next Offset", offset, 0x04, Values.Types.UInt32, false, file);
            values[2] = new Values("Material Node Offset", offset, 0x08, Values.Types.UInt32, false, file);
            values[3] = new Values("Mesh Node Offset", offset, 0x0C, Values.Types.UInt32, false, file);

            unknown0x0 = (UInt32) values[0].value;
            nextOffset0x4 = (UInt32) values[1].value;
            materialNodeOffset0x8 = (UInt32) values[2].value;
            meshNodeOffset0xC = (UInt32)values[3].value;
        }
    }

    public class MaterialNode : DatNode
    {
        public UInt32 unknown0x00;
        public UInt32 unknownFlags0x04;
        public UInt32 TextureNodeOffset0x08;
        public UInt32 MaterialColorNodeOffset0x0C;
        public UInt32 unknown0x10;
        public UInt32 unknown0x14;

        public MaterialNode(byte[] file, UInt32 offset) : base(file, offset)
        {
            values = new Values[6];
            values[0] = new Values("Unknown", offset, 0x00, Values.Types.UInt32, false, file);
            values[1] = new Values("Unknown Flags", offset, 0x04, Values.Types.UInt32, false, file);
            values[2] = new Values("Texture Node Offset", offset, 0x08, Values.Types.UInt32, false, file);
            values[3] = new Values("Material Color Node Offset", offset, 0x0C, Values.Types.UInt32, false, file);
            values[4] = new Values("Unknown", offset, 0x10, Values.Types.UInt32, false, file);
            values[5] = new Values("Unknown", offset, 0x14, Values.Types.UInt32, false, file);

            unknown0x00 = (UInt32) values[0].value;
            unknownFlags0x04 = (UInt32) values[1].value;
            TextureNodeOffset0x08 = (UInt32) values[2].value;
            MaterialColorNodeOffset0x0C = (UInt32) values[3].value;
            unknown0x10 = (UInt32) values[4].value;
            unknown0x14 = (UInt32) values[5].value;
        }
    }

    public class MaterialColorNode : DatNode
    {
        public UInt32 unknownColor0x00; // diffuse?
        public UInt32 unknownColor0x04; // ambient?
        public UInt32 unknownColor0x08; // specular?
        public float unknown0x0C;
        public float unknown0x10;

        public MaterialColorNode(byte[] file, UInt32 offset) : base(file, offset)
        {
            values = new Values[5];
            values[0] = new Values("Unknown Color", offset, 0x00, Values.Types.UInt32, true, file);
            values[1] = new Values("Unknown Color", offset, 0x04, Values.Types.UInt32, true, file);
            values[2] = new Values("Unknown Color", offset, 0x08, Values.Types.UInt32, true, file);
            values[3] = new Values("Unknown", offset, 0x0C, Values.Types.UInt32, true, file);
            values[4] = new Values("Unknown", offset, 0x10, Values.Types.UInt32, true, file);

            unknownColor0x00 = (UInt32) values[0].value;
            unknownColor0x04 = (UInt32) values[1].value;
            unknownColor0x08 = (UInt32) values[2].value;
            unknown0x0C = (UInt32) values[3].value;
            unknown0x10 = (UInt32)values[4].value;
        }
    }

    public class TextureNode : DatNode
    {
        public UInt32 unknown0x00;
        public UInt32 imageHeaderOffset0x4C;
        public UInt32 paletteHeaderOffset0x50;
        public UInt32 unknown0x54;
        public UInt32 unknownOffset0x58;
        public ImageHeader imageHeader;
        public PaletteHeader paletteHeader;

        public TextureNode(byte[] file, UInt32 offset) : base(file, offset)
        {
            values = new Values[5];
            values[0] = new Values("Unknown", offset, 0x00, Values.Types.UInt32, false, file);
            values[1] = new Values("Image Header Offset", offset, 0x4C, Values.Types.UInt32, false, file);
            values[2] = new Values("Palette Header Offset", offset, 0x50, Values.Types.UInt32, false, file);
            values[3] = new Values("Unknown", offset, 0x54, Values.Types.UInt32, false, file);
            values[4] = new Values("Unknown Offset", offset, 0x58, Values.Types.UInt32, false, file);

            unknown0x00 = (UInt32) values[0].value;
            imageHeaderOffset0x4C = (UInt32) values[1].value;
            paletteHeaderOffset0x50 = (UInt32) values[2].value;
            unknown0x54 = (UInt32) values[3].value;
            unknownOffset0x58 = (UInt32)values[4].value;
        }
    }

    public class ImageHeader : DatNode
    {
        public UInt32 imageOffset0x0;
        public UInt16 width0x4;
        public UInt16 height0x6;
        public UInt32 imageFormat0x8;
        public string imageFormatString;

        public ImageHeader(byte[] file, UInt32 offset) : base(file, offset)
        {
            values = new Values[4];
            values[0] = new Values("Image Offset", offset, 0x0, Values.Types.UInt32, false, file);
            values[1] = new Values("Width", offset, 0x4, Values.Types.UInt16, false, file);
            values[2] = new Values("Height", offset, 0x6, Values.Types.UInt16, false, file);
            values[3] = new Values("Image Format", offset, 0x8, Values.Types.UInt32, false, file);

            imageOffset0x0 = (UInt32) values[0].value;
            width0x4 = (UInt16) values[1].value;
            height0x6 = (UInt16) values[2].value;
            imageFormat0x8 = (UInt32) values[3].value;

            switch (imageFormat0x8)
            {
                case 0: imageFormatString = "I4"; break;
                case 1: imageFormatString = "I8"; break;
                case 2: imageFormatString = "IA4"; break;
                case 3: imageFormatString = "IA8"; break;
                case 4: imageFormatString = "RGB565"; break;
                case 5: imageFormatString = "RGB5A3"; break;
                case 6: imageFormatString = "RGBA8"; break;
                case 8: imageFormatString = "CI4"; break;
                case 9: imageFormatString = "CI8"; break;
                case 0xa: imageFormatString = "CI14X2"; break;
                case 0xe: imageFormatString = "CMPR"; break;
                default: imageFormatString = "Unknown"; break;
            }
        }
    }

    public class PaletteHeader : DatNode
    {
        public UInt32 paletteOffset0x0;
        public UInt32 paletteFormat0x4;
        public UInt32 unknown0x08;
        public UInt16 colorCount0xC;
        public UInt16 unknown0xE;
        public string paletteFormatString;

        public PaletteHeader(byte[] file, UInt32 offset) : base(file, offset)
        {
            values = new Values[5];
            values[0] = new Values("Palette Offset", offset, 0x00, Values.Types.UInt32, false, file);
            values[1] = new Values("Palette Format", offset, 0x04, Values.Types.UInt32, false, file);
            values[2] = new Values("Unknown", offset, 0x08, Values.Types.UInt32, false, file);
            values[3] = new Values("Color Count", offset, 0x0C, Values.Types.UInt16, false, file);
            values[4] = new Values("Unknown", offset, 0x0E, Values.Types.UInt16, false, file);

            paletteOffset0x0 = (UInt32) values[0].value;
            paletteFormat0x4 = (UInt32) values[1].value;
            unknown0x08 = (UInt32) values[2].value;
            colorCount0xC = (UInt16) values[3].value;
            unknown0xE = (UInt16)values[4].value;

            switch (paletteFormat0x4)
            {
                case 0: paletteFormatString = "IA8"; break;
                case 1: paletteFormatString = "RGB565"; break;
                case 2: paletteFormatString = "RGB5A3"; break;
                default: paletteFormatString = "Unknown"; break;
            }

        }
    }

    public class FighterDataNode : DatNode
    {
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


        public FighterDataNode(byte[] file, UInt32 offset) : base(file, offset)
        {
            values = new Values[24];
            values[0] = new Values("Unknown", offset, 0x00, Values.Types.UInt32, false, file);
            values[1] = new Values("Unknown", offset, 0x04, Values.Types.UInt32, false, file);
            values[2] = new Values("Unknown", offset, 0x08, Values.Types.UInt32, false, file);
            values[3] = new Values("Unknown", offset, 0x0C, Values.Types.UInt32, false, file);
            values[4] = new Values("Unknown", offset, 0x10, Values.Types.UInt32, false, file);
            values[5] = new Values("Unknown", offset, 0x14, Values.Types.UInt32, false, file);
            values[6] = new Values("Unknown", offset, 0x18, Values.Types.UInt32, false, file);
            values[7] = new Values("Unknown", offset, 0x1C, Values.Types.UInt32, false, file);
            values[8] = new Values("Unknown", offset, 0x20, Values.Types.UInt32, false, file);
            values[9] = new Values("Unknown", offset, 0x24, Values.Types.UInt32, false, file);
            values[10] = new Values("Unknown", offset, 0x28, Values.Types.UInt32, false, file);
            values[11] = new Values("Unknown", offset, 0x2C, Values.Types.UInt32, false, file);
            values[12] = new Values("Unknown", offset, 0x30, Values.Types.UInt32, false, file);
            values[13] = new Values("Unknown", offset, 0x34, Values.Types.UInt32, false, file);
            values[14] = new Values("Unknown", offset, 0x38, Values.Types.UInt32, false, file);
            values[15] = new Values("Unknown", offset, 0x3C, Values.Types.UInt32, false, file);
            values[16] = new Values("Unknown", offset, 0x40, Values.Types.UInt32, false, file);
            values[17] = new Values("Unknown", offset, 0x44, Values.Types.UInt32, false, file);
            values[18] = new Values("Unknown", offset, 0x48, Values.Types.UInt32, false, file);
            values[19] = new Values("Unknown", offset, 0x4C, Values.Types.UInt32, false, file);
            values[20] = new Values("Unknown", offset, 0x50, Values.Types.UInt32, false, file);
            values[21] = new Values("Unknown", offset, 0x54, Values.Types.UInt32, false, file);
            values[22] = new Values("Unknown", offset, 0x58, Values.Types.UInt32, false, file);
            values[23] = new Values("Unknown", offset, 0x5C, Values.Types.UInt32, false, file);

            unknownOffset0x00 = (UInt32) values[0].value;
            unknownOffset0x04 = (UInt32) values[1].value;
            unknownOffset0x08 = (UInt32) values[2].value;
            unknownOffset0x0C = (UInt32) values[3].value;
            unknownOffset0x10 = (UInt32) values[4].value;
            unknownOffset0x14 = (UInt32) values[5].value;
            unknownOffset0x18 = (UInt32) values[6].value;
            unknownOffset0x1C = (UInt32) values[7].value;
            unknownOffset0x20 = (UInt32) values[8].value;
            unknownOffset0x24 = (UInt32) values[9].value;
            unknownOffset0x28 = (UInt32) values[10].value;
            unknownOffset0x2C = (UInt32) values[11].value;
            unknownOffset0x30 = (UInt32) values[12].value;
            unknownOffset0x34 = (UInt32) values[13].value;
            unknownOffset0x38 = (UInt32) values[14].value;
            unknownOffset0x3C = (UInt32) values[15].value;
            unknownOffset0x40 = (UInt32) values[16].value;
            unknownOffset0x44 = (UInt32) values[17].value;
            unknownOffset0x48 = (UInt32) values[18].value;
            unknownOffset0x4C = (UInt32) values[19].value;
            unknownOffset0x50 = (UInt32) values[20].value;
            unknownOffset0x54 = (UInt32) values[21].value;
            unknownOffset0x58 = (UInt32) values[22].value;
            unknownOffset0x5C = (UInt32) values[23].value;
        }
    };


    public class Values
    {
        public string name;
        public int offset;
        public enum Types { UInt32, UInt16, Float };
        public Types type;
        public bool isEditable;
        public object value;

        public Values(string name, UInt32 location, int offset, Types type, bool isEditable, byte[] file)
        {
            this.name = name;
            this.offset = offset;
            int totalOffset = (int)location + offset;
            this.type = type;
            this.isEditable = isEditable;

            switch (type)
            {
                case Types.UInt32:
                    value = ReadUInt32(file, totalOffset);
                    break;
                case Types.UInt16:
                    value = ReadUInt16(file, totalOffset);
                    break;
                case Types.Float:
                    value = ReadFloat(file, totalOffset);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }




        private UInt32 ReadUInt32(byte[] data, int startIndex)
        {
            byte[] newData = new byte[4];
            newData = BitConverter.GetBytes(BitConverter.ToUInt32(data, startIndex));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(newData);
            return BitConverter.ToUInt32(newData, 0);
        }

        private UInt16 ReadUInt16(byte[] data, int startIndex)
        {
            byte[] newData = new byte[2];
            newData = BitConverter.GetBytes(BitConverter.ToUInt16(data, startIndex));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(newData);
            return BitConverter.ToUInt16(newData, 0);
        }

        private float ReadFloat(byte[] data, int startIndex)
        {
            byte[] newData = new byte[4];
            newData = BitConverter.GetBytes(BitConverter.ToSingle(data, startIndex));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(newData);
            return BitConverter.ToSingle(newData, 0);
        }

        private String ReadString(byte[] data, int startIndex)
        {
            String output = "";
            while (startIndex < data.Length || data[startIndex] != 0)
            {
                output += (char)data[startIndex];
                startIndex++;
            }
            return output;
        }
    }


}
