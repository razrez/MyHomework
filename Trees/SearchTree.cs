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
                //если текущий лист больше => go left
                if (elem.Info.CompareTo(data) > 0)
                    elem = elem.Left;
                else elem = elem.Right;
            }
            return false;
        }

        //Поиск
        public Elem<T> Find(T data)
        {
            if (Root == null)
                throw new Exception();
            var parent = new Elem<T>();
            var elem = Root;
            while (elem != null)
            {
                if (elem.Info.CompareTo(data) == 0)
                    break;
                //если текущий лист больше => go left
                if (elem.Info.CompareTo(data) > 0)
                    elem = elem.Left;
                else elem = elem.Right; 
            }
            return elem;
        }

        public void Delete(T data)
        {
            if (!Contains(data)) { Console.WriteLine($"{data} итак нет!"); return; }

            //найдём нужный узел и его отца
            var parent = new Elem<T>();
            var elem = Root;
            while (elem != null)
            {
                if (elem.Info.CompareTo(data) == 0)
                    break;
                //если текущий лист больше => go left
                if (elem.Info.CompareTo(data) > 0)
                {
                    parent = elem;
                    elem = elem.Left;
                }
                else { parent = elem; elem = elem.Right; }
            }
            /* elem - нужное поддерево
               parent - parent elem'a */


            //если elem-лист
            if (elem.Left == null && elem.Right == null) DeleteLeaf(elem, parent);
                
            //один сын
            if ((elem.Left != null && elem.Right == null) || (elem.Left == null && elem.Right != null))
                DelOneSon(elem, parent);

            //2 сына
            //беру максимум в левом поддереве, если у правого 
            if (elem.Left != null && elem.Right != null)
            {
                if (elem.Right != null & elem.Right.Left == null && elem.Right.Right == null)
                {
                    elem.Info = elem.Right.Info;
                    elem.Right = null;
                }
                else MaxL(elem);
            }
        }

        private void DeleteLeaf(Elem<T> elem, Elem<T> parent)
        {
            if (parent.Left == null) parent.Right = null;
            else if (parent.Right == null) parent.Left = null;
            else
            {
                if (parent.Right.CompareTo(elem) == 0) parent.Right = null;
                else parent.Left = null;
            }
        }

        private void DelOneSon(Elem<T> elem, Elem<T> parent)
        {
            if (elem.Left == null && elem.Right != null) RChanger(parent, elem);
            else LChanger(parent, elem);
        }
        private void RChanger(Elem<T> parent, Elem<T> elem)
        {
            if (parent.Left == null) parent.Right = elem.Right;
            else if (parent.Right == null) parent.Left = elem.Right;
            else
            {
                if (parent.Right.CompareTo(elem) == 0) parent.Right = elem.Right;
                else parent.Left = elem.Right;
            }
        }
        private void LChanger(Elem<T> parent, Elem<T> elem)
        {
            if (parent.Left == null) parent.Right = elem.Left;
            else if (parent.Right == null) parent.Left = elem.Left;
            else
            {
                if (parent.Right.CompareTo(elem) == 0) parent.Right = elem.Left;
                else parent.Left = elem.Left;
            }
        }

        private void MaxL(Elem<T> elem)
        {
            var current_parent = elem;
            var current_el = elem.Left;

            while(current_el.Right != null)
            {
                current_parent = current_el;
                current_el = current_el.Right; 
            }

            //если вправо не пошёл
            if (current_el.CompareTo(elem.Left) == 0)
            {
                elem.Info = current_el.Info;
                if(current_el.Left != null) elem.Left = current_el.Left;
                else elem.Left = null;
            }

            //ну а это если пошёл
            else
            {
                elem.Info = current_el.Info;
                if (current_el.Left != null)
                {
                    current_parent.Right = current_el.Left;
                }
                else current_parent.Right = null;
            }
        }
    }
}
