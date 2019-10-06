using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public class Functions
    {

        public const int MIN_PREPODNE = 2;
        public const int MIN_POSLEPODNE = 2;
        public const int MIN_UVECE = 1;
        public const int MAX_PREPODNE = 2;
        public const int MAX_POSLEPODNE = 2;
        public const int MAX_UVECE = 2;
        public static Random rng = new Random();

        public int radiLiNekaSestDana = 0;
        public void pocetak(List<Sestra> sestre, GlobalniRaspored globalniRaspored)
        {
            for(int i = 0;i<sestre.Count;i++)
            {
                sestre[i].prazniRaspored();
            }
            globalniRaspored.prazniRaspored();
            radiLiNekaSestDana = 0;
        }
        public void shuffleSestre(List<Sestra> sestre)
        {
            
            int br = sestre.Count;
            while (br > 1)
            {
                br--;
                int random = rng.Next(br + 1);
                Sestra tmp = sestre[random];
                sestre[random] = sestre[br];
                sestre[br] = tmp;
            }
        }





        public int proveriSestruPoslepodneiUvece(int[,] sestra, int red, int kolona)
        {
            if (kolona == 0) //proverava trenutni dan i sledeci jer je u pitanju ponedeljak
            {
                if (sestra[red, kolona] == 1 && sestra[red, kolona + 1] == 1)
                {
                    return 1;
                }
            }
            else //proverava trenutni, prethodni i sledeci
            {

                if (sestra[red, kolona] == 1 && sestra[red, kolona + 1] == 1)
                {
                    return 1;
                }
                else if (sestra[red, kolona] == 1 && sestra[red, kolona - 1] == 1)
                {
                    return -1;
                }
                //ne moram praviti return false jer ako ne nadje ovde samo ce izadji pa je return false automatski;
            }
            return 0;
        }

        public int proveriSestruPrepodne(int[,] niz)
        {
            int suma = 0;
            for (int i = 0; i < 7; i++)
            {
                suma += niz[0, i];
            }
            if (suma < MIN_PREPODNE)
            {
                return 0; //vraca 0 u slucaju da sestra nema ispunjen minimalan broj smena
            }
            else if (suma >= MIN_PREPODNE && suma < MAX_PREPODNE)
            {
                return 1; //vraca 1 ako je izmedju minimuma i maximuma 
            }
            else if (suma == MAX_PREPODNE)
            {
                return -1; // vraca -1 u slucaju da sestra ima ispunjen maximalan broj smena prepodne u toku nedelje
            }

            return -1;
        }

        public int proveriSestruPopodne(int[,] niz)
        {
            int suma = 0;
            for (int i = 0; i < 7; i++)
            {
                suma += niz[1, i];
            }
            if (suma < MIN_POSLEPODNE)
            {
                return 0; //vraca 0 u slucaju da sestra nema ispunjen minimalan broj smena
            }
            else if (suma >= MIN_POSLEPODNE && suma < MAX_POSLEPODNE)
            {
                return 1; //vraca 1 ako je izmedju minimuma i maximuma 
            }
            else if (suma == MAX_POSLEPODNE)
            {
                return -1; // vraca -1 u slucaju da sestra ima ispunjen maximalan broj smena prepodne u toku nedelje
            }

            return -1;
        }
        public int proveriSestruUvece(int[,] niz)
        {
            int suma = 0;
            for (int i = 0; i < 7; i++)
            {
                suma += niz[2, i];
            }
            if (suma < MIN_UVECE)
            {
                return 0; //vraca 0 u slucaju da sestra nema ispunjen minimalan broj smena
            }
            else if (suma >= MIN_UVECE && suma < MAX_UVECE)
            {
                return 1; //vraca 1 ako je izmedju minimuma i maximuma 
            }
            else if (suma == MAX_UVECE)
            {
                return -1; // vraca -1 u slucaju da sestra ima ispunjen maximalan broj smena prepodne u toku nedelje
            }

            return -1;
        }

        public bool proveriSestruTajDan(int[,] niz, int kolona)
        {
            for (int i = 0; i < 4; i++)
            {
                if (niz[i, kolona] == 1) return true;
            }
            return false;
        }

        public int IzracunajSumu(int[,] niz)
        {
            int suma = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    suma += niz[i, j];
                }
            }
            return suma;
        }


        public void DodajPrvuSlobodnuSestru(List<Sestra> sestre, GlobalniRaspored globalniRaspored, int red, int kolona) 
        {
            //tu treba sve uslove da dodam

            //valjalo bi kada bi napravio zastite neke za red i kolonu da ne bi dobijao greske indexOutOfRange
 
            for (int i = 0; i < sestre.Count; i++)
            {
                int[,] niz = sestre[i].raspored;
                //DA SESTRA RADI 5 ili 6 dana u nedelji
                #region PRVI USLOV 
                int suma = IzracunajSumu(niz);

                if (suma == 6)
                {
                    radiLiNekaSestDana = 1;
                }

                if (suma == 5 || suma == 6)
                {
                    
                    if(radiLiNekaSestDana != 0)
                    {
                        offdays(sestre[i].raspored);
                    }
                    //prvi uslov nije zadovoljen, ova sestra je radila dovoljno ove nedelje, ostale dane joj treba popuniti sa OFF dayovima
                    //napraviti funkciju koja dodaje off-dayove na ostatak
                  //  offdays(niz);//***************************************IDEJA OVO ZAKOMENTARISATI
                    continue;
                    //samo da predje odma na sledecu sestru
                }



                #endregion

                //da li sestra radi trenutnu smenu
                #region DRUGI USLOV 

                if (niz[red, kolona] == 1)
                {
                    continue; // to nam govori da ova sestra radi ovu smenu
                }



                #endregion

                //provera da li je sestra radila dovoljan broj dana u odredjenoj smeni
                #region TRECI USLOV
                int rez = 0;
                if (red == 0)
                {
                    rez = proveriSestruPrepodne(niz);
                } //pozivanje funkcije prepodne
                else if (red == 1)
                {
                    rez = proveriSestruPopodne(niz);
                } //pozivanje funkcije poslepodne
                else if (red == 2)
                {
                    rez = proveriSestruUvece(niz);
                } //pozivanje funkcije uvece
                else
                {
                    rez = 43; //offday al 43 je random broj cisto da preskocimo sledece ifove iako ne bi trebalo uopste da se poziva ova funkcija
                } //else nam je offday



                if (rez == -1)
                {
                    //popunila sve dane
                    continue; //predji na sledeci
                }


                #endregion

                //****************************************************************************************


                if (proveriSestruTajDan(niz, kolona))
                {
                    continue; //sestra vec radi taj dan neku smenu, treba preci na sledecu
                }

                //***************************************************************************************

                //u slucaju da je smena poslepodne ili uvece sledeci ili prethodni dan mora biti isto poslepodne ili uvece
                #region CETVRTI USLOV
                if (red == 1 || red == 2)
                {
                    int provera1 = proveriSestruPopodne(niz);
                    int provera2 = proveriSestruUvece(niz);

                    if (red == 1)
                    {


                        if (provera1 == 0)
                        {

                            //fali jos jos smena popodne

                            //TODO DODELITI JOJ SMENU POSLEPODNE I UPISATI U GLOBALNI RASPORED
                            sestre[i].raspored[red, kolona] = 1;
                            globalniRaspored.gl[red, kolona] = sestre[i];
                            
                            PostaviNaPoslednjeMesto(sestre, i);

                            return;


                        }
                        else if (provera1 == 1)
                        {
                            //izmedju minimuma i maximuma
                            int danas = proveriSestruPoslepodneiUvece(niz, red, kolona);
                            if (danas == 1)
                            {
                                if (niz[red, kolona - 1] == 0)
                                {
                                    if (proveriSestruTajDan(niz, kolona - 1))
                                    {
                                        //TODO DODELITI JOJ SMENU POSLEPODNE I UPISATI U GLOBALNI RASPORED
                                        sestre[i].raspored[red, kolona - 1] = 1;
                                        globalniRaspored.gl[red, kolona - 1] = sestre[i];
                                        
                                        PostaviNaPoslednjeMesto(sestre, i);
                                        return;
                                    }

                                }
                            }
                            else if (danas == -1)
                            {
                                if (niz[red, kolona + 1] == 0)
                                {
                                    if (proveriSestruTajDan(niz, kolona + 1))
                                    {
                                        //TODO DODELITI JOJ SMENU POSLEPODNE I UPISATI U GLOBALNI RASPORED
                                        sestre[i].raspored[red, kolona + 1] = 1;
                                        globalniRaspored.gl[red, kolona + 1] = sestre[i];
                                        
                                        PostaviNaPoslednjeMesto(sestre, i);
                                        return;
                                    }
                                }
                            }
                        }
                        else if (provera1 == -1)
                        {
                            continue; //smene za popodne su joj popunjene cao zdravo idemo na sledecu sestru

                        }
                    }

                    else if (red == 2)
                    {
                        if (provera2 == 0)
                        {
                            //fali jos jos smena uvece
                            
                                //TODO DODELITI JOJ SMENU uvece I UPISATI U GLOBALNI RASPORED
                                
                        
                            sestre[i].raspored[red, kolona] = 1;
                            globalniRaspored.gl[red, kolona] = sestre[i];
                            
                            PostaviNaPoslednjeMesto(sestre, i);
                            return;
                        }
                        else if (provera2 == 1)
                        {
                            int danas = proveriSestruPoslepodneiUvece(niz, red, kolona);
                            if (danas == 1)
                            {
                                
                                
                                    if (niz[red, kolona - 1] == 0)
                                    {
                                        if (proveriSestruTajDan(niz, kolona - 1))
                                        {
                                        //TODO DODELITI JOJ SMENU uvece I UPISATI U GLOBALNI RASPORED



                                            sestre[i].raspored[red, kolona - 1] = 1;
                                            globalniRaspored.gl[red, kolona - 1] = sestre[i];
                                            
                                            PostaviNaPoslednjeMesto(sestre, i);
                                            return;
                                        }

                                    }
                                
                            }
                            else if (danas == -1)
                            {
                                if (niz[red, kolona + 1] == 0)
                                {
                                    if (proveriSestruTajDan(niz, kolona + 1))
                                    {

                                        //TODO DODELITI JOJ SMENU uvece I UPISATI U GLOBALNI RASPORED
                                        sestre[i].raspored[red, kolona + 1] = 1;
                                        globalniRaspored.gl[red, kolona + 1] = sestre[i];
                                        
                                        PostaviNaPoslednjeMesto(sestre, i);
                                        return;
                                    }
                                }
                            }
                            else if (provera1 == -1)
                            {
                                continue; //smene za uvece su joj popunjene cao zdravo idemo na sledecu sestru

                            }
                        }
                    }

                }





                #endregion
                else
                {
                    sestre[i].raspored[red, kolona] = 1;
                    globalniRaspored.gl[red, kolona] = sestre[i];
                    
                    PostaviNaPoslednjeMesto(sestre, i);
                    return;
                }
            }

            return;
        }

        public void PostaviNaPoslednjeMesto(List<Sestra> sestre, int stariIndex)
        {
            int noviIndeks = sestre.Count - 1;
            Sestra tmp = sestre[noviIndeks];
            sestre[noviIndeks] = sestre[stariIndex];
            sestre[stariIndex] = tmp;
        }

       

       
        public void offdays(int[,] niz)
        {
            for (int i = 0; i < 7; i++)
            {
                int suma = 0;
                for (int j = 0; j < 3; j++)
                {
                    suma += niz[j, i];
                }
                if (suma == 0)
                {
                    niz[3, i] = 1; //nema smenu taj dan dacemo joj slobodan dan
                }
            }
        }

        public int SumaSaOffDayovima(int[,] niz)
        {
            int suma = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    suma += niz[i, j];
                }
            }
            return suma;
        }
        public void proveriRasporedSestre(int[,] niz)
        {
            if (SumaSaOffDayovima(niz) == 7)
            {
                //sve je ok sa sestrom 
                return;
            }
            else if (IzracunajSumu(niz) == 6)
            {
                offdays(niz);
                return;
            }
            else if (IzracunajSumu(niz) == 5)
            {
                offdays(niz);
                return;
            }
            

        } //u sustini samo dodeljulje offdayove sestrama koje vec imaju 5 ili 6 dana


        public void TraziSlobodnu(List<Sestra> sestre,GlobalniRaspored globalniRaspored, int red, int kolona)
        {
            for(int i = 0; i< sestre.Count;i++)
            {
                int[,] niz = sestre[i].raspored;
                if (SumaSaOffDayovima(niz) == 7)
                {
                    continue;
                }
                else if (IzracunajSumu(niz) == 6)
                {
                    radiLiNekaSestDana = 1;
                    offdays(sestre[i].raspored);
                    continue;
                }
                else if (IzracunajSumu(niz) <= 5)
                {
                   if(IzracunajSumu(niz) == 5 && radiLiNekaSestDana != 0)
                   {
                        offdays(sestre[i].raspored);
                        continue;
                   } 
                   if (niz[red, kolona] == 0)
                    {
                        if (!proveriSestruTajDan(niz, kolona))
                        {
                            if (globalniRaspored.gl[red, kolona] == null)
                            {
                                if(red == 0)
                                {
                                    int br = proveriSestruPrepodne(niz);
                                    if(br == -1)
                                    {
                                        continue;
                                    }
                                }
                                else if (red == 1)
                                {
                                    int br = proveriSestruPopodne(niz);
                                    if (br == -1)
                                    {
                                        continue;
                                    }
                                }
                                else if (red == 2)
                                {
                                    int br = proveriSestruUvece(niz);
                                    if (br == -1)
                                    {
                                        continue;
                                    }
                                }
                                sestre[i].raspored[red, kolona] = 1;

                                globalniRaspored.gl[red, kolona] = sestre[i];
                                
                                if (IzracunajSumu(niz) == 6)
                                {
                                    radiLiNekaSestDana = 1;
                                    offdays(sestre[i].raspored);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool krajnjaProvera(List<Sestra> sestre, GlobalniRaspored globalniRaspored)
        {
           
            if(!globalniRaspored.proveraRasporeda())
            {
                return false;
            }

            for(int i = 0; i<sestre.Count;i++)
            {
                if(SumaSaOffDayovima(sestre[i].raspored) != 7)
                {
                    return false;
                }
            }
            obeleziSve(sestre, globalniRaspored);
            return true;
        }
        public void obeleziSve(List<Sestra> sestre, GlobalniRaspored globalniRaspored)
        {
            for(int i = 0;i< sestre.Count;i++)
            {
                for(int j = 0;j<7;j++)
                {
                   if(sestre[i].raspored[3,j] == 1 )
                   {
                        if(globalniRaspored.gl[3,j] == null)
                        {
                            globalniRaspored.gl[3, j] = sestre[i];
                        }
                   }
                }
            }
        }

    }
}
