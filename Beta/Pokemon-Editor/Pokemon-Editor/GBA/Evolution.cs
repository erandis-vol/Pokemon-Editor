using System.Runtime.InteropServices;

namespace Lost.GBA
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Evolution
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
}
