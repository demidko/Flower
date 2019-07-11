using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Flower
{
    static class Placeholder
    {
        public static bool IsPlaceholderNow(this TextBox box) => box.Foreground == Brushes.LightGray;

        public static void SetPlaceholder(this TextBox box, string value)
        {
            box.Text = value;
            box.LostKeyboardFocus += (o, e) =>
            {
                if (String.IsNullOrEmpty(box.Text))
                {
                    box.Foreground = Brushes.LightGray;
                    box.Text = value;
                    return;
                }
                box.Foreground = Brushes.Black;
            };
            box.GotKeyboardFocus += (o, e) =>
            {
                if (box.IsPlaceholderNow())
                {
                    box.Clear();
                }
                box.Foreground = Brushes.DarkSlateBlue;
            };
        }
    }
}
