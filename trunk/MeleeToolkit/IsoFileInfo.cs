using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeleeToolkit
{
    public class IsoFileInfo
    {
        public IsoFileInfo(string name, uint size, uint fileOffset, uint fstOffset, bool isFolder)
        {
            Name = name;
            Size = size;
            FileOffset = fileOffset;
            FSTOffset = fstOffset;
            IsFolder = isFolder;
        }

        public string Name { get; set; }
        public uint Size { get; set; }
        public uint FileOffset { get; set; }
        public uint FSTOffset { get; set; }
        public bool IsFolder { get; set; }
    }
}
