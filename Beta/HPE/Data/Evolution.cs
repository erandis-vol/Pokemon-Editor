using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Hopeless.Data
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
