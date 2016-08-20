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
        public byte BaseExperience; // x
        public ushort EffortYield; // x
        public ushort HeldItem; // x
        public ushort HeldItem2; // x
        public byte GenderRatio;
        public byte EggCycles; // x
        public byte BaseFriendship;
        public byte LevelRate; // x
        public byte EggGroup, EggGroup2; // x
        public byte Ability, Ability2; // x
        public byte RunRate;
        public byte ColorFlip;
        public ushort Padding;
    }
}
