using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{
    public class PriorityTask
    {
        public string Name { get; set; }
        public int Priority { get; set; }

        public PriorityTask(string n, int p)
        {
            Name = n;
            Priority = p;
        }

        public override string ToString()
        {
            return $"{Name} : {Priority}";
        }

    }

    public class PriorityQueue
    {
        private int Capacity;

        private PriorityTask[] mas;

        public PriorityQueue(int cap)
        {
            Capacity = cap;
            mas = new PriorityTask[2 * Capacity];
        }

        private void Normalization() //пересчёт всей кучи
        {
            for (int i = Capacity - 1; i > 0; i--)
            {
                if (mas[2 * i] == null)
                {
                    mas[i] = mas[2 * i + 1];
                    continue;
                }
                if (mas[2 * i + 1] == null)
                {
                    mas[i] = mas[2 * i];
                    continue;
                }
                if (mas[2 * i].Priority >= mas[2 * i + 1].Priority)
                    mas[i] = mas[2 * i];
                else
                    mas[i] = mas[2 * i + 1];
            }


        }

        private void Resize()
        {
            var newarr = new PriorityTask[mas.Length*2];
            Capacity *= 2;
            for (int i = 0; i < mas.Length / 2; i++)
            {
                newarr[i] = mas[i];
            }

            for (int i = mas.Length / 2; i < mas.Length; i++)
            {
                newarr[i * 2] = mas[i];
            }
            mas = newarr;
        }

        public void Push(PriorityTask elem)
        {
            for (int i = Capacity; i < 2 * Capacity; i++)
            {
                if (mas[i] == null)
                {
                    mas[i] = elem;
                    Normalization();
                    return;
                }
            }
            // увеличить очередь в два раза - V
            Resize();

            for (int i = Capacity; i < 2 * Capacity; i++)
            {
                if (mas[i] == null)
                {
                    mas[i] = elem;
                    Normalization();
                    return;
                }
            }
        }

        public string Pop()
        {
            var task = mas[1].Name;
            int i = 1;
            while (i < Capacity)
            {
                if (mas[2 * i] == null)
                {
                    i = 2 * i + 1;
                    continue;
                }
                if (mas[2 * i + 1] == null)
                {
                    i = 2 * i;

                    continue;
                }
                if (mas[2 * i] == mas[i])
                    i = 2 * i;
                else
                    i = 2 * i + 1;
            }

            mas[i] = null;
            Normalization();
            return task;
        }



        public override string ToString()
        {
            int k = 2;
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < 2 * Capacity; i++)
            {
                if (i == k)
                {
                    sb.AppendLine();
                    k *= 2;
                }
                sb.Append($"{mas[i]} ");

            }
            return sb.ToString();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            PriorityQueue q = new PriorityQueue(8);
            q.Push(new PriorityTask("task1", 1));
            q.Push(new PriorityTask("task2", 7));
            q.Push(new PriorityTask("task3", 3));
            q.Push(new PriorityTask("task4", 5));
            q.Push(new PriorityTask("task5", 10));
            q.Push(new PriorityTask("task16", 2));
            q.Push(new PriorityTask("task13", 3));
            q.Push(new PriorityTask("task14", 5));
            q.Push(new PriorityTask("task24", 12));
            q.Push(new PriorityTask("task34", 15));

            Console.WriteLine(q);
            Console.ReadKey();
            Console.WriteLine();

            Console.WriteLine(q.Pop());
            Console.WriteLine();
            Console.WriteLine(q);
            Console.ReadKey();

            Console.WriteLine();
            q.Push(new PriorityTask("task10", 5));
            Console.WriteLine(q);

            /*Console.WriteLine(q.Pop());
            Console.WriteLine(q);

            Console.WriteLine(q.Pop());
            Console.WriteLine(q);*/

            /*Console.WriteLine(q.Pop());
            Console.WriteLine(q);

            Console.WriteLine(q.Pop());
            Console.WriteLine(q);

            Console.WriteLine(q.Pop());
            Console.WriteLine(q);*/

            
            Console.ReadKey();
        }
    }
}