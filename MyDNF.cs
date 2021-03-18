using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYDNF
{
    class MyDNF
    {
        private List<Konj> function = new List<Konj>();

        public List<Konj> Function
        {
            get
            {
                return  function;
            }
            private set
            {
                function = value;
            }
        }


        //пустой конструктор 
        public MyDNF() {}

        //построение по строке
        public MyDNF(string st)
        {
            // если ничего с исками не введено
            if (!st.Contains("X") && !st.Contains('x')) throw new Exception("Wrong enter");

            if (st.Contains('V')) // делим на дизъюнкты
            {
                foreach (var konj in st.Split('V')) //идём по конъюнктам
                {
                    function.Add(new Konj(konj));
                }
            }

            else { function.Add(new Konj(st)); }

        }

        //построение по конъюнкту
        public MyDNF(Konj konj)
        {
            Function.Add(konj);
        }

        //построение по днф
        public MyDNF(MyDNF d) 
        {
            foreach (var fill in d.function) function.Add(fill);
        }

        public override string ToString()
        {
            var res = new StringBuilder();
            foreach (var konj in function) //идём по конъюнктам
            {
                res.Append(konj.ToString());

                if (konj != function[function.Count - 1]) res.Append(" V ");
            }

            return res.ToString();
        }


        /***  МЕТОДЫ  ***/
        public bool Contain(Konj konj)
        {
            bool result = false;

            for (int i = 0; i < function.Count(); i++)
            {
                if (function[i].Nums.SequenceEqual(konj.Nums)) result = true;
            }
            return result;
        }

        public void Insert(Konj k)
        {
            if (!Contain(k)) function.Add(k);
        }

        public MyDNF Disj(MyDNF d)
        {
            var result = new MyDNF();

            //заполним нужными конъюнктами
            foreach(var fill in function) result.function.Add(fill);
            foreach (var nmK in d.function)
            {
                if (!result.Contain(nmK)) result.Insert(nmK);
            }

            return result;
        }

        // кол-во переменных
        public int NumbOfVars()
        {
            int max = 0;

            foreach (var konj in function)
            {
                var x = konj.Nums.Max();
                if (x > max) max = x;
            }
            return max;
        }

        public bool Value(bool[] v)
        {

            if (v.Length < NumbOfVars()) throw new Exception("Количество значений не совпадает с количетсвом перемменых функции");

            int valDnf = 0;
            foreach(var konj in function)
            {
                int valKonj = 1;
                for(int i = 0; i < konj.Nums.Count(); i++)
                {
                    if (konj.Nums[i] > 0 && v[konj.Nums[i]-1] == false) valKonj = 0;
                    else if (konj.Nums[i] < 0 && v[-konj.Nums[i]-1] == true) valKonj = 0;
                }
                valDnf += valKonj;
            }

            return valDnf != 0;
        }

        public void SortByLength() //x1 V x2&x3 V -x3&x4&x5 
        {
            for (int i = 0; i < function.Count() - 1; i++)
            {
                for (int j = i; j < function.Count(); j++)
                {
                    if (function[i].Nums.Count() > function[j].Nums.Count())
                    {
                        var temp = function[i];
                        function[i] = function[j];
                        function[j] = temp;
                    }
                }
            }
        }

        public MyDNF DnfWith(int i)
        {
            var result = new MyDNF();
            foreach (var konj in function)
            {
                if (konj.Nums.Contains(i) || konj.Nums.Contains(-i))
                    result.function.Add(konj);
            }

            return result;
        }

    }

    class Konj
    {
        private List<int> nums = new List<int>();

        public List<int> Nums
        {
            get
            {
                return nums;
            }
            set
            {
                nums = value;
            }
        }


        //пустой конструктор
        public Konj() {}

        //построение по строке
        public Konj(string st) 
        {
            if (!st.Contains("X") && !st.Contains('x')) throw new Exception("Wrong enter");
            if (st.Contains('&')) //parse konj on elemts (x1 x2 x3 x4 x5)
            {
                string[] strn = st.Split('&');

                foreach (var elem in strn) //filling Konj List<int> Nums
                {
                    if (elem[0] != '-') nums.Add(int.Parse($"{elem[1]}"));
                    else nums.Add((-1) * int.Parse($"{elem[2]}"));
                }
            }

            //если конъюнкт состоит из 1-ой переменной
            else if (st[0] != '-') nums.Add(int.Parse($"{st[1]}"));
            else nums.Add((-1) * int.Parse($"{st[2]}"));
        }

        //построение по списку значений <int>
        public Konj(List<int> p) { foreach (var fill in p) nums.Add(fill); }

        public override string ToString()
        {
            var res = new StringBuilder();
            var count = nums.Count;
            if (count > 1)
            {
                if (nums[0] < 0) res.Append($"-X{-nums[0]}");
                else res.Append($"X{nums[0]}");

                for (int i = 1; i < count; i++)
                {
                    if (nums[i] < 0) res.Append($"&-X{-nums[i]}");
                    else res.Append($"&X{nums[i]}");
                }
            }

            else if (nums[0] < 0) res.Append($"-X{-nums[0]}");
            else res.Append($"X{nums[0]}");

            return res.ToString();
        }
    }
}
