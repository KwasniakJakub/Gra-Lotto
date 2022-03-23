using System;
using System.Collections.Generic;
using System.Linq;

namespace Gra_Lotto
{
    //Uczestnik ma możliwość skreślenia 6 liczb w przedziale 1-49
    //Komputer losuje również 6 liczb (kule)
    //Sprawdza dopasowanie losów (max 8 losów w 1 grze)
    //Losy się nie sumują
    //Skreślenie 6 trafionych liczb daje tzw. kumulacje
    //Im więcej trafimy cyfr, tym większa wygrana
    //Najniższa wygrana to 24zł za 3 trafione liczby
    internal class Program
    {
        static int kumulacja; //Na każdą kumulację losowana
        static int START = 30; //Wartość początkowa naszego portfela
        static Random rnd = new Random();//Zmienna która będzie służyła za losowanie
        static void Main(string[] args)
        {
            int pieniadze = START;//Wartosc naszych pieniedzy
            int dzien = 0;  //Informacja na temat ile dni graliśmy
            do
            {
                pieniadze = START;  //Restartujemy pieniądze na domyslne 30
                dzien = 0;
                ConsoleKey wybor;   //Zmienna do przechowywania 
                //pętla stanowiąca nowy dzień
                do
                {
                    kumulacja = rnd.Next(2,37) * 1000000;
                    dzien++;
                    int losow = 0;//Ile losow postawilem w dany dzien
                    List<int[]> kupon = new List<int[]>();//Lista przyjmujaca jako argument tablice
                    //Pętla stanowiąca wybór w danym dniu
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("DZIEŃ: {0}", dzien);
                        Console.WriteLine("Witaj w grze LOTTO, dziś do wygrania jest aż {0} zł", kumulacja);
                        Console.WriteLine("\nStan konta: {0} zł", pieniadze);
                        WyswietlKupon(kupon);
                        //MENU
                        if (pieniadze >= 3 && losow < 8)
                        {
                            Console.WriteLine("1 - Postaw los - 3zł [{0}/8]", losow + 1);
                        }
                        Console.WriteLine("2 - Sprawdź kupon - losowanie");
                        Console.WriteLine("3 - Zakończ grę");
                        //MENU
                        wybor = Console.ReadKey().Key;
                        if (wybor == ConsoleKey.D1 && pieniadze >= 3 && losow < 8)
                        {
                            kupon.Add(PostawLos());
                            pieniadze -= 3;
                            losow++;
                        }
                    } while (wybor == ConsoleKey.D1);
                    Console.Clear();
                    if (kupon.Count > 0)  //Sprawdzenie czy mielismy postawiony jakikolwiek los
                    {
                        int wygrana = Sprawdz(kupon);
                        if(wygrana > 0)//Jeśli wygralismy
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nBrawo wygrałeś {0}zł w tym losowaniu !", wygrana);
                            Console.ResetColor();
                            pieniadze += wygrana;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nNiestety, nic nie wygrałeś.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nie miałeś losów w tym losowaniu");
                    }
                    Console.WriteLine("Enter - kontynuuj.");
                    Console.ReadKey(); 
                } while (pieniadze >= 3 && wybor != ConsoleKey.D3);//1 kupon kosztuje 3zł

                Console.Clear();
                Console.WriteLine("DZIEŃ {0}.\n Koniec gry, twój wynik to: {1}zł",
                    dzien, pieniadze - START);  //Wypisywanie statystyk po szkończeniu gry
                Console.WriteLine("Enter - graj od nowa");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);//Wykonuje się dopóki nie nastąpi naciśnięcie klawisza Enter
        }

        private static int Sprawdz(List<int[]> kupon)
        {
            throw new NotImplementedException();
        }

        private static int[] PostawLos()
        {
            int[] liczby = new int[6];
            int liczba = -1;
            for (int i = 0; i < liczby.Length; i++)
            {
                Console.Clear();
                Console.Write("Postawione liczby: ");
                foreach (int l in liczby)
                {
                    if(l>0)
                    {
                        Console.WriteLine(l + ", ");
                    }
                }
                Console.WriteLine("\n\nWybierz liczbę od 1 do 49: ");
                Console.Write("{0}/6: ", i + 1);
                bool prawidlowa = int.TryParse(Console.ReadLine(), out liczba);
                if (prawidlowa && liczba >= 1 && liczba <= 49 && !liczby.Contains(liczba))
                {
                    liczby[i] = liczba;
                }
                else
                {
                    Console.WriteLine("Niestety, błędna liczba");
                    i--;
                    Console.ReadKey();
                }
            }
            Array.Sort(liczby);
            return liczby;
        }

        private static void WyswietlKupon(List<int[]> kupon)
        {
            if (kupon.Count == 0)
            {
                Console.WriteLine("Nie postawiłeś jeszcze żadnego losu");
            }
            else
            {
                int i = 0;
                Console.WriteLine("\nTWÓJ KUPON:");
                foreach (int[] los in kupon)
                {
                    i++;
                    Console.WriteLine(i + ": ");
                    foreach (int liczba in los)
                    {
                        Console.WriteLine(liczba + ", ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
