using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSecondSem
{
    class MyListApp
    {
        public class Elem
        {
            public int Info { get; set; }
            public Elem Next { get; set; }
        }

        public class MySortedList
        {
            private Elem First;

            public override string ToString()
            {
                var sb = new StringBuilder();
                var elem = First;
                while (elem != null)
                {
                    sb.Append($"{elem.Info} -> ");
                    elem = elem.Next;
                }
                sb.Append("null");
                return sb.ToString();
            }

            public void AddFirst(int x)
            {
                var newElem = new Elem() { Info = x, Next = First };
                First = newElem;
            }

            public void AddLast(int x)
            {
                var newElem = new Elem() { Info = x };

                if (First == null)
                {
                    First = newElem;
                    return;
                }
                var elem = First;
                while (elem.Next != null)
                    elem = elem.Next;

                elem.Next = newElem;
            }

            public void AddWithSort(int x) //add new elem according frm min-to-max sort
            {
                if (First == null)
                {
                    First = new Elem() { Info = x };
                    return;
                }

                if (x <= First.Info)
                {
                    AddFirst(x);
                }


                var elem = First;
                while (elem.Next != null)
                {
                    if (x <= elem.Info)
                    {
                        var newElem = new Elem() { Info = x, Next = elem };

                        return;
                    }

                    if (x > elem.Info && x > elem.Next.Info)
                    {
                        elem = elem.Next;
                    }

                    else 
                    {
                        var newelem = new Elem { Info = x, Next = elem.Next };
                        elem.Next = newelem;

                        return;
                    }
                }
                if (elem.Next == null) elem.Next = new Elem { Info = x };
            }

            public void DeleteByIndex(int ind)
            {
                if (First == null)
                    throw new ArgumentException();
                if (ind == 0)
                    First = First.Next;
                else
                {
                    var prevElem = First;
                    for (int i = 1; i < ind; i++)
                    {
                        prevElem = prevElem.Next;
                        if (prevElem == null)
                            throw new ArgumentException();
                    }
                    if (prevElem.Next != null)
                        prevElem.Next = prevElem.Next.Next;

                }
            }

            public int Count()
            {
                var elem = First;
                int countInd = 1;
                if (elem == null) return 0;
                while (elem.Next != null)
                {
                    elem = elem.Next;
                    countInd++;
                }
                return countInd;
            }

            public int FindByIndex(int ind)
            {
                //ToDo
                if (ind > Count())
                {
                    throw new IndexOutOfRangeException();
                }
                var elem = First;
                for (int i = 0; i < ind; i++)
                {
                    elem = elem.Next;
                }
                return elem.Info;
            }

            public int FindByValue(int x)
            {
                var elem = First;
                if (elem == null) throw new ArgumentOutOfRangeException();
                int index = 0;
                while (elem.Info != x)
                {
                    if (elem == null) { throw new ArgumentOutOfRangeException(); }
                    elem = elem.Next;
                    index++;
                }
                return index;
            }

            public void AddLst(Elem x)
            {

                if (First == null)
                {
                    First = x;
                    return;
                }
                var elem = First;
                while (elem.Next != null)
                    elem = elem.Next;

                elem.Next = x;
            }

            public void DelFirst() { First = First.Next; }

            public static MySortedList ToUnite(MySortedList lst1, MySortedList lst2) 
            {
                MySortedList lst = new MySortedList();
                var elem1 = new Elem { Info = lst1.First.Info, Next = lst1.First.Next };
                var elem2 = new Elem { Info = lst2.First.Info, Next = lst2.First.Next };
                while(elem1 != null && elem2 != null)
                {
                    if (elem1.Info < elem2.Info) { lst.AddLast(lst1.First.Info); elem1 = elem1.Next; }
                    else if (elem1.Info == elem2.Info) { lst.AddLast(elem1.Info); elem1 = elem1.Next; elem2 = elem2.Next; }
                    else { lst.AddLast(elem2.Info); elem2 = elem2.Next; }
                }

                if (elem1 != null && elem2 == null) { lst.AddLst(elem1); }

                else if (elem1 == null && elem2 != null) { lst.AddLst(elem2); }

                return lst; 
            }

            public static MySortedList ToIntersect(MySortedList lst1, MySortedList lst2)
            {
                MySortedList lst = new MySortedList();
                var elem1 = new Elem { Info = lst1.First.Info, Next = lst1.First.Next };
                var elem2 = new Elem { Info = lst2.First.Info, Next = lst2.First.Next };
                while (elem1 != null && elem2 != null)
                {
                    if (elem1.Info < elem2.Info) elem1 = elem1.Next;

                    else if (elem1.Info == elem2.Info) // тут проверяем на равность
                    {
                        lst.AddLast(lst2.First.Info);
                        elem2 = elem2.Next;
                        elem1 = elem1.Next;
                    }

                    else elem1 = elem1.Next;
                }

                return lst;
            }

            public static MySortedList FindDiffrence(MySortedList lst1, MySortedList lst2) //в lst1 убирается всё, что есть в lst2
            {
                MySortedList lst = new MySortedList();
                var elem1 = new Elem { Info = lst1.First.Info, Next = lst1.First.Next };
                var elem2 = new Elem { Info = lst2.First.Info, Next = lst2.First.Next };
                while (elem1 != null && elem2 != null)
                {
                    if (elem1.Info < elem2.Info) { lst.AddLast(elem1.Info); elem1 = elem1.Next; }

                    // тут проверяем на равность
                    else if (elem1.Info == elem2.Info) { elem1 = elem1.Next; elem2 = elem2.Next; }

                    else elem2 = elem2.Next;

                }

                if (elem1 != null && elem2 == null) { lst.AddLst(elem1); }

                return lst;
            }
        }


        public class MyList
        {
            public Elem First { get; set; }

            public void AddFirst(int x)
            {
                var newElem = new Elem() { Info = x, Next = First };
                First = newElem;
            }

            public void AddLast(int x)
            {
                var newElem = new Elem() { Info = x };

                if (First == null)
                {
                    First = newElem;
                    return;
                }
                var elem = First;
                while (elem.Next != null)
                    elem = elem.Next;

                elem.Next = newElem;
            }

            public static void ToSort(MyList lst)// frm min-to-max sort
            {
                if (!lst.IsSorted())
                {
                    for (int i = 0; i < lst.Count() - 1; i++)
                    {
                        for (int j = i + 1; j < lst.Count(); j++)
                        {
                            if (lst.FindByIndex(i) > lst.FindByIndex(j))
                            {
                                var temp = lst.FindByIndex(i);
                                lst.FindByInd(i).Info = lst.FindByInd(j).Info;
                                lst.FindByInd(j).Info = temp;
                            }
                        }
                    }
                }
            }

            public Elem FindByInd(int ind)
            {
                //ToDo
                if (ind > Count())
                {
                    throw new IndexOutOfRangeException();
                }
                var elem = First;
                for (int i = 0; i < ind; i++)
                {
                    elem = elem.Next;
                }
                return elem;
            }

            public void AddWithSort(int x)//add new elem according frm min-to-max sort
            {
                if (First == null)
                {
                    First = new Elem() { Info = x };
                    return;
                }

                if (x <= First.Info)
                {
                    AddFirst(x);
                }


                var elem = First;
                while (elem.Next != null)
                {
                    if (x <= elem.Info)
                    {
                        var newElem = new Elem() { Info = x, Next = elem };

                        return;
                    }

                    if (x > elem.Info && x > elem.Next.Info)
                    {
                        elem = elem.Next;
                    }

                    else
                    {
                        var newelem = new Elem { Info = x, Next = elem.Next };
                        elem.Next = newelem;

                        return;
                    }
                }
                if (elem.Next == null) elem.Next = new Elem { Info = x };
            }

            public int Count()
            {
                var elem = First;
                int countInd = 1;
                if (elem == null) return 0;
                while (elem.Next != null)
                {
                    elem = elem.Next;
                    countInd++;
                }
                return countInd;
            }

            public int FindByIndex(int ind)
            {
                //ToDo
                if (ind > Count())
                {
                    throw new IndexOutOfRangeException();
                }
                var elem = First;
                for (int i = 0; i < ind; i++)
                {
                    elem = elem.Next;
                }
                return elem.Info;
            }

            public Elem FindByValue(int x)
            {
                var elem = First;
                while (elem.Info != x)
                {
                    if (elem == null) { break; }
                    elem = elem.Next;
                }
                if (elem == null) throw new ArgumentOutOfRangeException();
                return elem;
            }

            public void DeleteByIndex(int ind)
            {
                if (First == null)
                    throw new ArgumentException();
                if (ind == 0)
                    First = First.Next;
                else
                {
                    var prevElem = First;
                    for (int i = 1; i < ind; i++)
                    {
                        prevElem = prevElem.Next;
                        if (prevElem == null)
                            throw new ArgumentException();
                    }
                    if (prevElem.Next != null)
                        prevElem.Next = prevElem.Next.Next;

                }
            }

            public static MyList ToUnite(MyList list1, MyList list2)//Упорядоченный список.
                                                                    //Объединение, пересечение и разность двух упорядоченных списков за O(n) 
            {
                var gran = list2.Count();
                var elem = list2.First;
                while (elem != null)
                {
                    if (!list1.IsThereByVal(elem.Info))
                    {
                        list1.AddLast(elem.Info);
                    }
                    elem = elem.Next;
                }
                MyList.ToSort(list1);
                return list1;
            }

            public bool IsThereByVal(int x)
            {
                bool result = false;
                var elem = First;
                while (elem.Next != null)
                {
                    if (elem.Info == x)
                    {
                        result = true;
                        break;
                    }
                    elem = elem.Next;
                }
                if (elem == null) throw new ArgumentOutOfRangeException();
                return result;
            }

            public bool IsSorted() //проверка на отсортированность по возрастанию
            {
                bool result = true;
                if (First == null)
                {
                    return true;
                }
                var elem = First;
                while (elem.Next != null)
                {
                    if (elem.Info >= elem.Next.Info) { result = false; break; }
                    elem = elem.Next;
                }
                return result;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                var elem = First;
                while (elem != null)
                {
                    sb.Append($"{elem.Info} -> ");
                    elem = elem.Next;
                }
                sb.Append("null");
                return sb.ToString();
            }

        }
    }
}