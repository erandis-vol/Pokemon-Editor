using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lost.GBA;

namespace Lost
{
    // holds code responsible for loading/saving Pokemon data
    partial class MainForm
    {
        void OpenROM(string filename)
        {
            // create a new ROM
            var temp = new ROM(filename);

            // check that it is valid
        }
    }
}
