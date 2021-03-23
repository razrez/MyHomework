using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheSecondSem.MyListApp;

namespace TheSecondSem
{
    public class MyQueue
    {
        private Elem First { get; set; }
        private Elem Last { get; set; }

        public void Push(int x)
        {
            var newEl = new Elem();
            newEl.Info = x;

            if (IsEmpty())
            {
                First = Last = newEl;
            }
            else
            {
                Last.Next = newEl;
                Last = newEl;
            }

        }

        public int Pop()
        {
            var x = First.Info;
            First = First.Next;
            if (First == null)
                Last = null;
            return x;
        }


        public bool IsEmpty()
        {
            return First == null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var el = First;
            while (el != null)
            {
                sb.Append($"{el.Info} ->");
                el = el.Next;
            }
            sb.Append("null");
            return sb.ToString();
        }



    }
}
