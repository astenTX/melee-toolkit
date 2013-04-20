using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libWiiSharp;

namespace MeleeToolkit
{
    class TextureListObject
    {
        public string name;
        public TextureNode node;
        public ImageHeader imageHeader;
        public PaletteHeader paletteHeader;
        public Bitmap imageBitmap;
        public int imageSize;

        public TextureListObject(UInt32 imageHeaderOffset, TextureNode node)
        {
            name = "Texture " + (DatFile.textureList.Items.Count +1) + " (" + imageHeaderOffset.ToString("x8") + ")";
            this.node = node;
            imageHeader = node.imageHeader;
            paletteHeader = node.paletteHeader;
            imageBitmap = TPL.ConvertFromTextureMelee(imageHeader, paletteHeader, out imageSize);





        }

        public override string ToString()
        {
            return name;
        }
    }
}
