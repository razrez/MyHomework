using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var testree = new SimpleTree<int>("(11,(9,18,10),(13,12,15))", s => int.Parse(s));
            Console.WriteLine($"Поуровневый обход и поиск минимума {testree}:");
            Console.WriteLine(string.Join("\t", testree.TraverseLevelOrder()));
            Console.WriteLine($"MIN leaf = {testree.MinLeaf()}");
            Console.ReadKey();

            SimpleTree<int> tree = new SimpleTree<int>("(-10,(23,46,(5,8,)),(13,36,7))", s => int.Parse(s));
            SimpleTree<string> arTree = new SimpleTree<string>("(/,(*,(+,3,13), 7),6)", s => s);

            Console.WriteLine(arTree);
            Console.WriteLine(arTree.Calc());
            Console.WriteLine(tree.MyOper((a, b) => a + b, 0));
            Console.WriteLine(tree.MyOper((a, b) => a > b ? a : b, int.MinValue));


            ///////////////////////////////////////////////////////////////
            Console.WriteLine("\n******************************************\n");
            ///////////////////////////////////////////////////////////////


            SearchTree<int> st = new SearchTree<int>();
            st.AddElem(10);
            st.AddElem(5);
            st.AddElem(15);
            st.AddElem(3);
            st.AddElem(8);
            st.AddElem(12);
            st.AddElem(20);
            Console.WriteLine(st);

            //st.Delete(5);
            //st.Delete(91);
            //st.Delete(10);
            st.Delete(8);
            st.Delete(10);

            Console.WriteLine(st);
            Console.ReadKey();
        }
    }
}
