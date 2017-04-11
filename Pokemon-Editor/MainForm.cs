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
    public partial class s : Form
    {
        private ROM rom;
        private ListViewItem selection;

        public s()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClearAll();
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
                ix.Tag = i;
                listPokemon.Items.Add(ix);
            }

            cBaseStatsType1.Items.Clear();
            cBaseStatsType1.Items.AddRange(types);

            cBaseStatsType2.Items.Clear();
            cBaseStatsType2.Items.AddRange(types);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom == null || selection == null)
                return;

            int index = (int)selection.Tag;
            SaveBaseStats(index);
        }

        private void listPokemon_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Try to get the selected Pokemon
            ListViewItem tempSelection = null;
            foreach (ListViewItem item in listPokemon.SelectedItems)
                tempSelection = item;

            if (tempSelection == null)
                return;

            // All worked, set selection
            selection = tempSelection;

            // Load the Pokemon
            try
            {
                int index = (int)tempSelection.Tag;
                LoadBaseStats(index);
                LoadEvolutions(index);
                LoadMoveset(index);

                DisplayAll();
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine("ERROR: {0}\n\n{1}", ex.Message, ex.StackTrace);
#endif

                // Loading failed, undo
                ClearAll();
                selection = null;
                return;
            }
        }

        private void listPokemon_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // TODO: We could let the user sort the columns
        }

        private void DisplayAll()
        {
            tName.Text = names[(int)selection.Tag];

            // Base stats
            tBaseStatsHP.Value = baseStats.HP;
            tBaseStatsAtk.Value = baseStats.Attack;
            tBaseStatsDef.Value = baseStats.Defense;
            tBaseStatsSpAtk.Value = baseStats.SpecialAttack;
            tBaseStatsSpDef.Value = baseStats.SpecialDefense;
            tBaseStatsSpd.Value = baseStats.Speed;

            tBaseStatsHP2.Value = (baseStats.EffortYield & 3);
            tBaseStatsAtk2.Value = (baseStats.EffortYield >> 2) & 3;
            tBaseStatsDef2.Value = (baseStats.EffortYield >> 4) & 3;
            tBaseStatsSpAtk2.Value = (baseStats.EffortYield >> 8) & 3;
            tBaseStatsSpDef2.Value = (baseStats.EffortYield >> 10) & 3;
            tBaseStatsSpd2.Value = (baseStats.EffortYield >> 6) & 3;

            cBaseStatsType1.SelectedIndex = baseStats.Type;
            cBaseStatsType2.SelectedIndex = baseStats.Type2;

            pBaseStatsVisualizer.Invalidate();

            // Evolutions
            // Moveset
        }

        private void ClearAll()
        {
            tName.Text = "";

            // Base stats
            tBaseStatsHP.Value = 0;
            tBaseStatsAtk.Value = 0;
            tBaseStatsDef.Value = 0;
            tBaseStatsSpAtk.Value = 0;
            tBaseStatsSpDef.Value = 0;
            tBaseStatsSpd.Value = 0;
            tBaseStatsHP2.Value = 0;
            tBaseStatsAtk2.Value = 0;
            tBaseStatsDef2.Value = 0;
            tBaseStatsSpAtk2.Value = 0;
            tBaseStatsSpDef2.Value = 0;
            tBaseStatsSpd2.Value = 0;
            cBaseStatsType1.SelectedIndex = -1;
            cBaseStatsType2.SelectedIndex = -1;
        }

        private void pBaseStatsVisualizer_Paint(object sender, PaintEventArgs e)
        {
            // One possible visualizer, bars:
            /*
            // Visualize the base stats
            int height = tBaseStatsHP.Height - 1;
            int width = pBaseStatsVisualizer.Width;
            int y = 0;

            // Helper method to calculate area to fill
            int Fill(int x) => width * x / 255;

            // Fill all boxes
            y = tBaseStatsHP.Location.Y - pBaseStatsVisualizer.Location.Y;
            e.Graphics.FillRectangle(SystemBrushes.ControlLight, 0, y, Fill(tBaseStatsHP.Value), height);
            e.Graphics.DrawRectangle(SystemPens.WindowFrame, -1, y, width, height);

            y = tBaseStatsAtk.Location.Y - pBaseStatsVisualizer.Location.Y;
            e.Graphics.FillRectangle(SystemBrushes.ControlLight, 0, y, Fill(tBaseStatsAtk.Value), height);
            e.Graphics.DrawRectangle(SystemPens.WindowFrame, -1, y, width, height);

            y = tBaseStatsDef.Location.Y - pBaseStatsVisualizer.Location.Y;
            e.Graphics.FillRectangle(SystemBrushes.ControlLight, 0, y, Fill(tBaseStatsDef.Value), height);
            e.Graphics.DrawRectangle(SystemPens.WindowFrame, -1, y, width, height);

            y = tBaseStatsSpAtk.Location.Y - pBaseStatsVisualizer.Location.Y;
            e.Graphics.FillRectangle(SystemBrushes.ControlLight, 0, y, Fill(tBaseStatsSpAtk.Value), height);
            e.Graphics.DrawRectangle(SystemPens.WindowFrame, -1, y, width, height);

            y = tBaseStatsSpDef.Location.Y - pBaseStatsVisualizer.Location.Y;
            e.Graphics.FillRectangle(SystemBrushes.ControlLight, 0, y, Fill(tBaseStatsSpDef.Value), height);
            e.Graphics.DrawRectangle(SystemPens.WindowFrame, -1, y, width, height);

            y = tBaseStatsSpd.Location.Y - pBaseStatsVisualizer.Location.Y;
            e.Graphics.FillRectangle(SystemBrushes.ControlLight, 0, y, Fill(tBaseStatsSpd.Value), height);
            e.Graphics.DrawRectangle(SystemPens.WindowFrame, -1, y, width, height);
            */

            // Hexagon:
            PointF origin = new PointF(52, 50);
            int radius = 50;

            // Returns the scaled radius of the given value
            int Radius(int x) => radius * x / 255;

            // Returns the coordinate of a regular hexagon vertex where the first point is (0, r)
            PointF GetVertex(int i, int r)
            {
                return new PointF(
                    (float)(origin.X + r * Math.Cos(2 * Math.PI * i / 6 + (Math.PI / 6))),
                    (float)(origin.Y + r * Math.Sin(2 * Math.PI * i / 6 + (Math.PI / 6)))
                );
            }

            // Calcualte outer vertexes
            PointF[] vertexes = new PointF[6];
            for (int i = 0; i < 6; i++)
                vertexes[i] = GetVertex(i, radius);

            // Calculate vertexes for individual stat values
            PointF hp = GetVertex(4, Radius(tBaseStatsHP.Value));
            PointF atk = GetVertex(5, Radius(tBaseStatsAtk.Value));
            PointF def = GetVertex(6, Radius(tBaseStatsDef.Value));
            PointF spAtk = GetVertex(1, Radius(tBaseStatsSpAtk.Value));
            PointF spDef = GetVertex(2, Radius(tBaseStatsSpDef.Value));
            PointF spd = GetVertex(3, Radius(tBaseStatsSpd.Value));

            // Draw max outline outer vertexes
            e.Graphics.DrawPolygon(SystemPens.WindowFrame, vertexes);
            for (int i = 0; i < 6; i++)
                e.Graphics.DrawLine(SystemPens.WindowFrame, origin, vertexes[i]);

            // Draw filled stat blob
            e.Graphics.FillPolygon(SystemBrushes.Highlight, new[] { origin, hp, atk });
            e.Graphics.FillPolygon(SystemBrushes.Highlight, new[] { origin, atk, def });
            e.Graphics.FillPolygon(SystemBrushes.Highlight, new[] { origin, def, spAtk });
            e.Graphics.FillPolygon(SystemBrushes.Highlight, new[] { origin, spAtk, spDef });
            e.Graphics.FillPolygon(SystemBrushes.Highlight, new[] { origin, spDef, spd });
            e.Graphics.FillPolygon(SystemBrushes.Highlight, new[] { origin, spd, hp });
        }
    }
}
