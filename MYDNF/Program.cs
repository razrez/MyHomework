using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;

namespace MYDNF
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var typeInfo = typeof(MyDNF).
                GetFields(BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.Public);
            foreach(var item in typeInfo)
            {
                Console.WriteLine($"{item.Name}\t IsPrivate: {item.IsPrivate}\t IsPublic: {item.IsPrivate }") ;
            }*/

            //построение DNF по строке
            Console.WriteLine("Построим по строке и выведем");

            Console.Write("f1 = ");
            var f1 = new MyDNF("x1&x2Vx3&-x4Vx5");
            Console.WriteLine(f1);

            Console.Write("f2 = ");
            var f2 = new MyDNF("x5V-x4&x5");
            Console.WriteLine(f2);

            Console.WriteLine();


            //Дизъюнкция 
            var f3 = f1.Disj(f2);
            Console.WriteLine("f3 = Disj f1 with f2");
            Console.WriteLine(f3);

            Console.WriteLine();



            //insert Konj
            Console.Write("Вставим новый конъюнкт ");
            var konj = new Konj("-x5");
            Console.WriteLine(konj);
            f3.Insert(konj);
            Console.WriteLine(f3);

            Console.WriteLine();



            //SortByLength
            Console.WriteLine("Упорядочим список конъюнкций по возрастанию длины.");
            f3.SortByLength();
            Console.WriteLine(f3);
            Console.WriteLine();


            //Value by bool v[]
            Console.Write("Найдём значение функции на наборе ");
            var v = new bool[] { true, false, true, false, true };
            for (int i = 0; i < v.Length; i++)
            {
                if (v[i] == true) Console.Write($"X{i + 1}=1 ");
                else Console.Write($"X{i + 1}=0 ");
            }
            Console.WriteLine($": {f3.Value(v)}");
            Console.WriteLine();


            //DNF dnfWith(int i)
            Console.Write("построим новую ДНФ, содержащую x2:   ");
            Console.WriteLine(f1.DnfWith(2));
            Console.ReadKey();
            MessageBox.Show("Вуе =)");
        }
    }
}
