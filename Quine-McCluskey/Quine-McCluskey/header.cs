using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Quine_McCluskey
{
    class Header
    {
        private List<TextBlock> cell = new List<TextBlock>();
        public TextBlock GetCell(int i) { return cell[i]; }
        public double GetWitdh(int i) { return cell[i].Width; }
        public double GetSumWitdh()
        {
            double sum = 0;
            for (int i = 0; i < cell.Count; i++) sum += cell[i].Width;
            return sum+1;
        }
        private double height = 20;
        public double GetHeight() { return height; }
        private int font_size;
        public int GetFontSize() { return font_size; }

        public Header(List<string> List_name, int fontsize)
        {
            font_size = fontsize;
            foreach(string name in List_name)
            {
                TextBlock new_cell = new TextBlock();
                new_cell.Text = name;
                new_cell.Height = height;
                new_cell.Width = name.Length * font_size;
                new_cell.FontSize = fontsize;
                new_cell.TextAlignment = System.Windows.TextAlignment.Center;
                cell.Add(new_cell);
            }
        }
    }
}
