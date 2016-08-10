namespace Lost.GBA
{
    public struct Pokemon
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
        public byte EggGroup, EggGroup2;
        public byte Ability, Ability2;
        public byte RunRate;
        public byte ColorFlip;
        public ushort Padding;
    }
}
