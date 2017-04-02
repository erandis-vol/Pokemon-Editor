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
            // Get ROM filename
            string filename = string.Empty;
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Open ROM";
                dialog.Filter = "GBA ROMs|*.gba";
                dialog.FileName = "";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                filename = dialog.FileName;
            }

            // Try to load the ROM
            try
            {
                OpenROM(filename);
                LoadFirst();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load ROM:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set label
            lblROM.Text = $"ROM: {rom.Name}\nCode: {rom.Code}\nPokémon: {pokemonCount}";

            // Populate Pokemon list
            listPokemon.Items.Clear();
            for (int i = 0; i < names.Length; i++)
            {
                var ix = new ListViewItem($"{i + 1:X3}");
                ix.SubItems.Add(names[i]);
                listPokemon.Items.Add(ix);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom == null)
                return;


        }
    }
}
