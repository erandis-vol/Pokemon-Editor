using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lost
{
    // TODO: these don't allow Ctrl-A and other such things

    public class NumberBox : TextBox
    {
        /// <summary>
        /// Gets or sets the current value in the <see cref="NumberBox">NumberBox</see>.
        /// </summary>
        public int Value
        {
            get
            {
                // tries to convert Text to an int, returns 0 on failure
                try
                {
                    return Convert.ToInt32(Text);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                Text = value.ToString();
            }
        }

        public int MinimumValue { get; set; } = 0;
        public int MaximumValue { get; set; } = 100;

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // very simple, limit keys to back and numbers
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b')
                e.Handled = false;
            else
                e.Handled = true;

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (Value < MinimumValue)
                Value = MinimumValue;
            if (Value > MaximumValue)
                Value = MaximumValue;
        }
    }

    public class HexBox : TextBox
    {
        /// <summary>
        /// Gets or sets the current value in the <see cref="HexBox">HexBox</see>.
        /// </summary>
        public int Value
        {
            get
            {
                // tries to convert Text to an int, returns 0 on failure
                try
                {
                    return Convert.ToInt32(Text, 16);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                Text = "0x" + value.ToString("X");
            }
        }

        public int MinimumValue { get; set; } = 0;
        public int MaximumValue { get; set; } = 100;

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                (e.KeyChar >= 'a' && e.KeyChar <= 'f') ||
                (e.KeyChar >= 'A' && e.KeyChar <= 'F') ||
                e.KeyChar == '\b')
                e.Handled = false;
            else if (e.KeyChar == 'x')
                e.Handled = Text != "0";
            else
                e.Handled = true;

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (Value < MinimumValue)
                Value = MinimumValue;
            if (Value > MaximumValue)
                Value = MaximumValue;
        }
    }
}
