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
            //   Tree<int> tree = new Tree<int>("(100,4,)", s => int.Parse(s));

            Tree<int> tree = new Tree<int>("(-10,(23,46,(5,8,)),(13,36,7))", s => int.Parse(s));

            Tree<string> arTree = new Tree<string>("(/,(*,(+,3,13), 7),6)", s => s);

            //Tree<string> arTree = new Tree<string>("(*, (+, 4, (/, 8, 4)), (-, 6, 7))", s => s);
            // Tree<double> tree = new Tree<double>("(-10,4;(23,4;46,3;(5,2;8;));(13;36;7))", s => double.Parse(s));

            //tree.Root = new Elem<int>()
            //{
            //    Info = 5,
            //    Left = new Elem<int>() 
            //    { 
            //        Info = 4,
            //        Left = new Elem<int>() { Info = 10 },
            //        Right = new Elem<int>() { Info = 8 }
            //    },
            //    Right = new Elem<int>()
            //    {
            //        Info = 3,
            //        Left = new Elem<int>() { Info = 66 }
            //    }
            //};



            Console.WriteLine(arTree);
            Console.WriteLine(arTree.Calc());

            Console.WriteLine(tree.MyOper((a, b) => a + b, 0));

            Console.WriteLine(tree.MyOper((a, b) => a > b ? a : b, int.MinValue));



            Console.WriteLine("******************************************");

            SearchTree<int> st = new SearchTree<int>();
            st.AddElem(10);
            st.AddElem(5);
            st.AddElem(15);
            st.AddElem(3);
            st.AddElem(8);
            st.AddElem(12);
            st.AddElem(20);

            Console.WriteLine(st);


            Console.WriteLine(st.Contains(11));
        }
    }
}
