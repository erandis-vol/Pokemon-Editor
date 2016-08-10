namespace Lost.GBA
{
    public struct Pokemon
    {
        public byte HP; // x
        public byte Attack; // x
        public byte Defense; // x
        public byte Speed; // x
        public byte SpecialAttack; // x
        public byte SpecialDefense; // x
        public byte Type; // x
        public byte Type2; // x
        public byte CatchRate;
        public byte BaseExperience;
        public ushort EffortYield; // x
        public ushort HeldItem; // x
        public ushort HeldItem2; // x
        public byte GenderRatio;
        public byte EggCycles;
        public byte BaseFriendship;
        public byte LevelRate;
        public byte EggGroup, EggGroup2;
        public byte Ability, Ability2;
        public byte RunRate;
        public byte ColorFlip;
        public ushort Padding;
    }
}
