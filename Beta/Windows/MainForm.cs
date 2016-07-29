using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hopeless
{
    public partial class MainForm : Form
    {
        ROM rom;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // open the ROM
                rom = new ROM("bpre.gba");

                // load bulbasaur's frontsprite
                rom.Seek(0xD2FBD4);
                var frontSprite = rom.ReadCompressedBytes();

                // load bulbasaur's palette
                rom.Seek(0xD2FE78);
                var regularPalette = rom.ReadCompressedPalette();

                // draw the palette


                // todo: draw
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
