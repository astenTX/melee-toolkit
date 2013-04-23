using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

                IsoFile.OpenFile(info.FullName);
                label7.Text = IsoFile.GetTitle();
                label6.Text = IsoFile.GetVersion();
                filesystemTreeView.Nodes.Clear();
                filesystemTreeView.BeginUpdate();
                filesystemTreeView.Nodes.Add(IsoFile.root);
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
            if (IsoFile.isFileLoaded) button4.Enabled = true;
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
                IsoFile.ExportFileFromIso(info, saveFileDialog1);
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
            OpenDatFileAndUpdateWindow(IsoFile.OpenDatFile(info), info.Name);
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dataGridView1.Rows.Clear();

            DatNode datNode = (DatNode) nodesTreeView.SelectedNode.Tag;
            dataGridView1.Tag = datNode;
            dataGridView1.Rows.Add("", "Node Location", datNode.location.ToString("x8"));
            dataGridView1.Rows[0].ReadOnly = true;
            foreach (Values value in datNode.values)
            {
                switch (value.type)
                {
                    case Values.Types.UInt32:
                        dataGridView1.Rows.Add("0x" + value.offset.ToString("x2"), value.name, ((UInt32)value.value).ToString("x8"));
                        break;
                    case Values.Types.UInt16:
                        dataGridView1.Rows.Add("0x" + value.offset.ToString("x2"), value.name, ((UInt16)value.value).ToString("x4"));
                        break;
                    case Values.Types.Float:
                        dataGridView1.Rows.Add("0x" + value.offset.ToString("x2"), value.name, ((float)value.value).ToString());
                        break;
                }
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = value;
                if (!value.isEditable)
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].ReadOnly = true;
                }
                else
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Style.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
                }
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
            if (tex.paletteHeader != null) colors = tex.paletteHeader.colorCount0xC.ToString();
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
            IsoFileInfo fileInfo = IsoFile.SearchIso(datFileName, filesystemTreeView.Nodes[0]);
            if (fileInfo == null)
                MessageBox.Show("Error finding file in disc image!");
            else
            {
                IsoFile.ReplaceFileInIso(fileInfo, DatFile.file);
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
                IsoFile.ReplaceFileInIso(fileInfo, info.FullName);
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Values value = (Values) dataGridView1.CurrentRow.Tag;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Values value = (Values)dataGridView1.CurrentRow.Tag;
            int fileOffset = value.fileLocation + value.offset;
            byte[] newData;
            switch (value.type)
            {
                case Values.Types.UInt32:
                    UInt32 oldValue32 = (UInt32)value.value;
                    UInt32 newValue32;
                    try { newValue32 = UInt32.Parse(dataGridView1.CurrentCell.Value.ToString(), NumberStyles.HexNumber); }
                    catch (Exception f)
                    {
                        if (f is FormatException || f is OverflowException)
                        {
                            MessageBox.Show("Please enter a 32-bit hexadecimal number!");
                            dataGridView1.CurrentCell.Value = oldValue32.ToString("x8");
                            return;
                        }
                        else throw;

                    }
                    if (oldValue32 == newValue32) return;
                    value.value = newValue32;
                    newData = BitConverter.GetBytes(newValue32);
                    break;
                case Values.Types.UInt16:
                    UInt16 oldValue16 = (UInt16)value.value;
                    UInt16 newValue16;
                    try { newValue16 = UInt16.Parse(dataGridView1.CurrentCell.Value.ToString(), NumberStyles.HexNumber); }
                    catch (Exception f)
                    {
                        if (f is FormatException || f is OverflowException)
                        {
                            MessageBox.Show("Please enter a 16-bit hexadecimal number!");
                            dataGridView1.CurrentCell.Value = oldValue16.ToString("x4");
                            return;
                        }
                        else throw;

                    }
                    if (oldValue16 == newValue16) return;
                    value.value = newValue16;
                    newData = BitConverter.GetBytes(newValue16);
                    break;
                case Values.Types.Float:
                    float oldValueFloat = (float)value.value;
                    float newValueFloat;
                    try { newValueFloat = float.Parse(dataGridView1.CurrentCell.Value.ToString()); }
                    catch (FormatException f)
                    {
                        MessageBox.Show("Please enter a decimal number!");
                        dataGridView1.CurrentCell.Value = oldValueFloat;
                        return;
                    }
                    
                    if (oldValueFloat == newValueFloat) return;
                    value.value = newValueFloat;
                    newData = BitConverter.GetBytes(newValueFloat);
                    break;

                default:
                    return;
            }
            if (BitConverter.IsLittleEndian)
                Array.Reverse(newData);
            DatFile.UpdateValue(newData, fileOffset);
        }



    }
}
