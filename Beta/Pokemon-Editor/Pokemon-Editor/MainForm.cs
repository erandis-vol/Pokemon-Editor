using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lost.GBA;

namespace Lost
{
    public partial class MainForm : Form
    {
        bool ignore = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            /*try
            {
                romInfo = Settings.FromFile("ROMs.ini", "ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }*/
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            rom?.Dispose();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open ROM";
            openFileDialog1.Filter = "GBA ROMs|*.gba";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            // try to open the ROM
            if (!OpenROM(openFileDialog1.FileName))
                return;

            // load everything
            LoadAll();

            // display stuff
            listBox1.Items.Clear();
            listBox1.Items.AddRange(names);

            cBaseType.Items.Clear();
            cBaseType.Items.AddRange(types);

            cBaseType2.Items.Clear();
            cBaseType2.Items.AddRange(types);

            cBaseAbility.Items.Clear();
            cBaseAbility.Items.AddRange(abilities);

            cBaseAbility2.Items.Clear();
            cBaseAbility2.Items.AddRange(abilities);

            cBaseItem.Items.Clear();
            cBaseItem.Items.AddRange(items);

            cBaseItem2.Items.Clear();
            cBaseItem2.Items.AddRange(items);

            DisplayBlankPokemon();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rom == null) return;

            var pokemonIndex = listBox1.SelectedIndex;
            DisplayPokemon(pokemonIndex);
        }
    }
}
