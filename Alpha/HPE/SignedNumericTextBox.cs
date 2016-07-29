using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPE
{
    class SignedNumericTextBox : TextBox
    {
        private int maxValue, minValue;

        public SignedNumericTextBox()
        {
            maxValue = int.MaxValue - 1;
            minValue = int.MinValue + 1;
            //Value = 0;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Limit Key presses
            if (char.IsDigit(e.KeyChar)) { }
            else if (e.KeyChar == '\b') { }
            else if (e.KeyChar == '-') { }
            else e.Handled = true;

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (Value > MaxValue)
            {
                Value = MaxValue;
            }
            else if (Value < MinValue)
            {
                Value = MinValue;
            }

            base.OnTextChanged(e);
        }

        public int Value
        {
            get
            {
                if (TextLength > 0)
                {
                    // Safe-check the characters. There should be a better way.
                    for (int i = 0; i < TextLength; i++)
                    {
                        if (char.IsDigit(Text[i])) { }
                        else if (Text[i] == '-' && i == 0) { }
                        else return 0;
                    }

                    return Convert.ToInt32(Text, 10);
                }
                else return 0;
            }
            set
            {
                Text = value.ToString();
            }
        }

        [Description("Gets or sets the maximum value allowed by the TextBox."), DefaultValue(int.MaxValue - 1)]
        public int MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        [Description("Gets or sets the minimum value allowed by the TextBox."), DefaultValue(int.MinValue + 1)]
        public int MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }
    }
}
