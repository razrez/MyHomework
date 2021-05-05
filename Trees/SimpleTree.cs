using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeApp
{
    public class Elem<T> where T : IComparable
    {
        public T Info { get; set; }
        public Elem<T> Left { get; set; }
        public Elem<T> Right { get; set; }

        public int CompareTo(Elem<T> other)
        {
            if ((object)other == null) return 1;
            return this.Info.CompareTo(other.Info);
        }

        public override string ToString()
        {
            // если дерево это лист то выводим без скобок 
            if (Left == Right)    //Left == null && Right == null   
                return Info.ToString();
            String strLeft = "";
            String strRight = "";
            if (Left != null)
                strLeft = Left.ToString();
            if (Right != null)
                strRight = Right.ToString();

            return $"({Info},{strLeft},{strRight})";
        }

        public T MyOper(Func<T, T, T> oper, T init)
        {
            T result = Info;
            if (Left != null)
                result = oper(result, Left.MyOper(oper, init));
            if (Right != null)
                result = oper(result, Right.MyOper(oper, init));
            return result;
        }

        public int Calc()
        {
            if (Left == Right)
                return int.Parse(Info.ToString());

            var str = Info.ToString();
            if (str[0] == '(')
                str = str.Remove(0, 1);
            switch (str)
            {
                case "+":
                    return Left.Calc() + Right.Calc();
                case "-":
                    return Left.Calc() - Right.Calc();
                case "*":
                    return Left.Calc() * Right.Calc();
                case "/":
                    return Left.Calc() / Right.Calc();
            }
            return 0;

        }

    }

    public class SimpleTree<T> where T : IComparable
    {
        public Elem<T> Root { get; private set; }

        public override string ToString()
        {
            return Root.ToString();
        }

        public T MyOper(Func<T, T, T> oper, T init)
        {
            if (Root != null)
                return Root.MyOper(oper, init);
            return init;
        }

        public SimpleTree()
        {

        }
        public SimpleTree(string str, Func<string, T> parser)
        {
            // (1,(2,4,(5,8,)),(3,6,7))
            // (*,(+,4,(/,8,4)),(-,6,7))
            // (1 (2 4 (5 8 _ _ _ _ (3 6 7 _
            int k = 0;
            Root = Create(str.Split(new char[] { ',', ')' }), ref k, parser);

        }

        public Elem<T> Create(String[] str, ref int k, Func<string, T> parser)
        {
            Elem<T> t = new Elem<T>();
            if (!str[k].Contains('('))
            {
                t.Info = parser(str[k]);
            }
            else
            {
                t.Info = parser(str[k].Remove(0, 1));//убрали '(' 
                k++;
                if (str[k] != "")
                    t.Left = Create(str, ref k, parser);
                k++;
                if (str[k] != "")
                    t.Right = Create(str, ref k, parser);
                k++;
            }
            return t;
        }

        public int Calc()
        {
            return Root.Calc();
        }

        //возвращается узел со значением Info == data
        public Elem<T> Search(T data) => Find(data).First();
        private IEnumerable<Elem<T>> Find(T data)
        {
            if (Root == null) yield break;

            var queue = new Queue<Elem<T>>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node.Info.CompareTo(data) == 0) { yield return node; break; }
                if (node.Left != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);

            }
        }

        //возвращается значение минимального узла, не объект!
        public T Min() => TraverseLevelOrder().Min();
        public IEnumerable<T> TraverseLevelOrder() 
        {
            if (Root == null) yield break;

            var queue = new Queue<Elem<T>>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node.Info;
                if (node.Left != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);
            }
        }

        //поиск минимально ЛИСТА
        public Elem<T> MinLeaf()
        {
            return minLeaf()
                .Where(w => w.Info.Equals(minLeaf()
                .Select(s => s.Info)
                .Min())).First();
        }
        private IEnumerable<Elem<T>> minLeaf()
        {
            if (Root == null) yield break;

            var queue = new Queue<Elem<T>>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node.Left == null && node.Right == null ) yield return node; 
                if (node.Left != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);

            }
        }


    }
}
