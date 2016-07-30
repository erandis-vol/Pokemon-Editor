using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hopeless.Drawing
{
    public class Palette
    {
        Color[] colors;

        public Palette(int colors)
        {
            this.colors = new Color[colors];
            Clear(Color.Black);
        }

        /*public Palette(Bitmap source, int maxColors = 16)
        {
            var colors = new List<Color>();
            Clear(Color.Black);

            for (int y = 0; y < source.Height && colors.Count < maxColors; y++)
            {
                for (int x = 0; x < source.Width && colors.Count < maxColors; x++)
                {
                    var color = source.GetPixel(x, y).Quantize();
                    if (!colors.Contains(color))
                        colors.Add(color);
                }
            }
        }*/

        public void Clear(Color color)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = color;
            }
        }

        public Color this[int index]
        {
            get { return colors[index]; }
            set { colors[index] = value; }
        }

        public int Length
        {
            get { return colors.Length; }
        }
    }
}
