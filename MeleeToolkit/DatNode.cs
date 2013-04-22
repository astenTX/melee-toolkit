using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeleeToolkit
{
    public class DatNode
    {
        public enum NodeTypes
        {
            DatHeader,
            RootNode,
            JointNode,
            JointDataNode,
            MaterialNode,
            MaterialColorNode,
            TextureNode,
            ImageHeader,
            PaletteHeader,
            FighterDataNode
        };

        public string name;
        public NodeTypes type;
        public UInt32 location;
        public byte[] data;
        public Values[] values;

        public DatNode(NodeTypes type, UInt32 location, byte[] data)
        {
            this.type = type;
            this.location = location;
            this.data = data;

            switch (type)
            {
                case NodeTypes.DatHeader:
                    values = new Values[8];
                    values[0] = new Values("File Size", 0x00, Values.Types.UInt32, data);
                    values[1] = new Values("Data Block Size", 0x04, Values.Types.UInt32, data);
                    values[2] = new Values("Relocation Table Count", 0x08, Values.Types.UInt32, data);
                    values[3] = new Values("Root Count 1", 0x0C, Values.Types.UInt32, data);
                    values[4] = new Values("Root Count 2", 0x10, Values.Types.UInt32, data);
                    values[5] = new Values("Unknown", 0x14, Values.Types.UInt32, data);
                    values[6] = new Values("Unknown", 0x18, Values.Types.UInt32, data);
                    values[7] = new Values("Unknown", 0x1C, Values.Types.UInt32, data);
                    break;
                case NodeTypes.RootNode:
                    values = new Values[2];
                    values[0] = new Values("Root Offset", 0x00, Values.Types.UInt32, data);
                    values[1] = new Values("String Table Offset", 0x04, Values.Types.UInt32, data);
                    break;
                case NodeTypes.JointNode:
                    values = new Values[16];
                    values[0] = new Values("Unknown", 0x00, Values.Types.UInt32, data);
                    values[1] = new Values("Flags", 0x04, Values.Types.UInt32, data);
                    values[2] = new Values("Child Offset", 0x08, Values.Types.UInt32, data);
                    values[3] = new Values("Next Offset", 0x0C, Values.Types.UInt32, data);
                    values[4] = new Values("Joint Data Node Offset", 0x10, Values.Types.UInt32, data);
                    values[5] = new Values("Rotation X", 0x14, Values.Types.Float, data);
                    values[6] = new Values("Rotation Y", 0x18, Values.Types.Float, data);
                    values[7] = new Values("Rotation Z", 0x1C, Values.Types.Float, data);
                    values[8] = new Values("Scale X", 0x20, Values.Types.Float, data);
                    values[9] = new Values("Scale Y", 0x24, Values.Types.Float, data);
                    values[10] = new Values("Scale Z", 0x28, Values.Types.Float, data);
                    values[11] = new Values("Translation X", 0x2C, Values.Types.Float, data);
                    values[12] = new Values("Translation Y", 0x30, Values.Types.Float, data);
                    values[13] = new Values("Translation Z", 0x34, Values.Types.Float, data);
                    values[14] = new Values("Transform Offset", 0x38, Values.Types.UInt32, data);
                    values[15] = new Values("Unknown", 0x3C, Values.Types.UInt32, data);
                    break;
                case NodeTypes.JointDataNode:
                    values = new Values[4];
                    values[0] = new Values("Unknown", 0x00, Values.Types.UInt32, data);
                    values[1] = new Values("Next Offset", 0x04, Values.Types.UInt32, data);
                    values[2] = new Values("Material Node Offset", 0x08, Values.Types.UInt32, data);
                    values[3] = new Values("Mesh Node Offset", 0x0C, Values.Types.UInt32, data);
                    break;
                case NodeTypes.MaterialNode:
                    values = new Values[6];
                    values[0] = new Values("Unknown", 0x00, Values.Types.UInt32, data);
                    values[1] = new Values("Unknown Flags", 0x04, Values.Types.UInt32, data);
                    values[2] = new Values("Texture Node Offset", 0x08, Values.Types.UInt32, data);
                    values[3] = new Values("Material Color Node Offset", 0x0C, Values.Types.UInt32, data);
                    values[4] = new Values("Unknown", 0x10, Values.Types.UInt32, data);
                    values[5] = new Values("Unknown", 0x14, Values.Types.UInt32, data);
                    break;
                case NodeTypes.MaterialColorNode:
                    values = new Values[5];
                    values[0] = new Values("Unknown Color", 0x00, Values.Types.UInt32, data);
                    values[1] = new Values("Unknown Color", 0x04, Values.Types.UInt32, data);
                    values[2] = new Values("Unknown Color", 0x08, Values.Types.UInt32, data);
                    values[3] = new Values("Unknown", 0x0C, Values.Types.UInt32, data);
                    values[4] = new Values("Unknown", 0x10, Values.Types.UInt32, data);
                    break;
                case NodeTypes.TextureNode:
                    values = new Values[5];
                    values[0] = new Values("Unknown", 0x00, Values.Types.UInt32, data);
                    values[1] = new Values("Image Header Offset", 0x4C, Values.Types.UInt32, data);
                    values[2] = new Values("Palette Header Offset", 0x50, Values.Types.UInt32, data);
                    values[3] = new Values("Unknown", 0x54, Values.Types.UInt32, data);
                    values[4] = new Values("Unknown Offset", 0x58, Values.Types.UInt32, data);
                    break;
                case NodeTypes.ImageHeader:
                    values = new Values[4];
                    values[0] = new Values("Image Offset", 0x0, Values.Types.UInt32, data);
                    values[1] = new Values("Width", 0x4, Values.Types.UInt16, data);
                    values[2] = new Values("Height", 0x6, Values.Types.UInt16, data);
                    values[3] = new Values("Image Format", 0x8, Values.Types.UInt32, data);
                    break;
                case NodeTypes.PaletteHeader:
                    values = new Values[5];
                    values[0] = new Values("Palette Offset", 0x00, Values.Types.UInt32, data);
                    values[1] = new Values("Palette Format", 0x04, Values.Types.UInt32, data);
                    values[2] = new Values("Unknown", 0x08, Values.Types.UInt32, data);
                    values[3] = new Values("Color Count", 0x0C, Values.Types.UInt16, data);
                    values[4] = new Values("Unknown", 0x0E, Values.Types.UInt16, data);
                    break;
                case NodeTypes.FighterDataNode:
                    values = new Values[24];
                    values[0] = new Values("Unknown", 0x00, Values.Types.UInt32, data);
                    values[1] = new Values("Unknown", 0x04, Values.Types.UInt32, data);
                    values[2] = new Values("Unknown", 0x08, Values.Types.UInt32, data);
                    values[3] = new Values("Unknown", 0x0C, Values.Types.UInt32, data);
                    values[4] = new Values("Unknown", 0x10, Values.Types.UInt32, data);
                    values[5] = new Values("Unknown", 0x14, Values.Types.UInt32, data);
                    values[6] = new Values("Unknown", 0x18, Values.Types.UInt32, data);
                    values[7] = new Values("Unknown", 0x1C, Values.Types.UInt32, data);
                    values[8] = new Values("Unknown", 0x20, Values.Types.UInt32, data);
                    values[9] = new Values("Unknown", 0x24, Values.Types.UInt32, data);
                    values[10] = new Values("Unknown", 0x28, Values.Types.UInt32, data);
                    values[11] = new Values("Unknown", 0x2C, Values.Types.UInt32, data);
                    values[12] = new Values("Unknown", 0x30, Values.Types.UInt32, data);
                    values[13] = new Values("Unknown", 0x34, Values.Types.UInt32, data);
                    values[14] = new Values("Unknown", 0x38, Values.Types.UInt32, data);
                    values[15] = new Values("Unknown", 0x3C, Values.Types.UInt32, data);
                    values[16] = new Values("Unknown", 0x40, Values.Types.UInt32, data);
                    values[17] = new Values("Unknown", 0x44, Values.Types.UInt32, data);
                    values[18] = new Values("Unknown", 0x48, Values.Types.UInt32, data);
                    values[19] = new Values("Unknown", 0x4C, Values.Types.UInt32, data);
                    values[20] = new Values("Unknown", 0x50, Values.Types.UInt32, data);
                    values[21] = new Values("Unknown", 0x54, Values.Types.UInt32, data);
                    values[22] = new Values("Unknown", 0x58, Values.Types.UInt32, data);
                    values[23] = new Values("Unknown", 0x5C, Values.Types.UInt32, data);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }
    }


    public class Values
    {
        public string name;
        public int offset;
        public enum Types { UInt32, UInt16, Float };
        public Types type;
        public object value;

        public Values(string name, int offset, Types type, byte[] nodeData)
        {
            this.name = name;
            this.offset = offset;
            this.type = type;

            switch (type)
            {
                case Types.UInt32:
                    value = ReadUInt32(nodeData, offset);
                    break;
                case Types.UInt16:
                    value = ReadUInt16(nodeData, offset);
                    break;
                case Types.Float:
                    value = ReadFloat(nodeData, offset);
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
            newData = BitConverter.GetBytes(BitConverter.ToUInt32(data, startIndex));
            if (BitConverter.IsLittleEndian)
                Array.Reverse(newData);
            return BitConverter.ToUInt16(newData, 0);
        }

        private float ReadFloat(byte[] data, int startIndex)
        {
            byte[] newData = new byte[2];
            newData = BitConverter.GetBytes(BitConverter.ToUInt32(data, startIndex));
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
