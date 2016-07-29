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
    public partial class NumericTextBox : TextBox
    {
        private NumberStyles numberStyle;
        private uint maxValue;

        public NumericTextBox()
        {
            numberStyle = NumberStyles.Decimal;
            maxValue = uint.MaxValue - 1;
            //Value = 0;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Limit Key presses
            if (numberStyle == NumberStyles.Decimal)
            {
                if (char.IsDigit(e.KeyChar)) { }
                else if (e.KeyChar == '\b') { }
                else e.Handled = true;
            }
            else if (numberStyle == NumberStyles.Hexadecimal)
            {
                if (char.IsDigit(e.KeyChar)) { }
                else if (e.KeyChar == '\b') { }
                else if (e.KeyChar >= 'a' && e.KeyChar <= 'f') { }
                else if (e.KeyChar >= 'A' && e.KeyChar <= 'F') { }
                else if (e.KeyChar == 'x' && Text.StartsWith("0") && TextLength == 1) { }
                else e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (Value > MaxValue)
            {
                Value = MaxValue;
            }

            base.OnTextChanged(e);
        }

        [Description("Gets or sets the number base used by the TextBox."), DefaultValue(NumberStyles.Decimal)]
        public NumberStyles NumberStyle
        {
            get { return numberStyle; }
            set
            {
                uint val = Value;
                numberStyle = value;
                Value = val; // Yeah.
            }
        }

        public uint Value
        {
            get
            {
                if (TextLength > 0)
                {
                    // Safe-check the characters. There should be a better way.
                    for (int i = 0; i < TextLength; i++)
                    {
                        if (numberStyle == NumberStyles.Hexadecimal)
                        {
                            if (char.IsDigit(Text[i])) { }
                            else if (Text[i] >= 'a' && Text[i] <= 'f') { }
                            else if (Text[i] >= 'A' && Text[i] <= 'F') { }
                            else if (Text[i] == 'x' || Text[i] == 'X' && i == 1) { }
                            else return 0;
                        }
                        else if (numberStyle == NumberStyles.Decimal)
                        {
                            if (char.IsDigit(Text[i])) { }
                            else return 0;
                        }
                        else return 0;
                    }

                    if (numberStyle == NumberStyles.Decimal) return Convert.ToUInt32(Text, 10);
                    else if (numberStyle == NumberStyles.Hexadecimal)
                    {
                        if (Text == "0x") return 0;
                        else return Convert.ToUInt32(Text, 16);
                    }
                    else return 0;
                }
                else return 0;
            }
            set
            {
                if (numberStyle == NumberStyles.Decimal) Text = value.ToString();
                else if (numberStyle == NumberStyles.Hexadecimal) Text = "0x" + value.ToString("X");
            }
        }

        [Description("Gets or sets the maximum value allowed by the TextBox."), DefaultValue(uint.MaxValue - 1)]
        public uint MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        public enum NumberStyles : int
        {
            Decimal = 10, Hexadecimal = 16
        }
    }
}
