using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashTableApp
{

    public class Elem
    {
        public int Key { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Key}->{Value}";
        }

    }

    public class MyHashTable
    {
        public int Size { get; private set; }

        private Elem[] table;

        private Func<int, int> hashFunc;

        private bool[] used;

        public MyHashTable(int _size, Func<int, int> _hashFunc)
        {
            Size = _size;
            table = new Elem[Size];
            hashFunc = _hashFunc;
            used = new bool[Size];
            for (int i = 0; i < Size ;i++) used[i] = false;
        }


        public void Insert(Elem el)
        {
            var hash = hashFunc(el.Key) % Size; // Getting adress
            //var lst = table[hash]; //нужная "ячейка"
            //if (lst == null) //делаем ячейку массивом
            //{
            //    lst = new List<Elem>();

            //}
            //lst.Add(el);
            //table[hash] = lst; // все обратно присваивается

            //если не занято
            if (!used[hash])
            {
                used[hash] = true;
                table[hash] = el;
            }

            else
            {
                int i = hash + 1;
                while (used[i] & i < Size) i++;
                if (i == Size)
                {
                    throw new DataMisalignedException();
                    /*Size *= 2;
                    var newtable = new Elem[Size];
                    for (int k = 0; i < Size; i++)
                    {
                        newtable[k] = table[k];
                        table = newtable;
                    }*/
                }
                else { table[i] = el; used[i] = true; }

            }

        }

        public Elem GetElem(int key) // получаем элемент по ключу
        {
            var hash = hashFunc(key) % Size; // Func

            if (table[hash] == null)
                throw new KeyNotFoundException();

            for(int i = hash; i < Size; i++)
            {
                if (table[i].Key == key)
                    return table[i];
            }
            throw new KeyNotFoundException();
        }

        public void DeleteByKey(int key)
        {
            var hash = hashFunc(key) % Size; // Func

            if (table[hash] == null)
                throw new KeyNotFoundException();

            for (int i = hash; i < Size; i++)
            {
                if (table[i].Key == key)
                {
                    table[i] = null;
                    used[i] = false;
                    break;
                }
                    
            }
        }

        public void Show()
        {
            for (int i = 0; i < Size; i++)
            {
                Console.Write($"{i} --- ");
                if(table[i] != null)
                    Console.Write($"{table[i].Key} {hashFunc(table[i].Key)} {table[i] .Value}");
                Console.WriteLine();
            }
        }


    }










    class Program
    {
        static void Main(string[] args)
        {
            Int64 k = 2654435769;

            MyHashTable tbl = new MyHashTable(50, x => (((x % 1000) * (x % 1000))
                                                            % 1000) * 2153 % 1000);

            tbl.Insert(new Elem() { Key = 52, Value = "rrrrrrrrrrr" });
            tbl.Insert(new Elem() { Key = 73, Value = "gggggg" });
            tbl.Insert(new Elem() { Key = 16, Value = "kkkkkkkkkk" });
            tbl.Insert(new Elem() { Key = 461, Value = "aaaaaaaa" });
            tbl.Insert(new Elem() { Key = 1461, Value = "| КОЛЛИЗИЯ ОТМЕНЯЕТСЯ |" });
            tbl.Insert(new Elem() { Key = 33349, Value = "bbbbbbbbbbb" });
            tbl.Insert(new Elem() { Key = 462, Value = "cccccccccc" });
            tbl.Insert(new Elem() { Key = 1462, Value = "| КОЛИЗИЯЯ ОТМЕНЯЕТСЯ |" });

            tbl.DeleteByKey(52);


            tbl.Show();
            Console.WriteLine($"\n{tbl.GetElem(1462)}");
            Console.ReadKey();
        }
    }
}