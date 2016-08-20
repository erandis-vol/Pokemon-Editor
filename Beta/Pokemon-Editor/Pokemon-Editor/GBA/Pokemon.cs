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
        public byte Type, Type2;
        public byte CatchRate;
        public byte BaseExperience;
        public ushort EffortYield;
        public ushort HeldItem, HeldItem2;
        public byte GenderRatio;
        public byte EggCycles;
        public byte BaseFriendship;
        public byte LevelRate;
        public byte EggGroup, EggGroup2;
        public byte Ability, Ability2;
        public byte RunRate;
        public byte ColorFlip; // color on dex page
        public ushort Padding;
    }
}
