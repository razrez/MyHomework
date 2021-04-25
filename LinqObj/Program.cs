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
        // 1V 11V 21V 31V 41V 51V 61V 71V 81V 91

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
            Console.WriteLine("41");
            int M = new int[] { 92, 95, 98 }[new Random().Next(3)]; //random choice
            Console.WriteLine($"M={M}");
            Console.WriteLine("***Самый дорогой бенз марки М на каждой улице***");

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
            Console.WriteLine("Obj51");
            var studs = new[]
            {   //                  IT
                new {Mark ="100 100 21", Surname = "Иванов", Inic = "", School = 6 },
                new {Mark ="100 100 25", Surname = "Ичанов", Inic = "", School = 6 },
                new {Mark ="100 100 20", Surname = "Ивунов", Inic = "", School = 6 },
                new {Mark ="100 100 29", Surname = "Ивенов", Inic = "", School = 6 },//\\

                new {Mark ="100 100 31", Surname = "Ованов", Inic = "", School = 5 },
                new {Mark ="100 100 19", Surname = "Аванов", Inic = "", School = 5 },
                new {Mark ="100 100 45", Surname = "Еванов", Inic = "", School = 5 },//\\
                new {Mark ="100 100 30", Surname = "Ованок", Inic = "", School = 5 },
                new {Mark ="100 100 22", Surname = "Яванов", Inic = "", School = 5 },
                new {Mark ="100 100 40", Surname = "Явноев", Inic = "", School = 5 },

                new {Mark ="100 100 29", Surname = "Ивадов", Inic = "", School = 3 },
                new {Mark ="100 100 26", Surname = "Иранов", Inic = "", School = 3 },
                new {Mark ="100 100 30", Surname = "Арабов", Inic = "", School = 3 },//\\

                new {Mark ="100 100 41", Surname = "Окянов", Inic = "", School = 2 },
                new {Mark ="100 100 51", Surname = "Осянов", Inic = "", School = 2 },//\\
                new {Mark ="100 100 51", Surname = "Отянов", Inic = "", School = 2 },
                new {Mark ="100 100 43", Surname = "Нокнов", Inic = "", School = 2 },
                new {Mark ="100 100 24", Surname = "Нарнов", Inic = "", School = 2 },

                new {Mark ="100 100 21", Surname = "Парнов", Inic = "", School = 1 },
                new {Mark ="100 100 31", Surname = "Иронов", Inic = "", School = 1 },
                new {Mark ="100 100 31", Surname = "Азанов", Inic = "", School = 1 },
                new {Mark ="100 100 94", Surname = "Крюнов", Inic = "", School = 1 },//\\
            };
            // для каждой шк ученика с макс баллами по инфе
            // школа     фамилия     инициалы    балл по инфе
            var result = studs.Where(w => int.Parse(w.Mark.Split(' ')[2]) == (studs.Where(d => d.School == w.School).Max(m => int.Parse(m.Mark.Split(' ')[2]))))
                .Select(s => new { s.School, s.Surname, s.Inic, IT = int.Parse(s.Mark.Split(' ')[2]) })
                .GroupBy( gr => gr.School).SelectMany(s => s.Take(1)).OrderBy(or => or.School);

            Console.WriteLine(string.Join("\n", result));
        }

        public static void Obj71()
        {
            Console.WriteLine("Obj71");
            var A = new[]
            {
                new{ IdClient  = 10, Year = 2020, Street = "Пушкина 20" },
                new{ IdClient  = 11, Year = 2020, Street = "Пушкина 19" },
                new{ IdClient  = 12, Year = 2020, Street = "Пушкина 13" },
                new{ IdClient  = 13, Year = 2020, Street = "Пушкина 14" },
                new{ IdClient  = 14, Year = 2020, Street = "Пушкина 18" },
                new{ IdClient  = 15, Year = 2020, Street = "Пушкина 11" },
                new{ IdClient  = 16, Year = 2020, Street = "Пушкина 32" },
            };

            var C = new[]
            {
                new{ IdClient  = 10, Sell = 20, Shop = "Магаз 1" },
                new{ IdClient  = 11, Sell = 24, Shop = "Магаз 1" },
                new{ IdClient  = 10, Sell = 30, Shop = "Магаз 1" },//\\
                new{ IdClient  = 13, Sell = 10, Shop = "Магаз 1" },

                new{ IdClient  = 12, Sell = 22, Shop = "Магаз 2" },
                new{ IdClient  = 13, Sell = 20, Shop = "Магаз 2" },
                new{ IdClient  = 10, Sell = 21, Shop = "Магаз 2" },
                new{ IdClient  = 14, Sell = 35, Shop = "Магаз 2" },//\\

                new{ IdClient  = 15, Sell = 20, Shop = "Магаз 3" },
                new{ IdClient  = 14, Sell = 20, Shop = "Магаз 3" },//\\

                new{ IdClient  = 16, Sell = 20, Shop = "Магаз 4" },
                new{ IdClient  = 11, Sell = 27, Shop = "Магаз 4" },//\\
                new{ IdClient  = 12, Sell = 20, Shop = "Магаз 4" },

            };
            // Для каждлго магаза найти клиентов с макс. скидоном
            // магаз клиент год рождения и скидон

            var res = A.Join(C, sel => sel.IdClient, cr => cr.IdClient, (a, c) => new { c.Shop, c.IdClient, a.Year, c.Sell })
                .Where(w => w.Sell == A.Join(C, sel => sel.IdClient, cr => cr.IdClient, (a, c) => new { c.Shop, c.IdClient, a.Year, c.Sell })
                .Where(d => d.Shop == w.Shop).Max(m => m.Sell))
                .GroupBy(gr => gr.Sell).SelectMany(s => s.OrderBy(d => d.IdClient)
                .Take(1)).OrderBy(or => or.Shop);

            Console.WriteLine(string.Join("\n", res));
        }

        public static void Obj81()
        {
            Console.WriteLine("Obj81");
            var B = new[] //продукты
            {
                new { Article = 2222, Country = "Russia", Category = "food" },
                new { Article = 2228, Country = "Russia", Category = "food" },
                new { Article = 3322, Country = "Russia", Category = "cloth" },
                new { Article = 2323, Country = "Russia", Category = "cloth" },

                new { Article = 4222, Country = "USA", Category = "PC" },
                new { Article = 4223, Country = "USA", Category = "PC" },
                new { Article = 4212, Country = "USA", Category = "food" },
                new { Article = 4228, Country = "USA", Category = "mobile" },

                new { Article = 6332, Country = "Canada", Category = "materials" },
                new { Article = 6322, Country = "Canada", Category = "materials" },
                new { Article = 6342, Country = "Canada", Category = "beer" },
                new { Article = 6352, Country = "Canada", Category = "beer" },
            };

            var D = new[] //цены в магазинах на опре-е продукты
            {
                new { Shop = "Магаз 1", Article = 2222, Cost = 320 },
                new { Shop = "Магаз 1", Article = 2228, Cost = 210 },
                new { Shop = "Магаз 1", Article = 3322, Cost = 320 },//
                new { Shop = "Магаз 1", Article = 2323, Cost = 511 },//
                //
                new { Shop = "Магаз 1", Article = 4222, Cost = 322 },
                new { Shop = "Магаз 1", Article = 4223, Cost = 422 },//
                new { Shop = "Магаз 1", Article = 6322, Cost = 771 },//
                new { Shop = "Магаз 1", Article = 6342, Cost = 911 },


                new { Shop = "Магаз 2", Article = 4222, Cost = 321 },
                new { Shop = "Магаз 2", Article = 4223, Cost = 421 },//
                new { Shop = "Магаз 2", Article = 4212, Cost = 521 },//
                new { Shop = "Магаз 2", Article = 4228, Cost = 323 },
                
                new { Shop = "Магаз 3", Article = 6332, Cost = 721 },
                new { Shop = "Магаз 3", Article = 6322, Cost = 781 },//
                new { Shop = "Магаз 3", Article = 6342, Cost = 921 },//
                new { Shop = "Магаз 3", Article = 6352, Cost = 891 },//
                new { Shop = "Магаз 3", Article = 4212, Cost = 391 },//
                new { Shop = "Магаз 3", Article = 4228, Cost = 491 },//
            };

            var E = new[] // приобретено
            {
                 new {Shop = "Магаз 1", IdCode = 228, Article = 3322 },
                 new {Shop = "Магаз 1", IdCode = 238, Article = 2323 },
                 new {Shop = "Магаз 1", IdCode = 421, Article = 4223 },
                 new {Shop = "Магаз 1", IdCode = 218, Article = 6322 },

                 new {Shop = "Магаз 2", IdCode = 323, Article = 4223 },
                 new {Shop = "Магаз 2", IdCode = 322, Article = 4212 },
                 new {Shop = "Магаз 2", IdCode = 328, Article = 4228 },

                 new {Shop = "Магаз 3", IdCode = 528, Article = 6322 },
                 new {Shop = "Магаз 3", IdCode = 538, Article = 6342 },
                 new {Shop = "Магаз 3", IdCode = 518, Article = 6352 },
                 new {Shop = "Магаз 3", IdCode = 548, Article = 4212 },
                 new {Shop = "Магаз 3", IdCode = 558, Article = 4228 },
            };

            var res = D.Join(E, d => new { d.Shop, d.Article }, c => new { c.Shop, c.Article }, (d, e) => new { d.Shop, e.IdCode, e.Article, d.Cost })
                .Join(B, b => b.Article, ec => ec.Article, (b,ec) => new {ec.Country, b.Shop, b.Article, b.Cost})
                .GroupBy(gr => gr.Country).Select(s => new {s.Key, NumOfProducts = s.Count(), AllCost = s.Sum(sum => sum.Cost)});

            Console.WriteLine(string.Join("\n", res));

            // Для каждой страны определить кол-во приобр-х товаров и суммарную стоимость
            //          страна     кол-во товаров      суммарная стоимость
            // B слить с (D слитoe с Е)
        }

        public static void Obj91()
        {
            Console.WriteLine("Obj91");
            var A = new[] // Потребители
            {
                 new {Street = "Пушкина 11", DateOfBirth = 200, IdCode = 228},
                 new {Street = "Пушкина 11", DateOfBirth = 200, IdCode = 238},
                 new {Street = "Пушкина 11", DateOfBirth = 200, IdCode = 421},
                 new {Street = "Пушкина 14", DateOfBirth = 200, IdCode = 218},
                                        
                 new {Street = "Пушкина 1а", DateOfBirth = 200, IdCode = 323},
                 new {Street = "Пушкина 22", DateOfBirth = 200, IdCode = 322},
                 new {Street = "Пушкина 22", DateOfBirth = 200, IdCode = 328},
                                        
                 new {Street = "Пушкина 14", DateOfBirth = 200, IdCode = 528},
                 new {Street = "Пушкина 3a", DateOfBirth = 200, IdCode = 538},
                 new {Street = "Пушкина 3a", DateOfBirth = 200, IdCode = 518},
                 new {Street = "Пушкина 17", DateOfBirth = 200, IdCode = 548},
                 new {Street = "Пушкина 22", DateOfBirth = 200, IdCode = 558},
            };

            var B = new[] //продукты стран
            {
                new { Article = 2222, Country = "Russia", Category = "food" },
                new { Article = 2228, Country = "Russia", Category = "food" },
                new { Article = 3322, Country = "Russia", Category = "cloth" },
                new { Article = 2323, Country = "Russia", Category = "cloth" },

                new { Article = 4222, Country = "USA", Category = "PC" },
                new { Article = 4223, Country = "USA", Category = "PC" },
                new { Article = 4212, Country = "USA", Category = "food" },
                new { Article = 4228, Country = "USA", Category = "mobile" },

                new { Article = 6332, Country = "Canada", Category = "materials" },
                new { Article = 6322, Country = "Canada", Category = "materials" },
                new { Article = 6342, Country = "Canada", Category = "beer" },
                new { Article = 6352, Country = "Canada", Category = "beer" },
            };
            var E = new[] // приобретено
            {
                 new {Shop = "Магаз 1", Article = 3322, IdCode = 228},
                 new {Shop = "Магаз 1", Article = 2323, IdCode = 238},
                 new {Shop = "Магаз 1", Article = 4223, IdCode = 421},
                 new {Shop = "Магаз 1", Article = 6322, IdCode = 218},
                                                      
                 new {Shop = "Магаз 2", Article = 4223, IdCode = 323},
                 new {Shop = "Магаз 2", Article = 4212, IdCode = 322},
                 new {Shop = "Магаз 2", Article = 4228, IdCode = 328},

                 new {Shop = "Магаз 3", Article = 6322, IdCode = 528},
                 new {Shop = "Магаз 3", Article = 6342, IdCode = 538},
                 new {Shop = "Магаз 3", Article = 6352, IdCode = 518},
                 new {Shop = "Магаз 3", Article = 4212, IdCode = 548},
                 new {Shop = "Магаз 3", Article = 4228, IdCode = 558},
            };
            // улица проживания     категория товара    кол-во стран
            var res = B.Join(E, d => d.Article, c => c.Article, (d, e) => new { d.Article, d.Category, d.Country, e.IdCode })
                .Join(A, b => b.IdCode, ec => ec.IdCode, (b, ec) => new { b.Article, b.Category, b.Country, b.IdCode, ec.Street})
                .GroupBy(gr => new {gr.Street, gr.Category }).Select(s => new { s.Key.Street, s.Key.Category, NumOfCountries = s.Count()});
            Console.WriteLine(string.Join("\n", res));
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
            Console.WriteLine("\n ######################### \n");
            Obj71();
            Console.WriteLine("\n ######################### \n");
            Obj81();
            Console.WriteLine("\n ######################### \n");
            Obj91();
            Console.ReadKey();
        }
    }
}
