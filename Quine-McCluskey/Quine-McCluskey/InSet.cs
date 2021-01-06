using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quine_McCluskey
{
    public class InSet
    {
        public Boolean used = false;    //合成に使われたか
        public int[] pin;               //入力情報
        public int value = 0;
        public List<int> List_value = new List<int>();
        public int count = 0;
        public int unique = 0;

        public InSet(int n)
        {
            pin = new int[n];
            for (int i = 0; i < pin.Length; i++)
            {
                pin[i] = 0;
            }
        }

        //各入力情報の問い合わせ
        public Boolean IsOn(int n)
        {
            if (pin[n] == 1) return true;
            else return false;
        }
        

        //入力値の10進数計算
        public void CalcValue()
        {
            value = 0;
            count = 0;
            for(int i = 0; i < pin.Length; i++)
            {
                if (pin[i]==1)
                {
                    value += (int)Math.Pow(2, pin.Length - i - 1);
                    count++;
                }
            }
            if(List_value.Count == 0) List_value.Add(value);
        }

        public override string ToString()
        {
            string str = "";
            for(int i = 0; i < pin.Length; i++)
            {
                //ドントケアでなければ表示
                if(pin[i] != -1)
                {
                    if (pin[i] == 0) str += " -X";
                    else str += " X";
                    str += (i + 1).ToString();
                }
            }
            return str;
        }
    }
}
