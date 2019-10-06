using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace ConsoleApp1
{
    public class Sestra
    {
        private string ime;
        private string prezime;
        public int[,] raspored = new int[4, 7];

        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }

       
        public Sestra(string im, string p)
        {
            Ime = im;
            Prezime = p;
            prazniRaspored();
        }

        public void prazniRaspored() 
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    raspored[i, j] = 0;
                }
            }
        }
        public string getImeIPrezime()
        {
            return $"Med. sestra: {Ime} {Prezime}";
        }
        
        public void printaj()
        {
            var table = new ConsoleTable("Smena", "Ponedeljak", "Utorak", "Sreda", "Cetvrtak", "Petak", "Subota", "Nedelja");

            table.AddRow("Prepodne",getter(0,0),getter(0,1), getter(0, 2), getter(0, 3), getter(0, 4), getter(0, 5), getter(0, 6))
                .AddRow("Poslepodne", getter(1, 0), getter(1, 1), getter(1, 2), getter(1, 3), getter(1, 4), getter(1, 5), getter(1, 6))
                .AddRow("Uvece", getter(2, 0), getter(2, 1), getter(2, 2), getter(2, 3), getter(2, 4), getter(2, 5), getter(2, 6))
                .AddRow("Off", getter(3, 0), getter(3, 1), getter(3, 2), getter(3, 3), getter(3, 4), getter(3, 5), getter(3, 6));
                
            table.Write();
            Console.WriteLine();
        }
        public string  getter(int red, int kolona)
        {
            if(raspored[red,kolona] == 1)
            {
                return getImeIPrezime();
            }
            return "";
        }
        public override string ToString()
        {
            string s = $"\nMed. sestra: {Ime} {Prezime}";
            s += "\n";
            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    

                    s +=$"   {raspored[i, j].ToString()}";
                }
                s += "\n\n";
            }


            return s;
        }
    }
}
