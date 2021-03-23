using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheSecondSem.MyListApp;

namespace TheSecondSem
{
    public class MyStack
    {
        private Elem Top { get; set; }

        public void Push(int x)
        {
            var nEl = new Elem();
            nEl.Info = x;
            nEl.Next = Top;
            Top = nEl;
        }

        public int Pop()
        {
            var x = Top.Info;
            Top = Top.Next;
            return x;
        }


        public bool IsEmpty()
        {
            return Top == null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var el = Top;
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
