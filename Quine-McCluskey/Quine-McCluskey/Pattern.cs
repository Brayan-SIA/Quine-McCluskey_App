using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Quine_McCluskey
{
    public class Pattern
    {
        public List<int> IN;
        public　int Get_IN(int n)
        {
            return IN[n];
        }
        public List<TextBlock> IN_Cell;
        public int OUT;
        public TextBlock OUT_Cell;
        public int number;


        public Pattern(int count, int number)
        {
            this.number = number;
            IN = new List<int>();
            IN_Cell = new List<TextBlock>();
            for(int i=0; i<count; i++)
            {
                //入力パターン計算
                int value = number;
                for (int j = count-1; j > i; j--) value /= 2;
                value %= 2;

                //入力値の保存
                IN.Add(value);
                //セルの用意
                TextBlock cell = new TextBlock();
                cell.Foreground = new SolidColorBrush(Colors.White);
                cell.TextAlignment = TextAlignment.Center;
                cell.Text = value.ToString();
                IN_Cell.Add(cell);
            }

            OUT = 0;
            OUT_Cell = new TextBlock();
            OUT_Cell.Height = 20;
            OUT_Cell.Width = 35;
            OUT_Cell.Text = "0";
            OUT_Cell.Foreground = new SolidColorBrush(Colors.Red);
            OUT_Cell.TextAlignment = TextAlignment.Center;
            OUT_Cell.PreviewMouseLeftButtonUp += ChangeOut; ;
        }

        private void ChangeOut(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (OUT == 0)
            {
                OUT_Cell.Foreground = new SolidColorBrush(Colors.LimeGreen);
                OUT = 1;
            }
            else
            {
                OUT_Cell.Foreground = new SolidColorBrush(Colors.Red);
                OUT = 0;
            }
            OUT_Cell.Text = OUT.ToString();
        }

        public void SetOut(int n)
        {
            OUT = n;
            if (OUT == 0)
            {
                OUT_Cell.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                OUT_Cell.Foreground = new SolidColorBrush(Colors.LimeGreen);
            }
            OUT_Cell.Text = OUT.ToString();
        }
    }
}
