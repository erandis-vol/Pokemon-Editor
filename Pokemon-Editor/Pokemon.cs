using System.Runtime.InteropServices;

namespace Lost
{
    // Structures for Pokémon data

    /// <summary>
    /// Represents a Pokémon's base stats.
    /// </summary>
    struct Pokemon
    {
        public byte HP;
        public byte Attack;
        public byte Defense;
        public byte Speed;
        public byte SpecialAttack;
        public byte SpecialDefense;
        public byte Type;
        public byte Type2;
        public byte CatchRate;
        public byte BaseExperience;
        public ushort EffortYield;
        public ushort HeldItem;
        public ushort HeldItem2;
        public byte GenderRatio;
        public byte EggCycles;
        public byte BaseFriendship;
        public byte LevelRate;
        public byte EggGroup;
        public byte EggGroup2;
        public byte Ability;
        public byte Ability2;
        public byte RunRate;
        public byte ColorFlip;
        public ushort Padding;
    }

    /// <summary>
    /// Represents an evolution entry.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    struct Evolution
    {
        [FieldOffset(0)]
        public ushort Method;
        [FieldOffset(2)]
        public ushort Parameter;
        [FieldOffset(4)]
        public ushort Target;
        [FieldOffset(6)]
        public ushort Padding;

        [FieldOffset(2)]
        public ushort Level;
        [FieldOffset(2)]
        public ushort Item;
    }

    /// <summary>
    /// Represents a moveset entry.
    /// </summary>
    struct Move
    {
        public ushort Attack;
        public byte Level;
    }

    /// <summary>
    /// Represents a Pokédex entry.
    /// </summary>
    struct Pokedex
    {
        public string Species; // 12 bytes
        public ushort Height;
        public ushort Weight;
        public Page Page1;
        public Page Page2;
        public ushort PokemonScale;
        public ushort PokemonOffset;
        public ushort TrainerScale;
        public ushort TrainerOffset;

        /// <summary>
        /// A page in a Pokédex entry.
        /// </summary>
        public struct Page
        {
            public int Offset;
            public string Text;
        }
    }
}
