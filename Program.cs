using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dwarf_game
{
    class Program
    {
        static void Main()
        {
            Random r = new Random();
            int[,] rockpos = new int[Console.WindowHeight, Console.WindowWidth];
            
            int playfield = Console.WindowWidth - 20;
            int dwarfx = playfield / 2;
            int dwarfy = Console.WindowHeight - 1;
            var score = Stopwatch.StartNew();
            string dwarflook = "(O)";
            ConsoleColor dwarfcolor = ConsoleColor.Green;
            bool isactive = true;
            while (isactive)

            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkGray;
                //movment
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo pressedkey = Console.ReadKey(true);
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    if (pressedkey.Key == ConsoleKey.LeftArrow)
                    {
                        if (dwarfx - 1 >= 0)
                            dwarfx -= 2;
                    }
                    if (pressedkey.Key == ConsoleKey.RightArrow)
                    {
                        if (dwarfx + 3 < playfield)
                            dwarfx += 2;
                    }

                }



                //implement rocks
                int numberofrocks = r.Next(1, 3);
                for (int i = 0; i < numberofrocks; i++)
                {
                    int rocklenght = r.Next(1, 3);
                    int positionx = r.Next(0, playfield);
                    for (int j = 0; j < rocklenght; j++) rockpos[0, positionx + j] = 0;
                    for (int j = 0; j < rocklenght; j++)
                    {
                        rockpos[0, positionx + j] = 1;
                    }
                }
                //    implement rocks falling
                for (int i = Console.WindowHeight - 1; i >= 0; i--)
                {
                    for (int j = 0; j < playfield; j++)
                    {
                        if (rockpos[i, j] == 1)
                        {
                            rockpos[i, j] = 0;
                            rockpos[i + 1, j] = 1;
                        }
                    }

                }
                //collisions
                bool collision;
                int count = 0;
                for (int c = 0; c < playfield; c++)
                {
                    if ((rockpos[Console.WindowHeight - 1, c] == 1) && (dwarfx == c || dwarfx + 1 == c || dwarfx + 2 == c))
                    {
                        count++;
                    }

                }
                if (count == 0)
                    collision = false;
                else
                    collision = true;



                //draw dwarf
                Console.SetCursorPosition(dwarfx, dwarfy);
                Console.ForegroundColor = dwarfcolor;
                Console.Write(dwarflook);


                //draw rocks
                for (int i = 0; i < Console.WindowHeight; i++)
                {
                    for (int j = 0; j < playfield; j++)
                    {
                        if (rockpos[i, j] == 1)
                        {
                            Console.SetCursorPosition(j, i);
                            Console.ForegroundColor = randomcolor(r);
                            Console.Write("{0}", rocksshape(r));
                        }
                    }
                }
                //vanishing
                for (int j = 0; j < playfield; j++)
                    rockpos[Console.WindowHeight - 1, j] = 0;
                //implement score
                Console.SetCursorPosition(playfield + 1, 3);
                Console.Write("the score is {0}", score.ElapsedMilliseconds / 10);

                //speed
                Thread.Sleep(150);
                //end game
                if (collision)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    string s = "YOU DIED                                ";
                    Console.WriteLine(s+"\n\nyour score is {0}", score.ElapsedMilliseconds / 10);
                    isactive = false;
                    Console.ReadLine();
                }

            }
        }
        private static char rocksshape(Random r)
        {
            char[] rocks = { '^', '@', '*', '&', '+', '%', '$', '#', '!', '.', ':' };
            int randrocks = r.Next(0, rocks.Length);
            return rocks[randrocks];
        }

        private static ConsoleColor randomcolor(Random r)
        {
            ConsoleColor[] colors = { ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Gray };
            int rand = r.Next(0, colors.Length);
            return colors[rand];

        }
    }
}
