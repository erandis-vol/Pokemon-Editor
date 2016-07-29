using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPE
{
    public class Pokemon
    {
        public const int SIZE = 28; // bytes

        public byte HP, Attack, Defense, Speed, SpAttack, SpDefense;
        public byte Type1, Type2;
        public byte CatchRate;
        public byte BaseExperience;
        public ushort EffortYield;
        public ushort Item1, Item2;
        public byte Gender;
        public byte EggCycles;
        public byte Friendship;
        public byte LevelRate;
        public byte EggGroup1, EggGroup2;
        public byte Ability1, Ability2;
        public byte RunRate;
        public byte ColorFlip;

        public Pokemon()
        {
            HP = Attack = Defense = SpAttack = SpDefense = Speed = 0;
            Type1 = Type2 = 0;
            CatchRate = 0;
            BaseExperience = 0;
            EffortYield = 0;
            Item1 = Item2 = 0;
            Gender = 0;
            EggCycles = 0;
            Friendship = 0;
            LevelRate = 0;
            EggGroup1 = EggGroup2 = 0;
            Ability1 = Ability2 = 0;
            RunRate = 0;
            ColorFlip = 0;
        }
    }

    public class Evolution
    {
        public const int SIZE = 8; // 5 entries of 8 bytes

        public ushort Type;
        public ushort Parameter;
        public ushort Target;

        public Evolution()
        {
            Type = 0; Parameter = 0; Target = 0;
        }
    }

    public class AttackEntry
    {
        public ushort Level;
        public ushort Attack;

        public AttackEntry()
        {
            Level = 0;
            Attack = 0;
        }
    }

    public class DexEntry
    {
        public string Species;
        public ushort Height, Weight;

        public uint Page1Offset, Page2Offset;
        public string Page1, Page2;
        public int Page1OS, Page2OS;

        public ushort PokemonScale, TrainerScale;
        public short PokemonOffset, TrainerOffset;

        public DexEntry()
        {
            Species = "";
            Height = Weight = 0;
            Page1Offset = Page2Offset = 0;
            Page1 = Page2 = "";
            Page1OS = Page2OS = 0;
            PokemonScale = TrainerScale = 0;
            PokemonOffset = TrainerOffset = 0;
        }
    }

    public class Cry
    {
        public uint TableOffset, Offset;
        public bool Compressed; // :(
        public bool Looped;
        public uint Pitch;
        public uint LoopStart;
        public uint OriginalSize;
        public sbyte[] Data;

        public Cry()
        {
            Offset = 0;
            Compressed = false;
            Looped = false;
            LoopStart = 0;
            OriginalSize = 0;
            Data = null;
        }
    }
}
