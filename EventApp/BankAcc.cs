using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp
{
    public class BankAcc
    {
        public int Sum { get; private set; }
        public string Name { get; private set; }


        public delegate void AccountHandler(string message);


        public event Action<string> Notify;              // 1.Определение события
        public BankAcc(string fio, int sum)
        {
            Name = fio;
            Sum = sum;
        }
        public void Put(int sum)
        {
            Sum += sum;
            Notify?.Invoke($"На счет {Name} поступило: {sum}");   // 2.Вызов события 
        }
        public void Take(int sum)
        {
            if (Sum >= sum)
            {
                Sum -= sum;
                Notify?.Invoke($"Со счета {Name} снято: {sum}");   // 2.Вызов события

                //   if (Notify != null)
                //      Notify($"Со счета {Name} снято: {sum}");

            }
            else
            {
                Notify?.Invoke($"Недостаточно денег на счете {Name}. Текущий баланс: {Sum}"); ;
            }
        }
    }




}