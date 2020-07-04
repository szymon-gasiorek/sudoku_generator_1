using System;
using System.Collections.Generic;


namespace sudoku_proba1
{
    class Solver
    {
         Random r1 = new Random();
        //  int ilosc_rozw = 0;


        List<int> zakres_liczb = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, };


        int[,] sudoku2 = new int[9, 9];

        int[,] sudoku = new int[9, 9]
        {
                {0 , 0 , 0,      0 , 0 , 0,      0 , 0 , 0},
                {0 , 0 , 0,      0 , 0 , 0,      0 , 0 , 0},
                {0 , 0 , 0,      0 , 0 , 0,      0 , 0 , 0},

                {0 , 0 , 0,      0 , 0 , 0,      0 , 0 , 0},
                {0 , 0 , 0,      0 , 0 , 0,      0 , 0 , 0},
                {0 , 0 , 0,      0 , 0 , 0,      0 , 0 , 0},

                {0 , 0 , 0,      0 , 0 , 0,      0 , 0 , 0},
                {0 , 0 , 0,      0 , 0 , 0,      0 , 0 , 0},
                {0 , 0 , 0,      0 , 0 , 0,      0 , 0 , 0}
        };


        
        public bool spr_rzad(int y, int liczba)
        {
            //sprawdzanie czy w rzedzie nie ma juz takiej samej liczby
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[y, i] == liczba)
                {
                    return false;
                }
            }
            return true;
        }

        public bool spr_kolumna(int x, int liczba)
        {
            //sprawdzanie czy w kolumnie nie ma juz takiej samej liczby
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[i, x] == liczba)
                {
                    return false;
                }
            }
            return true;
        }

        public bool spr_blok(int y, int x, int liczba)
        {
            //sprawdzanie czy w jednym bloku 3x3 nie ma takiej samej liczby
            int blok_x = x / 3 * 3;
            int blok_y = y / 3 * 3;

            for (int i = blok_y; i < blok_y + 3; i++)
            {
                for (int j = blok_x; j < blok_x + 3; j++)
                {
                    if (sudoku[i, j] == liczba)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool spr_wszystko(int y, int x, int liczba)
        {
            //sprawdza wszystkie poprzednie warunki
            if (spr_rzad(y, liczba) == true && spr_kolumna(x, liczba) == true && spr_blok(y, x, liczba) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      

        public bool rozwiazanie_okienka(int y, int x, int[,] sud)
        {
           
            // rozwiazanie pojedynczego pola
            foreach (int n in zakres_liczb)
            {
                int a = r1.Next(0, 9);
                if (spr_wszystko(y, x, zakres_liczb[a]) == true)
                {
                    sud[y, x] = zakres_liczb[a];
                    if (rozwiazanie_calosc(sud)==true)
                    {
                        return true;
                    }
                }
            }

            sud[y, x] = 0;
            return false;
        }

        public bool rozwiazanie_calosc(int[,] sud)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
          

           
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sud[i, j] == 0)
                    {
                        return rozwiazanie_okienka(i, j,sud);
                    }
                }
            }
           
            //Array.Copy(sud, 0, sudoku2, 0, sudoku.Length);
            //ilosc_rozw++;
            watch.Stop();
            var czas = watch.Elapsed;
            Console.WriteLine();
            Console.WriteLine("Czas wygenerowania sudoku: " + czas);
            return true;
        }

       
        public void usuwanie_losowe(int[,] sud)
        {
            //usuwa losowe liczby z sudoku
            int i, j;
           
            for (int k = 0; k < 71; k++)
            {
                i = r1.Next(0, 9);
                j = r1.Next(0, 9);

                if (sud[i, j] != 0)
                {                
                    sud[i, j] = 0;
                }
             
            }

        }
       

       

        public void pokaz_sudoku(int[,] sud)
        {
            //wyswietlenie sudoku
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0 && i != 0)
                {
                    Console.WriteLine("--------------------------");
                }

                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0 && j != 0)
                    {
                        Console.Write(" |  ");
                    }

                    if (j == 8)
                    {
                        Console.Write(sud[i, j] + " ");
                        Console.WriteLine("");
                    }

                    else
                    {
                        Console.Write(sud[i, j] + " ");
                    }
                }
            }
        }

        

        static void Main(string[] args)
        {
            Console.SetWindowSize(90, 50);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Title = "Sudoku solver";
            Solver pr = new Solver();

           
           Console.WriteLine("                            Pole przed losowaniem");
              pr.pokaz_sudoku(pr.sudoku);
              pr.rozwiazanie_calosc(pr.sudoku);
           Console.WriteLine();

           Console.WriteLine("                            Pole po losowaniu ");
           Console.WriteLine();       
               pr.pokaz_sudoku(pr.sudoku);
              
               pr.usuwanie_losowe(pr.sudoku);
             
            Console.WriteLine();

           Console.WriteLine("                            gotowe sudoku  ");
               pr.pokaz_sudoku(pr.sudoku);
       
            Console.WriteLine();

          
           Console.ReadKey();
        }
    }
}