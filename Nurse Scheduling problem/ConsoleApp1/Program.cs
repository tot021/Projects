using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Sestra nurse1 = new Sestra("Jelena", "Vasic");
            Sestra nurse2 = new Sestra("Marija", "Majkic");
            Sestra nurse3 = new Sestra("Dragana","Tomcic");
            Sestra nurse4 = new Sestra("Snezana", "Subasic");

            GlobalniRaspored globalniRaspored = new GlobalniRaspored();
            

            List<Sestra> sestre = new List<Sestra>();
            sestre.Add(nurse1);
            sestre.Add(nurse2);
            sestre.Add(nurse3);
            sestre.Add(nurse4);
            Functions fun = new Functions();

            do
            {
                fun.pocetak(sestre, globalniRaspored);
                for (int k = 0; k < 2; k++)
                {
                    for (int i = 0; i < 7; i++)
                    { //prva petlja je za dane {PON,UT...} 
                        for (int j = 0; j < 3; j++)
                        { //druga je za smene {PREPO,POP,UVE,OFF}
                            if (k == 0)
                            {
                                fun.shuffleSestre(sestre);

                                if (globalniRaspored.gl[j, i] == null)
                                {
                                    fun.DodajPrvuSlobodnuSestru(sestre, globalniRaspored, j, i);
                                }
                            }
                            else if (k > 0)
                            {


                                if (globalniRaspored.gl[j, i] == null)
                                {
                                    fun.TraziSlobodnu(sestre, globalniRaspored, j, i);
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < sestre.Count; i++)
                {
                    //proveri raspored svake sestre pojedinacno
                    fun.proveriRasporedSestre(sestre[i].raspored);
                } 
            } while (!fun.krajnjaProvera(sestre, globalniRaspored));


            
            for (int i = 0; i < sestre.Count; i++)
            {
                Console.WriteLine();
                sestre[i].printaj();
                
            }
          
            Console.WriteLine("************************Globalni raspored***********************");
            globalniRaspored.printaj();
            Console.ReadKey();
        }

    }

}


