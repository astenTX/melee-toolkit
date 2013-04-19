using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeleeToolkit
{
    class TextureListObject
    {
        public string name;
        public TextureNode node;
        public ImageHeader imageHeader;
        public PaletteHeader paletteHeader;
        public byte[] imageData;
        public Bitmap imageBitmap;

        public TextureListObject(UInt32 imageHeaderOffset, TextureNode node)
        {
            name = "Texture " + (DatFile.textureList.Items.Count +1) + " (" + imageHeaderOffset.ToString("x8") + ")";
            this.node = node;
            imageHeader = node.imageHeader;
            paletteHeader = node.paletteHeader;
            

            if (imageHeader.imageFormat0x8 == 0xe)   //CMPR
            {
                //The actual image file size is not (width * height * bits per pixel)
                //It's actually the next multiple of blockWidth and blockHeight, so for example for CMPR if the width and height are 0xC7 and 0x91, the file size is:
                //(0xC8 * 0x98 / 2)
                int blockWidth = ImageDataFormat.Cmpr.BlockWidth;
                int blockHeight = ImageDataFormat.Cmpr.BlockHeight;
                int arrayWidth;
                int arrayHeight;
                if (imageHeader.width0x4 % blockWidth == 0)
                    arrayWidth = imageHeader.width0x4;
                else
                    arrayWidth = (imageHeader.width0x4) + (blockWidth - imageHeader.width0x4 % blockWidth);
                if (imageHeader.height0x6 % blockHeight == 0)
                    arrayHeight = imageHeader.height0x6;
                else
                    arrayHeight = (imageHeader.height0x6) + (blockHeight - imageHeader.height0x6 % blockHeight);
                imageData = new byte[arrayWidth * arrayHeight / (8 / ImageDataFormat.Cmpr.BitsPerPixel)];
                Array.ConstrainedCopy(DatFile.file, (int)(imageHeader.imageOffset0x0 + DatFile.dataOffset), imageData, 0, imageData.GetLength(0));
                imageBitmap = ImageData.ToBitmap(ImageDataFormat.Cmpr.ConvertFrom(imageData, imageHeader.width0x4, imageHeader.height0x6), imageHeader.width0x4, imageHeader.height0x6);
            }

            else if (imageHeader.imageFormat0x8 == 0)   //i4
            {
                int blockWidth = ImageDataFormat.I4.BlockWidth;
                int blockHeight = ImageDataFormat.I4.BlockHeight;
                int arrayWidth;
                int arrayHeight;
                if (imageHeader.width0x4 % blockWidth == 0)
                    arrayWidth = imageHeader.width0x4;
                else
                    arrayWidth = (imageHeader.width0x4) + (blockWidth - imageHeader.width0x4 % blockWidth);
                if (imageHeader.height0x6 % blockHeight == 0)
                    arrayHeight = imageHeader.height0x6;
                else
                    arrayHeight = (imageHeader.height0x6) + (blockHeight - imageHeader.height0x6 % blockHeight);

                imageData = new byte[arrayWidth * arrayHeight / (8 / ImageDataFormat.I4.BitsPerPixel)];
                Array.ConstrainedCopy(DatFile.file, (int)(imageHeader.imageOffset0x0 + DatFile.dataOffset), imageData, 0, imageData.GetLength(0));
                imageBitmap = ImageData.ToBitmap(ImageDataFormat.I4.ConvertFrom(imageData, imageHeader.width0x4, imageHeader.height0x6), imageHeader.width0x4, imageHeader.height0x6);
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
