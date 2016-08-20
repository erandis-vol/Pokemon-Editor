using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost.GBA
{
    struct PokedexEntry
    {
        public string Species;  // 12 bytes
        public ushort Height, Weight;
        public PokedexPage Page1, Page2;
        public ushort Padding;
        public ushort PokemonScale, PokemonOffset;
        public ushort TrainerScale, TrainerOffset;
        public ushort Padding2;
    }

    struct PokedexPage
    {
        public int Offset;
        public string Text;
    }
}
