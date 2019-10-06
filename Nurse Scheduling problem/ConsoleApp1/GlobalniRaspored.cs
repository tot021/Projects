using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace ConsoleApp1
{
    public class GlobalniRaspored
    {
        public Sestra[,] gl = new Sestra[4, 7];
        

        public GlobalniRaspored()
        {
            prazniRaspored();
        }
        public void prazniRaspored()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    gl[i, j] = null;
                    
                }
            }
        }
        public bool proveraRasporeda()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (gl[i, j] == null) return false;
                }
            }
            return true;
        }
        public void printaj()
        {
            var table = new ConsoleTable("Smena", "Ponedeljak", "Utorak", "Sreda", "Cetvrtak", "Petak", "Subota", "Nedelja");
            table.AddRow("Prepodne", $"{gl[0, 0].getImeIPrezime()}", $"{gl[0, 1].getImeIPrezime()}", $"{gl[0, 2].getImeIPrezime()}", $"{gl[0, 3].getImeIPrezime()}", $"{ gl[0, 4].getImeIPrezime()}", $"{ gl[0, 5].getImeIPrezime()}", $"{ gl[0, 6].getImeIPrezime()}")

                .AddRow("Poslepodne", $"{gl[1, 0].getImeIPrezime()}", $"{gl[1, 1].getImeIPrezime()}", $"{gl[1, 2].getImeIPrezime()}", $"{gl[1, 3].getImeIPrezime()}", $"{ gl[1, 4].getImeIPrezime()}", $"{ gl[1, 5].getImeIPrezime()}", $"{ gl[1, 6].getImeIPrezime()}")
                .AddRow("Uvece", $"{gl[2, 0].getImeIPrezime()}", $"{gl[2, 1].getImeIPrezime()}", $"{gl[2, 2].getImeIPrezime()}", $"{gl[2, 3].getImeIPrezime()}", $"{ gl[2, 4].getImeIPrezime()}", $"{ gl[2, 5].getImeIPrezime()}", $"{ gl[2, 6].getImeIPrezime()}")
                .AddRow("Off", $"{gl[3, 0].getImeIPrezime()}", $"{gl[3, 1].getImeIPrezime()}", $"{gl[3, 2].getImeIPrezime()}", $"{gl[3, 3].getImeIPrezime()}", $"{ gl[3, 4].getImeIPrezime()}", $"{ gl[3, 5].getImeIPrezime()}", $"{ gl[3, 6].getImeIPrezime()}"); 
            table.Write();
            Console.WriteLine();
        } 
    }
}
