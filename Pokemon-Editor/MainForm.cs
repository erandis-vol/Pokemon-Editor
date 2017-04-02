using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GBAHL;
using GBAHL.IO;
using GBAHL.Text;

namespace Lost
{
    public partial class MainForm : Form
    {
        private ROM rom;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            rom?.Dispose();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Open ROM";
                dialog.Filter = "GBA ROMs|*.gba";
                dialog.FileName = "";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom == null)
                return;


        }
    }
}
