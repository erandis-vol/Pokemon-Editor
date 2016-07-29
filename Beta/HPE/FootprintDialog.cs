using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HPE
{
    public partial class FootprintDialog : Form
    {
        private ROM rom;
        private uint tableStart;
        private int tableIndex;
        private byte[] buffer;

        public FootprintDialog(ROM rom, int footprint, uint table)
        {
            InitializeComponent();

            this.rom = rom;
            this.tableStart = table;
            this.tableIndex = footprint;
            this.buffer = null;
        }

        private void FootprintDialog_Load(object sender, EventArgs e)
        {
            LoadFootprint();
        }

        private void FootprintDialog_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void bSave_Click(object sender, EventArgs e)
        {
            if (buffer == null) return;
            SaveFootprint();
        }

        private void bRepoint_Click(object sender, EventArgs e)
        {
            if (buffer == null) return;
            RepointFootprint();
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            if (buffer == null) return;

            save.FileName = "";
            save.Filter = "Bitmaps|*.bmp";
            save.Title = "Export Footprint";
            save.InitialDirectory = Path.GetDirectoryName(rom.File);
            if (save.ShowDialog() == DialogResult.OK)
            {
                SaveBitmap(save.FileName);
            }
        }

        private void bImport_Click(object sender, EventArgs e)
        {
            if (buffer == null) return;

            open.FileName = "";
            open.Filter = "Image Files|*.bmp;*.png;*.gif";
            open.Title = "Import Footprint";
            open.InitialDirectory = Path.GetDirectoryName(rom.File);
            if (open.ShowDialog() == DialogResult.OK)
            {
                OpenBitmap(open.FileName);
                pFoot.Invalidate();
            }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pFoot_Paint(object sender, PaintEventArgs e)
        {
            if (buffer != null)
            {
                int num1 = 0, num2 = 0, num3 = 0;
                for (int b = 0; b < 32; b++)
                {
                    for (int bit = 0; bit < 8; bit++)
                    {
                        if (((buffer[b] >> bit) & 1) == 1)
                        {
                            e.Graphics.FillRectangle(Brushes.Black, bit * 16 + num1 * 128, num2 * 16 + num3 * 128, 16, 16);
                        }
                    }

                    if (num2 < 7)
                    {
                        num2++;
                    }
                    else if (num1 == 1)
                    {
                        num2 = 0;
                        num1 = 0;
                        num3++;
                    }
                    else
                    {
                        num2 = 0;
                        num1++;
                    }
                }
            }

            // Draw Grid
            for (int i = 1; i < 16; i++)
            {
                e.Graphics.DrawLine(Pens.Pink, i * 16, 0, i * 16, 256);
                e.Graphics.DrawLine(Pens.Pink, 0, i * 16, 256, i * 16);
            }
        }

        private void pFoot_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / 16, y = e.Y / 16;
            int blockX = x / 8, blockY = y / 8;
            int pixelX = x % 8, pixelY = y % 8;
            if (x < 0 || y < 0 || x > 15 || y > 15) return;

            int index = pixelY + blockX * 8 + blockY * 16;
            if (e.Button == MouseButtons.Left)
            {
                buffer[index] = (byte)(buffer[index] | (1 << pixelX));
                pFoot.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                buffer[index] = (byte)(buffer[index] & ~(1 << pixelX));
                pFoot.Invalidate();
            }
        }

        private void LoadFootprint()
        {
            // Not really much to do here...
            using (GBABinaryReader br = new GBABinaryReader(rom))
            {
                // Read the location from the table
                br.BaseStream.Seek(tableStart + tableIndex * 4, SeekOrigin.Begin);
                uint dataOffset = br.ReadPointer();

                // Read the data
                br.BaseStream.Seek(dataOffset, SeekOrigin.Begin);
                buffer = br.ReadBytes(32);
            }
        }

        private void SaveFootprint()
        {
            // Read the table offset
            uint dataOffset = 0;
            using (GBABinaryReader br = new GBABinaryReader(rom))
            {
                // Read the location from the table
                br.BaseStream.Seek(tableStart + tableIndex * 4, SeekOrigin.Begin);
                dataOffset = br.ReadPointer();
            }

            // And write the data
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                bw.BaseStream.Seek(dataOffset, SeekOrigin.Begin);
                bw.Write(buffer); // 32 bytes, baby~
            }
        }

        private void RepointFootprint()
        {
            // Get free space
            FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, 32);
            if (fsf.ShowDialog() != DialogResult.OK) return;

            // Write to that offset
            uint writeTo = fsf.FreeSpaceOffset;
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                // Adjust the table
                bw.BaseStream.Seek(tableStart + tableIndex * 4, SeekOrigin.Begin);
                bw.WritePointer(writeTo);

                // And copy the data itself
                bw.BaseStream.Seek(writeTo, SeekOrigin.Begin);
                bw.Write(buffer); // 32 bytes, baby~
            }
        }

        private void SaveBitmap(string file)
        {
            using (Bitmap bmp = new Bitmap(16, 16))
            {
                int num1 = 0, num2 = 0, num3 = 0;
                for (int b = 0; b < 32; b++)
                {
                    for (int bit = 0; bit < 8; bit++)
                    {
                        if (((buffer[b] >> bit) & 1) == 1)
                        {
                            //e.Graphics.FillRectangle(Brushes.Black, bit * 16 + num1 * 128, num2 * 16 + num3 * 128, 16, 16);
                            bmp.SetPixel(bit + num1 * 8, num2 + num3 * 8, Color.Black);
                        }
                        else
                        {
                            bmp.SetPixel(bit + num1 * 8, num2 + num3 * 8, Color.White);
                        }
                    }

                    if (num2 < 7)
                    {
                        num2++;
                    }
                    else if (num1 == 1)
                    {
                        num2 = 0;
                        num1 = 0;
                        num3++;
                    }
                    else
                    {
                        num2 = 0;
                        num1++;
                    }
                }

                bmp.Save(file);
            }
        }

        private void OpenBitmap(string file)
        {
            using (Bitmap bmp = new Bitmap(file))
            {
                // Dimension check~
                if (bmp.Width != 16 || bmp.Height != 16)
                {
                    MessageBox.Show("Image must be 16 x 16 pixels!", "Uh-oh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Let's go~
                // I should do this a better way, but I just don't feel like it.
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        // Get color in bmp
                        Color pixel = bmp.GetPixel(x, y);

                        // Match it up to the buffer array
                        int blockX = x / 8, blockY = y / 8;
                        int pixelX = x % 8, pixelY = y % 8;
                        int index = pixelY + blockX * 8 + blockY * 16;

                        // And set in data
                        if (pixel.R <= 8 && pixel.G <= 8 && pixel.B <= 8) // Shades of black. On.
                        {
                            buffer[index] = (byte)(buffer[index] | (1 << pixelX));
                        }
                        else // Off.
                        {
                            buffer[index] = (byte)(buffer[index] & ~(1 << pixelX));
                        }
                    }
                }
            }
        }
    }
}
