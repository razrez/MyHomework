using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheSecondSem.MyListApp;

namespace TheSecondSem
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Random rnd = new Random();
            MySortedList glist1 = new MySortedList();
            MySortedList glist2 = new MySortedList();

            Console.WriteLine();
            Console.WriteLine("_____________CREATING FIRST LIST____________");
            for (int i = 0; i < 6; i++)
            {
                var add = rnd.Next(-143, 414);
                Console.WriteLine(add);
                glist1.AddWithSort(add);
                Console.WriteLine(glist1);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("_____________CREATING SECOND LIST____________");

            for (int i = 0; i < 6; i++)
            {
                var add = rnd.Next(-43, 44);
                Console.WriteLine(add);
                glist2.AddWithSort(add);
                Console.WriteLine(glist2);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("_____________UNITING OF BOTH LISTS____________");

            var result = MySortedList.ToUnite(glist1, glist2);
            Console.WriteLine(result);
            Console.WriteLine(glist1);
            Console.WriteLine(glist2);
            /////////////////////////////////////////////////////////


            MySortedList list1 = new MySortedList();
            MySortedList list2 = new MySortedList();
            for (int i = 1; i < 6; i++)
            {
                list1.AddLast(i);
                list2.AddLast(i + 2);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("_______________________");

            Console.WriteLine(list1);
            Console.WriteLine(list2);
            Console.WriteLine($"list1 & list2 INERSECTED ==> {MySortedList.ToIntersect(list1, list2)}");

            //////////////////////////////////////////////////////////


            MySortedList list3 = new MySortedList();
            MySortedList list4 = new MySortedList();
            for (int i = 0; i < 10; i++)
            {
                list3.AddWithSort(rnd.Next(20));
                list4.AddWithSort(rnd.Next(20));
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("_______________________");

            Console.WriteLine(list3);
            Console.WriteLine(list4);
            Console.WriteLine($"list3 & list4 DIFFERENCE ==> {MySortedList.FindDiffrence(list3, list4)}");
            Console.WriteLine(list3);
            Console.WriteLine(list4);*/


            /*MySortedList lst1 = new MySortedList();
            MySortedList lst2 = new MySortedList();
            for (int i = 20; i >= 10; i -= 2)
            {
                lst1.AddWithSort(i);
                lst2.AddWithSort(i + 3);
            }
            Console.WriteLine(lst1);
            Console.WriteLine(lst2);
            Console.WriteLine(lst1.ToUniteWith(lst2));*/

            /*MyList.ToSort(lst);
            Console.WriteLine(lst);
            Console.WriteLine("///////////////// the first list is sorted /////////////////");
            Console.WriteLine();

            Random rnd = new Random();

            MyList lst1 = new MyList();
            for (int i = -5; i <= 5; i++)
            {
                lst1.AddLast(rnd.Next(-8,16));
            }
            Console.WriteLine(lst1);
            MyList.ToSort(lst1);
            Console.WriteLine(lst1);
            Console.WriteLine("///////////////// the second list is sorted /////////////////");
            Console.WriteLine();

            Console.WriteLine(MyList.ToUnite(lst1, lst));
            Console.WriteLine("///////////////// both are united and sorted /////////////////");*/


            MyStack<string> people = new MyStack<string>();
            people.Push("Tom");
            people.Push("Bob");
            people.Push("Sam");
            people.Push("Abu-Bandit");
            Console.WriteLine($"Stack: {people}");
            Console.Write("pop:");
            while (!people.IsEmpty)
            {
                Console.WriteLine($"\t{people.Pop()}");
            }
            Console.WriteLine($"Stack: {people}");


            Console.WriteLine();
            Console.Write("Queue: ");
            MyQueue<string> queue = new MyQueue<string>();
            queue.Enqueue("Kate");
            queue.Enqueue("Sam");
            queue.Enqueue("Alice");
            queue.Enqueue("Tom");

            Console.WriteLine($"Queue: {queue}");
            string firstItem = queue.Dequeue();
            Console.WriteLine($"Dequeue: {firstItem}");
            Console.WriteLine($"Queue: {queue}");

            Console.WriteLine("_______________________");
            Console.WriteLine(IsCorrectString("(([])[])"));
            Console.WriteLine(IsCorrectString("((][])"));
            Console.WriteLine(IsCorrectString("((("));
            Console.WriteLine(IsCorrectString("(x)"));

            Console.WriteLine("_______________________");
            OutPut(new int[] {1, 2, 3, 4, 5, 6, 7});
            Console.ReadKey();
        }

        public static void OutPut(int[] arr)
        {
            var queue0 = new MyQueue<int>();
            var queue1 = new MyQueue<int>();
            foreach(var num in arr)
            {
                if (num % 2 == 0) queue0.Enqueue(num);
                else queue1.Enqueue(num);
            }
            Console.WriteLine(queue0);
            Console.WriteLine(queue1);
        }

        public static bool IsCorrectString(string str)
        {
            var pairs = new Dictionary<char, char>();
            pairs.Add('(', ')');
            pairs.Add('[', ']');
            var stack = new MyStack<char>();
            foreach (var e in str)
            {
                if (pairs.ContainsKey(e)) stack.Push(e);
                else if (pairs.ContainsValue(e))
                {
                    if (stack.Count == 0 || pairs[stack.Pop()] != e) return false;
                }
                else return false;
            }
            return stack.Count == 0;
        }
    }
}
