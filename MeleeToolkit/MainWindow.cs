using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeleeToolkit.Properties;

namespace MeleeToolkit
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void Button_openfile_Click(object sender, EventArgs e)
        {
            openDiscImageDialog.InitialDirectory = Settings.Default.openDiscImagePath;
            openDiscImageDialog.FileName = "";

            if (openDiscImageDialog.ShowDialog() == DialogResult.OK)
            {
                var info = new FileInfo(openDiscImageDialog.FileName);
                Settings.Default.openDiscImagePath = Path.GetDirectoryName(openDiscImageDialog.FileName);

                FileHandler.OpenFile(info.FullName);
                label7.Text = FileHandler.GetTitle();
                label6.Text = FileHandler.GetVersion();
                filesystemTreeView.Nodes.Clear();
                filesystemTreeView.BeginUpdate();
                filesystemTreeView.Nodes.Add(FileHandler.root);
                filesystemTreeView.Nodes[0].Expand();
                filesystemTreeView.EndUpdate();
                if (DatFile.isFileLoaded) button4.Enabled = true;


            }
        }

        private void OpenDatFileAndUpdateWindow(byte[] file, string fileName)
        {
            DatFile.OpenDatFile(file, fileName, ref textureListBox);
            label2.Text = DatFile.fileName;
            label3.Text = DatFile.file.Length.ToString() + " bytes";
            button3.Enabled = true;
            if (FileHandler.isFileLoaded) button4.Enabled = true;
            texturePictureBox.Image = null;
            textureInfoLabel.Text = "Size:\nFormat:\nColors:";
            nodesTreeView.Nodes.Clear();
            nodesTreeView.BeginUpdate();
            nodesTreeView.Nodes.Add(DatFile.rootNode);
            nodesTreeView.Nodes[0].Expand();
            nodesTreeView.EndUpdate();
        }

        private void Button_Export_Click(object sender, EventArgs e)
        {
            IsoFileInfo info = (IsoFileInfo) filesystemTreeView.SelectedNode.Tag;
            String extension = info.Name.Substring(info.Name.IndexOf('.'));
            var saveFileDialog1 = new SaveFileDialog
            {
                Title = "Export File",
                InitialDirectory = Settings.Default.saveDatFilePath,
                FileName = info.Name,
                Filter = "All files|*.*",
                FilterIndex = 1,
                AddExtension = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.saveDatFilePath = Path.GetDirectoryName(saveFileDialog1.FileName);
                FileHandler.ExportFileFromIso(info, saveFileDialog1);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (filesystemTreeView.SelectedNode == null || filesystemTreeView.SelectedNode.Nodes.Count > 0)
            {
                Button_Export.Enabled = false;
                Button_Open.Enabled = false;
                button6.Enabled = false;
            }

            else
            {
                Button_Export.Enabled = filesystemTreeView.SelectedNode.Text.IndexOf(".") != -1;
                Button_Open.Enabled = filesystemTreeView.SelectedNode.Text.IndexOf(".") != -1;
                button6.Enabled = filesystemTreeView.SelectedNode.Text.IndexOf(".") != -1;
            }

            IsoFileInfo fileInfo = (IsoFileInfo)filesystemTreeView.SelectedNode.Tag;
            label5.Text = fileInfo.Name;
            label4.Text = fileInfo.Size.ToString() + " bytes";
        }

        private void Button_Open_Click(object sender, EventArgs e)
        {
            IsoFileInfo info = (IsoFileInfo) filesystemTreeView.SelectedNode.Tag;
            OpenDatFileAndUpdateWindow(FileHandler.OpenDatFile(info), info.Name);
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dataGridView1.Rows.Clear();
            
            if (nodesTreeView.SelectedNode.Level == 0)
            {
                DatHeader node = (DatHeader) nodesTreeView.SelectedNode.Tag;
                dataGridView1.Rows.Add("", "Location", "00000000");
                dataGridView1.Rows.Add("0x00", "File Size", node.FileSize0x00.ToString("x8"));
                dataGridView1.Rows.Add("0x04", "Data Block Size", node.DataBlockSize0x04.ToString("x8"));
                dataGridView1.Rows.Add("0x08", "Relocation Table Count", node.RelocationTableCount0x08.ToString("x8"));
                dataGridView1.Rows.Add("0x0C", "Root Node Count 1", node.RootCount0x0C.ToString("x8"));
                dataGridView1.Rows.Add("0x10", "Root Node Count 2", node.RootCount0x10.ToString("x8"));
                dataGridView1.Rows.Add("0x14", "Unknown", node.Unknown0x14.ToString("x8"));
                dataGridView1.Rows.Add("0x18", "Unknown", node.Unknown0x18.ToString("x8"));
                dataGridView1.Rows.Add("0x1C", "Unknown", node.Unknown0x1C.ToString("x8"));
            }
            else if (nodesTreeView.SelectedNode.Level == 1)
            {
                RootNode node = (RootNode) nodesTreeView.SelectedNode.Tag;
                dataGridView1.Rows.Add("", "Location", node.location.ToString("x8"));
                dataGridView1.Rows.Add("0x00", "Data Offset", node.RootOffset0x0.ToString("x8"));
                dataGridView1.Rows.Add("0x04", "String Table Offset", node.StringTableOffset0x4.ToString("x8"));
                dataGridView1.Tag = node;
            }

            else if (nodesTreeView.SelectedNode.Text == "JointNode")
            {
                JointNode node = (JointNode)nodesTreeView.SelectedNode.Tag;
                dataGridView1.Rows.Add("", "Location", node.location.ToString("x8"));
                dataGridView1.Rows.Add("0x00", "Unknown", node.unknown0x00.ToString("x8"));
                dataGridView1.Rows.Add("0x04", "Flags", node.flags0x04.ToString("x8"));
                dataGridView1.Rows.Add("0x08", "Child Offset", node.childOffset0x08.ToString("x8"));
                dataGridView1.Rows.Add("0x0C", "Next Offset", node.nextOffset0x0C.ToString("x8"));
                dataGridView1.Rows.Add("0x10", "JointDataNode Offset", node.jointDataNodeOffset0x10.ToString("x8"));
                dataGridView1.Rows.Add("0x14", "X Rotation", node.rotationX0x14);
                dataGridView1.Rows.Add("0x18", "Y Rotation", node.rotationY0x18);
                dataGridView1.Rows.Add("0x1C", "Z Rotation", node.rotationZ0x1C);
                dataGridView1.Rows.Add("0x20", "X Scale", node.scaleX0x20);
                dataGridView1.Rows.Add("0x24", "Y Scale", node.scaleY0x24);
                dataGridView1.Rows.Add("0x28", "Z Scale", node.scaleZ0x28);
                dataGridView1.Rows.Add("0x2C", "X Translation", node.translationX0x2C);
                dataGridView1.Rows.Add("0x30", "Y Translation", node.translationY0x30);
                dataGridView1.Rows.Add("0x34", "Z Translation", node.translationZ0x34);
                dataGridView1.Rows.Add("0x38", "Transform Offset", node.transformOffset0x38.ToString("x8"));
                dataGridView1.Rows.Add("0x3C", "Unknown", node.unknown0x3C.ToString("x8"));
                dataGridView1.Tag = node;
            }

            else if (nodesTreeView.SelectedNode.Text == "JointDataNode")
            {
                JointDataNode node = (JointDataNode) nodesTreeView.SelectedNode.Tag;
                dataGridView1.Rows.Add("", "Location", node.location.ToString("x8"));
                dataGridView1.Rows.Add("0x00", "Unknown", node.unknown0x0.ToString("x8"));
                dataGridView1.Rows.Add("0x04", "Next Offset", node.nextOffset0x4.ToString("x8"));
                dataGridView1.Rows.Add("0x08", "Material Node Offset", node.materialNodeOffset0x8.ToString("x8"));
                dataGridView1.Rows.Add("0x0C", "Mesh Node Offset", node.meshNodeOffset0xC.ToString("x8"));
                dataGridView1.Tag = node;
            }

            else if (nodesTreeView.SelectedNode.Text == "MaterialNode")
            {
                MaterialNode node = (MaterialNode) nodesTreeView.SelectedNode.Tag;
                dataGridView1.Rows.Add("", "Location", node.location.ToString("x8"));
                dataGridView1.Rows.Add("0x00", "Unknown", node.unknown0x00.ToString("x8"));
                dataGridView1.Rows.Add("0x04", "Unknown Flags", node.unknownFlags0x04.ToString("x8"));
                dataGridView1.Rows.Add("0x08", "Texture Node Offset", node.TextureNodeOffset0x08.ToString("x8"));
                dataGridView1.Rows.Add("0x0C", "Material Color Node Offset",
                                       node.MaterialColorNodeOffset0x0C.ToString("x8"));
                dataGridView1.Rows.Add("0x10", "Unknown", node.unknown0x10.ToString("x8"));
                dataGridView1.Rows.Add("0x14", "Unknown", node.unknown0x14.ToString("x8"));
            }

            else if (nodesTreeView.SelectedNode.Text == "MaterialColorNode")
            {
                MaterialColorNode node = (MaterialColorNode) nodesTreeView.SelectedNode.Tag;
                dataGridView1.Rows.Add("", "Location", node.location.ToString("x8"));
                dataGridView1.Rows.Add("0x00", "Color", node.unknownColor0x00.ToString("x8"));
                dataGridView1.Rows.Add("0x04", "Color", node.unknownColor0x04.ToString("x8"));
                dataGridView1.Rows.Add("0x08", "Color", node.unknownColor0x08.ToString("x8"));
                dataGridView1.Rows.Add("0x0C", "Unknown", node.unknown0x0C);
                dataGridView1.Rows.Add("0x10", "Unknown", node.unknown0x10);
            }

            else if (nodesTreeView.SelectedNode.Text == "TextureNode")
            {
                TextureNode node = (TextureNode) nodesTreeView.SelectedNode.Tag;
                dataGridView1.Rows.Add("", "Location", node.location.ToString("x8"));
                dataGridView1.Rows.Add("0x00", "Unknown", node.unknown0x00.ToString("x8"));
                dataGridView1.Rows.Add("0x4C", "Image Header Offset", node.imageHeaderOffset0x4C.ToString("x8"));
                dataGridView1.Rows.Add("0x50", "Palette Header Offset", node.paletteHeaderOffset0x50.ToString("x8"));
                dataGridView1.Rows.Add("0x54", "Unknown", node.unknown0x54.ToString("x8"));
                dataGridView1.Rows.Add("0x58", "Unknown Offset", node.unknownOffset0x58.ToString("x8"));
            }

            else if (nodesTreeView.SelectedNode.Text == "ImageHeader")
            {
                ImageHeader header = (ImageHeader) nodesTreeView.SelectedNode.Tag;
                dataGridView1.Rows.Add("", "Location", header.location.ToString("x8"));
                dataGridView1.Rows.Add("0x00", "Image Offset", header.imageOffset0x0.ToString("x8"));
                dataGridView1.Rows.Add("0x04", "Height", header.height0x6.ToString("x4"));
                dataGridView1.Rows.Add("0x06", "Width", header.width0x4.ToString("x4"));
                dataGridView1.Rows.Add("0x08", "Image Format", header.imageFormatString);

            }

            else if (nodesTreeView.SelectedNode.Text == "PaletteHeader")
            {
                PaletteHeader header = (PaletteHeader) nodesTreeView.SelectedNode.Tag;
                dataGridView1.Rows.Add("", "Location", header.location.ToString("x8"));
                dataGridView1.Rows.Add("0x00", "Palette Offset", header.paletteOffset0x0.ToString("x8"));
                dataGridView1.Rows.Add("0x04", "Palette Format", header.paletteFormatString);
                dataGridView1.Rows.Add("0x08", "Unknown", header.unknown0x08.ToString("x8"));
                dataGridView1.Rows.Add("0x0C", "Color Count", header.colorCount0xA.ToString("x4"));
                dataGridView1.Rows.Add("0x0E", "Unknown", header.unknown0xA.ToString("x4"));
            }

            else if (nodesTreeView.SelectedNode.Text == "FighterDataNode")
            {
                FighterDataNode header = (FighterDataNode)nodesTreeView.SelectedNode.Tag;
                dataGridView1.Rows.Add("", "Location", header.location.ToString("x8"));
                dataGridView1.Rows.Add("0x00", "Unknown Offset", header.unknownOffset0x00.ToString("x8"));
                dataGridView1.Rows.Add("0x04", "Unknown Offset", header.unknownOffset0x04.ToString("x8"));
                dataGridView1.Rows.Add("0x08", "Unknown Offset", header.unknownOffset0x08.ToString("x8"));
                dataGridView1.Rows.Add("0x0C", "Unknown Offset", header.unknownOffset0x0C.ToString("x8"));
                dataGridView1.Rows.Add("0x10", "Unknown Offset", header.unknownOffset0x10.ToString("x8"));
                dataGridView1.Rows.Add("0x14", "Unknown Offset", header.unknownOffset0x14.ToString("x8"));
                dataGridView1.Rows.Add("0x18", "Unknown Offset", header.unknownOffset0x18.ToString("x8"));
                dataGridView1.Rows.Add("0x1C", "Unknown Offset", header.unknownOffset0x1C.ToString("x8"));
                dataGridView1.Rows.Add("0x20", "Unknown Offset", header.unknownOffset0x20.ToString("x8"));
                dataGridView1.Rows.Add("0x24", "Unknown Offset", header.unknownOffset0x24.ToString("x8"));
                dataGridView1.Rows.Add("0x28", "Unknown Offset", header.unknownOffset0x28.ToString("x8"));
                dataGridView1.Rows.Add("0x2C", "Unknown Offset", header.unknownOffset0x2C.ToString("x8"));
                dataGridView1.Rows.Add("0x30", "Unknown Offset", header.unknownOffset0x30.ToString("x8"));
                dataGridView1.Rows.Add("0x34", "Unknown Offset", header.unknownOffset0x34.ToString("x8"));
                dataGridView1.Rows.Add("0x38", "Unknown Offset", header.unknownOffset0x38.ToString("x8"));
                dataGridView1.Rows.Add("0x3C", "Unknown Offset", header.unknownOffset0x3C.ToString("x8"));
                dataGridView1.Rows.Add("0x40", "Unknown Offset", header.unknownOffset0x40.ToString("x8"));
                dataGridView1.Rows.Add("0x44", "Unknown Offset", header.unknownOffset0x44.ToString("x8"));
                dataGridView1.Rows.Add("0x48", "Unknown Offset", header.unknownOffset0x48.ToString("x8"));
                dataGridView1.Rows.Add("0x4C", "Unknown Offset", header.unknownOffset0x4C.ToString("x8"));
                dataGridView1.Rows.Add("0x50", "Unknown Offset", header.unknownOffset0x50.ToString("x8"));
                dataGridView1.Rows.Add("0x54", "Unknown Offset", header.unknownOffset0x54.ToString("x8"));
                dataGridView1.Rows.Add("0x58", "Unknown Offset", header.unknownOffset0x58.ToString("x8"));
                dataGridView1.Rows.Add("0x5C", "Unknown Offset", header.unknownOffset0x5C.ToString("x8"));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextureListObject tex = (TextureListObject) textureListBox.SelectedItem;
            if (tex != null)
            {
                buttonExportTexture.Enabled = true;
                buttonReplaceTexture.Enabled = true;
            }
            else
            {
                buttonExportTexture.Enabled = false;
                buttonReplaceTexture.Enabled = false;
            }
            texturePictureBox.Image = tex.imageBitmap;
            String colors = "";
            if (tex.paletteHeader.colorCount0xA != 0) colors = tex.paletteHeader.colorCount0xA.ToString();
            textureInfoLabel.Text = "Size: " + tex.imageHeader.width0x4.ToString() + " x " + tex.imageHeader.height0x6.ToString() + "\nFormat: " + tex.imageHeader.imageFormatString + "\nColors: " + colors;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = textureListBox.SelectedItem.ToString();
            Image image = texturePictureBox.Image;
            saveTextureDialog.InitialDirectory = Settings.Default.saveTexturePath;
            saveTextureDialog.FileName = fileName;

            if (saveTextureDialog.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.saveTexturePath = Path.GetDirectoryName(saveTextureDialog.FileName);
                DatFile.ExportImage(image, saveTextureDialog.FileName);
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            openTextureDialog.InitialDirectory = Settings.Default.openTexturePath;
            openTextureDialog.FileName = "";

            if (openTextureDialog.ShowDialog() != DialogResult.OK) return;


            Stream myStream = null;
            if ((myStream = openTextureDialog.OpenFile()) == null) return;
            var info = new FileInfo(openTextureDialog.FileName);
            Settings.Default.openTexturePath = Path.GetDirectoryName(info.FullName);

            Image newImage = new Bitmap(myStream);
            TextureListObject currentTexture = (TextureListObject) textureListBox.SelectedItem;
            int selectedTextureIndex = textureListBox.SelectedIndex;
            DatFile.ReplaceTexture(newImage, currentTexture);
            textureListBox.SetSelected(selectedTextureIndex, true);
            textureListBox.Focus();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveDatFileDialog.InitialDirectory = Settings.Default.saveDatFilePath;
            saveDatFileDialog.FileName = "";
            if (saveDatFileDialog.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.saveDatFilePath = Path.GetDirectoryName(saveDatFileDialog.FileName);
                DatFile.SaveDatFile(saveDatFileDialog);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string datFileName = DatFile.fileName;
            IsoFileInfo fileInfo = FileHandler.SearchIso(datFileName, filesystemTreeView.Nodes[0]);
            if (fileInfo == null)
                MessageBox.Show("Error finding file in disc image!");
            else
            {
                FileHandler.ReplaceFileInIso(fileInfo, DatFile.file);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            openDatFileDialog.InitialDirectory = Settings.Default.openDatFilePath;
            openDatFileDialog.FileName = "";

            if (openDatFileDialog.ShowDialog() == DialogResult.OK)
            {
                var info = new FileInfo(openDatFileDialog.FileName);
                Settings.Default.openDatFilePath = Path.GetDirectoryName(info.FullName);
                OpenDatFileAndUpdateWindow(DatFile.OpenDatFile(new FileStream(openDatFileDialog.FileName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite)), info.Name);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openDatFileDialog.InitialDirectory = Settings.Default.openDatFilePath;
            openDatFileDialog.FileName = "";

            if (openDatFileDialog.ShowDialog() == DialogResult.OK)
            {
                var info = new FileInfo(openDatFileDialog.FileName);
                Settings.Default.openDatFilePath = Path.GetDirectoryName(info.FullName);
                var fileInfo = (IsoFileInfo) filesystemTreeView.SelectedNode.Tag;
                FileHandler.ReplaceFileInIso(fileInfo, info.FullName);
            }
        }



    }
}
