using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeApp
{
    public class SearchTree<T> where T : IComparable
    {
        public Elem<T> Root { get; set; }

        public override string ToString()
        {
            return Root.ToString();
        }

        public void AddElem(T data)
        {
            if (Root == null)
            {
                Root = new Elem<T>() { Info = data };
                return;
            }

            var elem = Root;
            while (elem.Info.CompareTo(data) != 0)
            {
                if (elem.Info.CompareTo(data) > 0)
                {
                    if (elem.Left != null)
                        elem = elem.Left;
                    else
                        elem.Left = new Elem<T>() { Info = data };
                }
                else if (elem.Info.CompareTo(data) < 0)
                {
                    if (elem.Right != null)
                        elem = elem.Right;
                    else
                        elem.Right = new Elem<T>() { Info = data };
                }

            }
        }

        public bool Contains(T data)
        {
            if (Root == null)
                return false;

            var elem = Root;
            while (elem != null)
            {
                if (elem.Info.CompareTo(data) == 0)
                    return true;
                if (elem.Info.CompareTo(data) > 0)
                    elem = elem.Left;
                else //if (elem.Info.CompareTo(data) < 0)
                    elem = elem.Right;
            }
            return false;
        }

        public void Delete(T data)
        {
            //TODO
        }
    }
}
