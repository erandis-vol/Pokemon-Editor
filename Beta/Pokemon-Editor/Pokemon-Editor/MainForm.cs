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
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                romInfo = Settings.FromFile("ROMs.ini", "ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
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
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rom == null) return;

            var pokemonIndex = listBox1.SelectedIndex;

            tBaseHealth.Value = pokemon[pokemonIndex].HP;
            tBaseAttack.Value = pokemon[pokemonIndex].Attack;
            tBaseDefense.Value = pokemon[pokemonIndex].Defense;
            tBaseSpecialAttack.Value = pokemon[pokemonIndex].SpecialAttack;
            tBaseSpecialDefense.Value = pokemon[pokemonIndex].SpecialDefense;
            tBaseSpeed.Value = pokemon[pokemonIndex].Speed;

            tBaseHealth2.Value = pokemon[pokemonIndex].EffortYield & 3;
            tBaseAttack2.Value = pokemon[pokemonIndex].EffortYield >> 2 & 3;
            tBaseDefense2.Value = pokemon[pokemonIndex].EffortYield >> 4 & 3;
            tBaseSpecialAttack2.Value = pokemon[pokemonIndex].EffortYield >> 6 & 3;
            tBaseSpecialDefense2.Value = pokemon[pokemonIndex].EffortYield >> 8 & 3;
            tBaseSpeed2.Value = pokemon[pokemonIndex].EffortYield >> 10 & 3;
        }
    }
}
