using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Flower
{
    static class InputController
    {
        private static void WrongCharsFilter(object sender, TextChangedEventArgs e)
        {
            var box = sender as TextBox;
            var builder = new StringBuilder(box.Text);
            var wasPoint = false;
            for (int i = 0; i < builder.Length; ++i)
            {
                if (builder[i] == '.')
                {
                    if (wasPoint || i == 0)
                    {
                        builder.Remove(i, 1);
                        --i;
                        continue;
                    }
                    wasPoint = true;
                    continue;
                }
                if (!Char.IsDigit(builder[i]))
                {
                    builder.Remove(i, 1);
                    --i;
                }
            }
            box.Text = builder.ToString();
        }

        public static void AllowOnlyDouble(this TextBox box) => box.TextChanged += WrongCharsFilter;
    }
}
