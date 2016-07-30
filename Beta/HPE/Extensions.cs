using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hopeless
{
    public static class Extensions
    {
        public static Color Quantize(this Color c)
        {
            int a = Round(c.A, 8);
            int r = Round(c.R, 8);
            int g = Round(c.G, 8);
            int b = Round(c.B, 8);

            return Color.FromArgb(a, r, g, b);
        }

        static int Round(int i, int n)
        {
            // if already rounded, return
            if (i % n == 0) return i;

            // round half-way between n
            return (i % n <= (n / 2) ? (i / n + 1) : (i / n)) * n;
        }
    }
}
