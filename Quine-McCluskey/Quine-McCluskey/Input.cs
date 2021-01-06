using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Quine_McCluskey
{
    public class Input
    {
        public Button button;
        private int number;
        public int value = -1;
        public int Number() { return number; }
        MainWindow mainWindow;

        public Input(int n, MainWindow main)
        {
            number = n;
            button = new Button();
            button.Content = "X" + (number+1).ToString();
            button.FontSize = 18;
            button.Background = new SolidColorBrush(Colors.White);
            button.Margin = new Thickness(5, 5, 5, 5);
            button.Click += Button_Click;
            mainWindow = main;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (value)
            {
                //OFF
                case 0:
                    button.Background = new SolidColorBrush(Colors.White);
                    value = -1;
                    break;
                //ON
                case 1:
                    button.Background = new SolidColorBrush(Colors.Red);
                    value = 0;
                    break;
                //Dontcare
                case -1:
                    button.Background = new SolidColorBrush(Colors.LimeGreen);
                    value = 1;
                    break;
            }
            mainWindow.UpdateSetText();
        }
    }
}
