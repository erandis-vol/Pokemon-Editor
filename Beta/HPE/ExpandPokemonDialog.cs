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
    public partial class ExpandPokemonDialog : Form
    {
        private int originalPokemon;
        private int pokemon;
        private bool doit;
        private bool mc = false;

        public ExpandPokemonDialog(int numberPokemon)
        {
            InitializeComponent();
            pokemon = originalPokemon = numberPokemon;
            doit = false;
        }

        private void ExpandPokemonDialog_Load(object sender, EventArgs e)
        {
            mc = true;
            txtPkmnAdd.Value = 0;
            txtPkmnTtl.Value = (uint)originalPokemon;
            txtTotalTotal.Value = (uint)(originalPokemon + 25 + 28);
            mc = false;
        }

        private void ExpandPokemonDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (doit) DialogResult = DialogResult.OK;
            else DialogResult = DialogResult.Cancel;
        }

        private void bExpand_Click(object sender, EventArgs e)
        {
            doit = true;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtPkmnAdd_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            pokemon = originalPokemon + (int)txtPkmnAdd.Value;

            mc = true;
            txtPkmnTtl.Value = (uint)(pokemon);
            txtTotalTotal.Value = (uint)(pokemon + 25 + 28);
            mc = false;
        }

        private void txtPkmnTtl_TextChanged(object sender, EventArgs e)
        {
            if (mc || txtPkmnTtl.Value < originalPokemon) return;

            pokemon = (int)txtPkmnTtl.Value;

            mc = true;
            txtPkmnAdd.Value = (uint)(pokemon - originalPokemon);
            txtTotalTotal.Value = (uint)(pokemon + 25 + 28);
            mc = false;
        }

        /// <summary>
        /// The new amuount of Pokémon to expand to.
        /// Includes the 28 Bad Egg/Unown.
        /// </summary>
        public int NewPokemonCount
        {
            get { return pokemon; }
        }

        /*/// <summary>
        /// The new number Pokédex entries to expand to.
        /// The remaining entries should point to entry 0.
        /// </summary>
        public int NewPokedexEntryCount
        {
            get { return pokemon; }
        }*/
    }
}
