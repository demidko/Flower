using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Linq;

namespace Flower
{
    public partial class MainWindow : Window
    {
        private void AnyWrapPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var panel = sender as WrapPanel;
            var globalWidth = panel.ActualWidth;
            var boxList = new List<TextBox>();
            foreach(var i in panel.Children)
            {
                if (i is TextBox box)
                {
                    boxList.Add(box);
                }
                if(i is TextBlock block)
                {
                    globalWidth -= block.ActualWidth;
                }
            }
            var localWidth = (globalWidth / boxList.Count) - 5;
            if(localWidth < 0)
            {
                return;
            }
            boxList.ForEach(i => i.MaxWidth = localWidth);
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            if (FuncBox.IsPlaceholderNow())
            {
                MessageBox.Show("Сначала введите функцию в поле ввода.", "Невозможно провести тест!");
                return;
            }
            if (Compiler.CompileMathFunc(FuncBox.Text) is var f && f != null)
            {
                MessageBox.Show("Функция работоспособна.", "Успешный тест!");
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Генератор лабораторных работ по вычислительной математике\n" +
                "Поддерживаются функции и константы e, pi, pow(x, y) и прочие\n" +
                "Специально для группы Б8202 в 2019 году\n" +
                "Copyright © 2019 Даниил Демидко", "Flower 1.0");
        }

        public MainWindow()
        {
            InitializeComponent();
            
            FuncBox.SetPlaceholder("Введите вашу функцию здесь...");

            A_Box.SetPlaceholder("0");
            A_Box.AllowOnlyDouble();

            B_Box.SetPlaceholder("10");
            B_Box.AllowOnlyDouble();

            PointerBox.SetPlaceholder("0.25");
            PointerBox.AllowOnlyDouble();
        }
    }
}
