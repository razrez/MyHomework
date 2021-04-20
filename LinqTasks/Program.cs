using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTasks
{
    public class Fitness
    {
        public int ClientId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Duration { get; set; }

        public override string ToString()
        {
            return $"Client - {ClientId} Year- {Year} Month - {Month} Duration - {Duration}";
        }

        public static List<Fitness> CreateList()
        {
            var lst = new List<Fitness>();
            lst.Add(new Fitness() { ClientId = 1, Year = 2020, Month = 1, Duration = 10 });
            lst.Add(new Fitness() { ClientId = 1, Year = 2020, Month = 2, Duration = 23 });
            lst.Add(new Fitness() { ClientId = 1, Year = 2020, Month = 3, Duration = 14 });
            lst.Add(new Fitness() { ClientId = 3, Year = 2020, Month = 12, Duration = 8 });
            lst.Add(new Fitness() { ClientId = 3, Year = 2021, Month = 11, Duration = 12 });
            lst.Add(new Fitness() { ClientId = 4, Year = 2020, Month = 10, Duration = 13 });
            lst.Add(new Fitness() { ClientId = 4, Year = 2021, Month = 9, Duration = 30 });
            lst.Add(new Fitness() { ClientId = 2, Year = 2021, Month = 1, Duration = 5 });
            lst.Add(new Fitness() { ClientId = 2, Year = 2020, Month = 3, Duration = 8 });
            lst.Add(new Fitness() { ClientId = 2, Year = 2020, Month = 4, Duration = 5 });
            return lst;
        }
    }

    public class Prices
    {
        public string Shop { get; set; }
        public int IdProduct { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"Price - {IdProduct} {Shop} {Price}";
        }

    }

    class Program
    {
        public static void Obj71()
        {
            var A = new[]
            { 
                new{ IdClient  = 10, Year = 2020, Street = "acb" },
                new{ IdClient  = 11, Year = 2020, Street = "acb" },
                new{ IdClient  = 10, Year = 2020, Street = "acb" },
                new{ IdClient  = 13, Year = 2020, Street = "acb" },
                new{ IdClient  = 12, Year = 2020, Street = "acb" },
                new{ IdClient  = 13, Year = 2020, Street = "acb" },
                new{ IdClient  = 10, Year = 2020, Street = "acb" },
                new{ IdClient  = 14, Year = 2020, Street = "acb" },
                new{ IdClient  = 15, Year = 2020, Street = "acb" },
                new{ IdClient  = 14, Year = 2020, Street = "acb" },
                new{ IdClient  = 12, Year = 2020, Street = "acb" },
                new{ IdClient  = 16, Year = 2020, Street = "acb" },
                new{ IdClient  = 11, Year = 2020, Street = "acb" },
            };

            var C = new[] 
            {
                new{ IdClient  = 10, Sell = 20, Shop = "acb" },
                new{ IdClient  = 11, Sell = 24, Shop = "acF" },
                new{ IdClient  = 10, Sell = 20, Shop = "acF" },
                new{ IdClient  = 13, Sell = 20, Shop = "ack" },
                new{ IdClient  = 12, Sell = 20, Shop = "ack" },
                new{ IdClient  = 13, Sell = 20, Shop = "acb" },
                new{ IdClient  = 10, Sell = 20, Shop = "acl" },
                new{ IdClient  = 14, Sell = 20, Shop = "acl" },
                new{ IdClient  = 15, Sell = 20, Shop = "acD" },
                new{ IdClient  = 14, Sell = 20, Shop = "acc" },
                new{ IdClient  = 12, Sell = 20, Shop = "acD" },
                new{ IdClient  = 16, Sell = 20, Shop = "acc" },
                new{ IdClient  = 11, Sell = 20, Shop = "acb" },

            };

            Console.WriteLine(string.Join("\n", ""));
        }

        // 1V 11V 21V 31V 41 51 61 71 81 91
        public static void Obj1(List<Fitness> clients)
        {
            Console.WriteLine("Obj1");
            var minDur = from t in clients // определяем каждый объект из clients как t
                            where t.Duration == clients.Min(a => a.Duration) //фильтрация по критерию
                            select t;
            Console.WriteLine(minDur.Last());
        }

        public static void Obj11(List<Fitness> lst)
        {
            Console.WriteLine("Obj11");
            var res1 = lst.GroupBy(f => new { f.Year, f.Month })
                .Select(gr => new { Sum = gr.Sum(f => f.Duration), Year = gr.Key.Year, Month = gr.Key.Month })
                .OrderByDescending(f => f.Sum).ThenBy(f => f.Year);
            Console.WriteLine(string.Join("\n", res1));

            /*var res1 = lst.
                GroupBy(f => new
                {
                    f.ClientId,
                    f.Duration,
                    f.Year,
                    f.Month
                })
                .Select(gr => new
                {
                    AllDuration = gr.Sum(f => f.Duration),
                    Year = gr.Key.Year,
                    Month = gr.Key.Month
                })
                .OrderBy(c => c.AllDuration).ThenByDescending(c => c.Year).ThenBy(c => c.Month);*/
        }

        public static void Obj21()
        {
            Console.WriteLine("Obj21");
            var studs = new[]
            {
                new {Surname = "Иванов", Year = 2020, School = 6},
                new {Surname = "Ичанов", Year = 2021, School = 6},
                new {Surname = "Ивунов", Year = 2021, School = 6},
                new {Surname = "Ивенов", Year = 2021, School = 6},
                // у школы 6 максимум в 21
                new {Surname = "Ованов", Year = 2019, School = 5},
                new {Surname = "Аванов", Year = 2020, School = 5},
                new {Surname = "Еванов", Year = 2020, School = 5},
                new {Surname = "Ованок", Year = 2023, School = 5},
                new {Surname = "Яванов", Year = 2023, School = 5},
                new {Surname = "Явноев", Year = 2023, School = 5},
                // у школы 5 максимум в 23
                new {Surname = "Ивадов", Year = 2022, School = 3},
                new {Surname = "Иранов", Year = 2022, School = 3},
                new {Surname = "Арабов", Year = 2020, School = 3},
                // у школы 3 максимум в 22
                new {Surname = "Окянов", Year = 2022, School = 2},
                new {Surname = "Осянов", Year = 2022, School = 2},
                new {Surname = "Отянов", Year = 2021, School = 2},
                new {Surname = "Нокнов", Year = 2021, School = 2},
                new {Surname = "Нарнов", Year = 2021, School = 2},
                // у школы 2 максимум в 21
                new {Surname = "Парнов", Year = 2022, School = 1},
                new {Surname = "Иронов", Year = 2022, School = 1},
                new {Surname = "Азанов", Year = 2021, School = 1},
                new {Surname = "Крюнов", Year = 2020, School = 1},
                // у школы 1 максимум в 22
            };
            /*var result = from st in studs
                         where studs.GroupBy(s => new { s.Year, s.School }).Count() == studs.GroupBy(s => new { s.Year, s.School }).Count()
                         select st;*/

            /*var res1 = studs.GroupBy(u => new { u.Year, u.School }, f => new { f.Surname, f.Year, f.School })
                .Select(s => new { Sum = s.Count(), Surname = s.First().Surname, Year = s.Key.Year, School = s.Key.School });*/

            /* var res = studs.GroupBy(f => new { f.Year, f.School })
                 .Select(s => new { Sum = s.Count(), Surname = s.First().Surname, Year = s.Key.Year, School = s.Key.School })
                 .GroupBy(gr=> gr.School);*/

            var rs = (from r in ( studs.GroupBy(f => new { f.Year, f.School })
                    .Select(s => new { Sum = s.Count(),Surname = s.First().Surname,Year = s.Key.Year,School = s.Key.School }).GroupBy(gr => gr.School) )
                    from r2 in r where r2.Sum == r.Max(z => z.Sum) select (new { r2.Year, r2.Surname, r2.School }))
                    .OrderBy(or => or.Surname).ThenBy(tb=>tb.School);

            Console.WriteLine(string.Join("\n", rs));
        }

        public static void Obj31()
        {
            Console.WriteLine("Obj31\nТОП 3 ЗАДОЛЖНИКОВ ПО КАЖДОМУ ДОМУ");
            var citizens = new[]
            {
                new { Dept = 1234.24, Surname = "Иванов", Flat = 1},
                new { Dept = 1334.24, Surname = "Йванов", Flat = 3},
                new { Dept = 1434.24, Surname = "Цванов", Flat = 5},
                new { Dept = 1534.24, Surname = "Уванов", Flat = 9},
                new { Dept = 1634.24, Surname = "Кванов", Flat = 11},
                new { Dept = 1734.24, Surname = "Еванов", Flat = 14},
                new { Dept = 1834.24, Surname = "Нванов", Flat = 17},
                new { Dept = 1934.24, Surname = "Гванов", Flat = 36},

                new { Dept = 2234.24, Surname = "Шванов", Flat = 41},
                new { Dept = 3234.24, Surname = "Щванов", Flat = 44},
                new { Dept = 4234.24, Surname = "Званов", Flat = 57},
                new { Dept = 5234.24, Surname = "Хванов", Flat = 69},
                new { Dept = 6234.24, Surname = "Фванов", Flat = 72},

                new { Dept = 7234.24, Surname = "Ыванов", Flat = 73},
                new { Dept = 8234.24, Surname = "Аванов", Flat = 75},
                new { Dept = 9234.24, Surname = "Пванов", Flat = 80},
                new { Dept = 3334.24, Surname = "Рванов", Flat = 87},
                new { Dept = 3434.24, Surname = "Ованов", Flat = 98},
                new { Dept = 3534.24, Surname = "Лванов", Flat = 100},
                new { Dept = 3634.24, Surname = "Дванов", Flat = 108},

                new { Dept = 3734.24, Surname = "Жванов", Flat = 109},
                new { Dept = 3834.24, Surname = "Эванов", Flat = 121},
                new { Dept = 3934.24, Surname = "Яванов", Flat = 126},
                new { Dept = 1235.24, Surname = "Чванов", Flat = 134},
                new { Dept = 1236.24, Surname = "Тванов", Flat = 138},
                new { Dept = 1239.24, Surname = "Юванов", Flat = 144}
            };
            // (9 этажей 36 квартир) * 4 подъезда | всего 144 квартиры в доме
            // для каждого дома найти по 3 жильца с макс долгом
            // [долг] [подъезд] [квартира] [фамилия]

            /*var result = citizens.Select(s => new { Dept = s.Dept, Pod = (s.Flat - 1) / 36 + 1, Flat = s.Flat, Surname = s.Surname })
                .GroupBy(gr => gr.Pod).Select(s => new {s.Key, top3 = s.OrderByDescending(t=>t.Dept).Take(3)});*/

            /*foreach (var k in result) 
                foreach(var t in k.top3)
                    Console.WriteLine($"{k.Key} {t}");*/

            var res = citizens.Select(s => new { Dept = s.Dept, Podezd = (s.Flat - 1) / 36 + 1, Flat = s.Flat, Surname = s.Surname })
                .GroupBy(gr => new { gr.Podezd }).SelectMany(selector => selector.OrderByDescending(orb => orb.Dept).Take(3));

            Console.WriteLine(string.Join("\n", res));
        }

        public static void Obj41()
        {
            Console.WriteLine("Obj41");
            int M = 95;
            Console.WriteLine($"M={M}");

            var fuel = new[]
            {
                new {Mark = 92, Street = "Пушкина 19", Company = "BenzoTec", Price = 4200 },
                new {Mark = 95, Street = "Пушкина 18", Company = "BenzoTec", Price = 4300 },
                new {Mark = 98, Street = "Пушкина 17", Company = "BenzoTech", Price = 4320 },
                new {Mark = 92, Street = "Пушкина 18", Company = "BenzoTech", Price = 4400 },
                new {Mark = 95, Street = "Пушкина 21", Company = "BenzoTech", Price = 4340 },//
                new {Mark = 98, Street = "Пушкина 11", Company = "BenzoTech", Price = 4500 },
                new {Mark = 92, Street = "Пушкина 11", Company = "Benzoech", Price = 4400 },
                new {Mark = 92, Street = "Пушкина 11", Company = "BenzoTec", Price = 4200 },
                new {Mark = 98, Street = "Пушкина 19", Company = "BenzoTech", Price = 4300 },
                new {Mark = 95, Street = "Пушкина 19", Company = "BenzTech", Price = 4900 },//
                new {Mark = 95, Street = "Пушкина 18", Company = "BenTech", Price = 5000 },//
                new {Mark = 92, Street = "Пушкина 20", Company = "BenzoTech", Price = 4200 },
                new {Mark = 95, Street = "Пушкина 19", Company = "BenoTech", Price = 4250 },

            };
            // для каждой улицы где есть азс с бензом марки М,
            //      опр-ть макс цену бенза этой марки 
            var res = fuel.Where(w => w.Mark == M && w.Price == fuel.Where(d => d.Street == w.Street && d.Mark == w.Mark)
            .Max(m => m.Price)).Select(s => new { s.Mark, s.Price, s.Street })
            .OrderBy(or => or.Price).ThenBy(th => th.Street);

            Console.WriteLine((res.Count() > 0) ? string.Join("\n", res) : "Нет");
        }

        public static void Obj51()
        {

        }

        static void Main(string[] args)
        {
            Obj1(Fitness.CreateList()); // 1
            Console.WriteLine("\n ######################### \n");
            Obj11(Fitness.CreateList()); // 11
            Console.WriteLine("\n ######################### \n");
            Obj21();
            Console.WriteLine("\n ######################### \n");
            Obj31();
            Console.WriteLine("\n ######################### \n");
            Obj41();
            Console.WriteLine("\n ######################### \n");
            Obj51();

            Console.ReadKey();
        }
    }
}
