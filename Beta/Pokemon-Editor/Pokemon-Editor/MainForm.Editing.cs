using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lost
{
    partial class MainForm
    {
        private void tBaseGenderRatio_TextChanged(object sender, EventArgs e)
        {
            if (tBaseGenderRatio.Value == 255)
            {
                lBaseGender.Text = "genderless";
            }
            else
            {
                var percentFemale = tBaseGenderRatio.Value / 254f * 100f;
                lBaseGender.Text = $"{100f - percentFemale:0.0}% male/{percentFemale:0.0}% female";
            }

            if (ignore) return;
        }
    }
}
