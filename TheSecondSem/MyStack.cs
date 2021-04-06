using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheSecondSem.MyListApp;

namespace TheSecondSem
{
    public class MyStack<T> //Stack on arrays
    {
        private T[] items; //elems of Stack
        private int count; //num of elems
        const int n = 10; //default size 

        public MyStack() { items = new T[n]; }
        public MyStack(int length)
        {
            items = new T[length];
        }//choose size

        // пуст ли стек
        public bool IsEmpty
        {
            get { return count == 0; }
        }
        // размер стека
        public int Count
        {
            get { return count; }
        }

        public void Push(T x)
        {
            if (count == items.Length)
                Resize(items.Length + 10);
            items[count++] = x;
        }

        public T Pop()
        {
            if (IsEmpty) 
                throw new InvalidOperationException("Stack is empty");

            T top = items[--count]; //сначала присвоение, потом вычетание
            items[count] = default(T); //сброс ссылки 

            if (count > 0 && count < items.Length - 10)
                Resize(items.Length - 10);

            return top;
        }

        public T Peek()
        {
            return items[count - 1];
        }

        private void Resize(int max)
        {
            T[] tempItems = new T[max];
            for (int i = 0; i < count; i++)
                tempItems[i] = items[i];
            items = tempItems;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            //out from the Top
            for(int i = 0; i <= Count - 1; i++)
            {
                sb.Append($"{items[i]} -> ");
            }
            sb.Append("...");
            return sb.ToString();
        }

    }
}
