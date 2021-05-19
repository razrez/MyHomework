using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp
{




    class Program
    {
        static void Main(string[] args)
        {
            for(int k = 0; k < 3; k++)
            {
                try
                {
                    PowerStation ps1 = new PowerStation("Station 1", 100, 1500, 100);
                    ps1.Boom += FireFighters.Work;
                    ps1.Boom += Ambulance.Work;
                    ps1.Boom += Police.Work;
                    for (int i = 0; i < 20; i++)
                    {
                        ps1.TempUp();
                        Console.WriteLine(ps1);
                    }

                    PowerStation ps2 = new PowerStation("Station 2", 300, 1000, 150);
                    ps2.Boom += FireFighters.Work;
                    ps2.Boom += Ambulance.Work;
                    ps2.Boom += Police.Work;
                    for (int i = 0; i < 20; i++)
                    {
                        ps2.TempUp();
                        Console.WriteLine(ps2);
                    }

                    PowerStation ps3 = new PowerStation("Station 3", 500, 2000, 200);
                    ps3.Boom += FireFighters.Work;
                    ps3.Boom += Ambulance.Work;
                    ps3.Boom += Police.Work;
                    for (int i = 0; i < 20; i++)
                    {
                        ps2.TempUp();
                        Console.WriteLine(ps2);
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadKey();
                Console.WriteLine();
            }
            
        }

    }
}