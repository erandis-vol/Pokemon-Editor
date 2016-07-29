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
    public partial class MoveTutorDialog : Form
    {
        private ROM rom;
        private uint dataOffset;
        private ushort[] attacks;
        private string[] attackNames;

        private int selected = -1;

        private bool mc = false;

        public MoveTutorDialog(ROM rom, uint offset, int count, string[] attackNames)
        {
            InitializeComponent();

            this.rom = rom;
            this.dataOffset = offset;
            this.attacks = new ushort[count];
            for (int i = 0; i < count; i++) this.attacks[i] = 0;
            this.attackNames = attackNames;
        }

        private void MoveTutorDialog_Load(object sender, EventArgs e)
        {
            // Load
            LoadMTList();

            // And display
            //listMoveTutor.Items.Clear();
            for (int i = 0; i < attacks.Length; i++)
            {
                //var item = new ListViewItem(attackNames[attacks[i]]);
                listMoveTutor.Items.Add(new ListViewItem(attackNames[attacks[i]]));
            }

            // ~~~
            //cAtkAtk.Items.Clear();
            cAtkAtk.Items.AddRange(attackNames);
        }

        private void LoadMTList()
        {
            using (GBABinaryReader br = new GBABinaryReader(rom))
            {
                // :D
                br.BaseStream.Seek(dataOffset, SeekOrigin.Begin);
                for (int i = 0; i < attacks.Length; i++)
                {
                    attacks[i] = br.ReadUInt16();
                }
            }
        }

        private void SaveMTList()
        {
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                bw.BaseStream.Seek(dataOffset, SeekOrigin.Begin);
                for (int i = 0; i < attacks.Length; i++)
                {
                    bw.Write(attacks[i]);
                }
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            SaveMTList();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listMoveTutor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            // Get attack #
            int id = -1;
            foreach (int x in listMoveTutor.SelectedIndices) id = x;
            if (id < 0) return;
            selected = id;

            // Show it in the combobox
            mc = true;
            cAtkAtk.SelectedIndex = attacks[selected];
            mc = false;
        }

        private void cAtkAtk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selected > -1 && !mc)
            {
                attacks[selected] = (ushort)cAtkAtk.SelectedIndex;

                mc = true;
                listMoveTutor.Items[selected].Text = attackNames[attacks[selected]];
                mc = false;
            }
        }
    }
}
