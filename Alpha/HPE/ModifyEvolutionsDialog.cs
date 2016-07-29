using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPE
{
    public partial class ModifyEvolutionsDialog : Form
    {
        private int oldCount, newCount;

        private bool ok = false;

        public ModifyEvolutionsDialog(int currentCount)
        {
            InitializeComponent();
            oldCount = newCount = currentCount;
        }

        private void ModifyEvolutionsDialog_Load(object sender, EventArgs e)
        {
            switch (oldCount)
            {
                case 4:
                    comboBox1.SelectedIndex = 0;
                    break;
                case 5:
                    comboBox1.SelectedIndex = 1;
                    break;
                case 8:
                    comboBox1.SelectedIndex = 2;
                    break;
                case 16:
                    comboBox1.SelectedIndex = 3;
                    break;
                case 32:
                    comboBox1.SelectedIndex = 4;
                    break;

                default:
                    comboBox1.SelectedIndex = 0;
                    break;
            }
        }

        private void ModifyEvolutionsDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ok && newCount != oldCount) this.DialogResult = DialogResult.OK;
            else this.DialogResult = DialogResult.Cancel;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    newCount = 4;
                    break;
                case 1:
                    newCount = 5;
                    break;
                case 2:
                    newCount = 8;
                    break;
                case 3:
                    newCount = 16;
                    break;
                case 4:
                    newCount = 32;
                    break;

                default:
                    newCount = 4;
                    break;
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            ok = true;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            ok = false;
            Close();
        }

        /// <summary>
        /// The number of new evolutions to add. :D
        /// </summary>
        public int NewEvolutionCount
        {
            get { return newCount; }
        }
    }
}
