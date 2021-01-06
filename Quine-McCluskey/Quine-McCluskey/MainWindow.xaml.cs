using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quine_McCluskey
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        int input_count = 5;
        List<Input> List_input = new List<Input>();         //入力パネル情報
        List<Pattern> List_pattern = new List<Pattern>();   //真理値表情報
        List<InSet> List_true_set = new List<InSet>();      //加法標準形
        List<InSet> List_calc = new List<InSet>();          //計算用リスト
        List<int> List_value = new List<int>();             //確認用リスト
        Header header_pattern;
        InSet current_set = new InSet(5);          //入力中の項

        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow GetMainWindow() { return this; }

        //入力端子数の更新
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateInputCount();
        }
        private void UpdateInputCount()
        {
            bool success = false;   //成功判定
            try
            {
                int input = int.Parse(Input_text.Text);
                if (input >= 2)
                {
                    success = true;
                    input_count = input;
                }
                else
                {
                    OutputDebug("入力端子数は、2以上の整数値で入力してください。");
                }
                Input_text.Text = input_count.ToString();
            }
            catch (FormatException)
            {
                OutputDebug("入力端子数は、半角数字で入力してください。");
            }

            if (success)
            {
                OutputDebug("入力端子準備中…");
                InItInputPanel();
                InitPatternTable();
                current_set = new InSet(input_count);
                UpdateSetText();
                OutputDebug("入力端子準備完了-加法標準形か真理値表を入力して、算出ボタンをクリックしてください。");
                OutputDebug("入力パネルから加法標準形の式を入力できます。");
                OutputDebug("真理値表の出力値をクリックすることで、出力値を切り替えられます。");
            }
        }
        
        //入力パネルの準備
        private void InItInputPanel()
        {

            int i;
            List_input.Clear();
            List_true_set.Clear();
            Input_panel.Children.Clear();
            Form_text.Text = "OUT =";

            
            //ボタン用意
            for (i = 0; i < input_count; i++)
            {
                Input input = new Input(i, this);
                List_input.Add(input);
                Input_panel.Children.Add(input.button);
            }
        }


        //真理値表の準備
        private void InitPatternTable()
        {
            int i;
            List_pattern.Clear();
            table.Children.Clear();
            table_headder.Children.Clear();

            //ヘッダー用意
            List<string> list = new List<string>();
            for (i = 0; i < input_count; i++) list.Add("X"+(i+1));
            list.Add("OUT");
            header_pattern = new Header(list, 12);
            for (i = 0; i < input_count+1; i++) table_headder.Children.Add(header_pattern.GetCell(i));

            //セル用意
            for (i = 0; i < Math.Pow(2, input_count); i++)
            {
                Pattern data = new Pattern(input_count, i);
                
                for(int j = 0; j < data.IN_Cell.Count; j++)
                {
                    TextBlock cell = data.IN_Cell[j];
                    cell.Height = header_pattern.GetHeight();
                    cell.Width = header_pattern.GetWitdh(j);
                    cell.FontSize = header_pattern.GetFontSize();
                    table.Children.Add(cell);
                }
                table.Children.Add(data.OUT_Cell);
                List_pattern.Add(data);
            }
            table.Width = header_pattern.GetSumWitdh();
            table_headder.Width = header_pattern.GetSumWitdh();
            table.Height = Math.Pow(2, input_count) * 20 + 1;
        }
        

        //デバッグテキストの出力
        public void OutputDebug(string text)
        {
            Debug_text.Text = Debug_text.Text + text +"\n";
            Debug_Scroll.ScrollToBottom();       /*下までスクロール*/
        }

        //入力項の更新
        public void UpdateSetText()
        {
            set_text.Text = "";
            for(int i = 0; i < List_input.Count; i++)
            {
                int number = List_input[i].Number();
                switch (List_input[i].value)
                {
                    //OFF
                    case 0:
                        current_set.pin[number] = 0;
                        set_text.Text = set_text.Text + " -X" + (number + 1).ToString();
                        break;
                    //ON
                    case 1:
                        current_set.pin[number] = 1;
                        set_text.Text = set_text.Text + " X" + (number + 1).ToString();
                        break;
                    //Dontcare
                    case -1:
                        current_set.pin[number] = -1;
                        break;
                }
            }
        }

        //加算
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CanUse(current_set))
            {
                if(List_true_set.Count > 0) Form_text.Text = Form_text.Text + " +";
                Form_text.Text = Form_text.Text + set_text.Text;
                List_true_set.Add(current_set);
                current_set = new InSet(input_count);
            }
            else
            {
                OutputDebug("その項目はすでに追加されています。");
            }
        }

        //クリア
        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            List_true_set.Clear();
            Form_text.Text = "OUT =";
            OutputDebug("論理式をクリアしました。");
        }
        

        //既にあるかの確認
        public Boolean CanUse(InSet inSet)
        {
            inSet.CalcValue();

            //同値の項を検索
            InSet same = List_true_set.Find(x => x.pin.SequenceEqual(inSet.pin));
            //無ければ追加
            if (same == null) return true;
            else return false;
        }

        //加法標準形から導出
        private void Calc_Form_Button_Click(object sender, RoutedEventArgs e)
        {
            //真理値表を導出
            InitPatternTable();
            foreach (InSet inSet in List_true_set)
            {
                foreach (Pattern pattern in List_pattern)
                {
                    Boolean same = true;
                    for(int i = 0; i < input_count; i++)
                    {
                        //ドントケアじゃなければ
                        if (inSet.pin[i] != -1) {
                            if (inSet.pin[i] != pattern.IN[i])
                            {
                                same = false;
                                break;
                            }
                        }
                    }
                    if (same) pattern.SetOut(1);
                }
            }
            
            //計算
            Calc();
        }


        //真理値表から導出
        private void Calc_Table_Button_Click(object sender, RoutedEventArgs e)
        {
            //計算
            Calc();
        }

        //クワインマキラスキー計算
        public void Calc()
        {
            Boolean keep = true;    //計算継続の確認
            List<InSet> List_culc_next = new List<InSet>();     //計算結果格納用リスト
            OutputDebug("計算中です。。。");
            
            //パターンを導出
            List_calc.Clear();
            foreach (Pattern pattern in List_pattern)
            {
                if (pattern.OUT == 1)
                {
                    InSet inSet = new InSet(input_count);
                    for (int i = 0; i < input_count; i++)
                    {
                        inSet.pin[i] = pattern.Get_IN(i);
                    }
                    inSet.CalcValue();
                    List_value.Add(inSet.value);
                    List_calc.Add(inSet);
                }
            }
            List_value = List_value.Distinct().ToList();

            while (keep)
            {
                keep = false;
                List_culc_next = new List<InSet>();
                for (int i = 0; i < List_calc.Count; i++)
                {
                    for(int j = i+1; j < List_calc.Count; j++)
                    {
                        //比較して合成可能だったら
                        if(Comparison(List_calc[i], List_calc[j]))
                        {
                            //合成結果を格納用リストに格納
                            InSet merge = Merge(List_calc[i], List_calc[j]);
                            merge.List_value = List_calc[i].List_value;
                            merge.List_value.AddRange(List_calc[j].List_value);
                            merge.List_value = merge.List_value.Distinct().ToList();
                            merge.CalcValue();
                            List_culc_next.Add(merge);
                            List_calc[i].used = true;
                            List_calc[j].used = true;
                            keep = true;
                        }
                    }
                }
                //どれとも合成されなかった要素の継承
                for (int i = 0; i < List_calc.Count; i++)
                {
                    if (List_calc[i].used == false) List_culc_next.Add(List_calc[i]);
                }
                List_calc = List_culc_next;
                List_calc = DeleteSame();
                string str = "";
                foreach (InSet inSet in List_calc) str += inSet.ToString() + " | ";
                OutputDebug(str);
            }

            //不要項の整理
            CleanSet();

            DisplayAns();
            OutputDebug("計算完了");
        }

        //項目の比較
        private Boolean Comparison(InSet inSet1, InSet inSet2)
        {
            int count = 0;

            if (inSet1.count - inSet2.count != 1 && inSet2.count - inSet1.count != 1) return false;

            for (int i = 0; i < input_count; i++)
            {
                //異なったら
                if(inSet1.pin[i] != inSet2.pin[i])
                {
                    count++;
                    if (count > 1) return false;
                }
            }
            //異なるのが1か所だったらTrue
            if (count == 1) return true;
            else return false;
        }

        //項目の合成
        private InSet Merge(InSet inSet1, InSet inSet2)
        {
            InSet after = new InSet(input_count);

            for (int i = 0; i < input_count; i++)
            {
                //異なったら
                if (inSet1.pin[i] != inSet2.pin[i])
                {
                    after.pin[i] = -1;
                }
                //同じだったら
                else
                {
                    after.pin[i] = inSet1.pin[i];
                }
            }
            
            return after;
        }
        
        //不要項の整理
        public void CleanSet()
        {
            List_calc = DeleteSame();
            CalcUnique();
            //ユニーク数の多い順にソート
            List_calc.Sort((a, b) => b.unique - a.unique);
            
            foreach(InSet inSet in List_calc)
            {
                Boolean used = false;
                foreach(int value in inSet.List_value)
                {
                    int same = -1;
                    if(List_value.Contains(value)) same = List_value.Find(x => x == value);
                    if(same != -1)
                    {
                        List_value.Remove(same);
                        used = true;
                    }
                }
                inSet.used = used;
            }
        }

        //重複要素の削除
        public List<InSet> DeleteSame()
        {
            List<InSet> new_List = new List<InSet>();

            foreach(InSet inSet in List_calc)
            {
                InSet same = new_List.Find(x => x.pin.SequenceEqual(inSet.pin));
                if (same == null)
                {
                    new_List.Add(inSet);
                }
            }

            return new_List;
        }
        //配列要素が等しいか

        //ユニーク数をカウント
        public void CalcUnique()
        {
            foreach (InSet inSet in List_calc)
            {
                foreach (int value in inSet.List_value)
                {
                    Boolean unique = true;
                    foreach (InSet inSet2 in List_calc)
                    {
                        if (inSet == inSet2) continue;
                        if (inSet2.List_value.Contains(value))
                        {
                            unique = false;
                            break;
                        }
                    }
                    if (unique) inSet.unique++;
                }
            }
        }

        //答えの表示
        public void DisplayAns()
        {
            out_text.Text = "OUT= ";
            for(int i = 0; i < List_calc.Count; i++)
            {
                if (!List_calc[i].used) continue;
                if (i > 0) out_text.Text = out_text.Text + " +";
                out_text.Text = out_text.Text + List_calc[i].ToString();
            }
        }
    }
}
