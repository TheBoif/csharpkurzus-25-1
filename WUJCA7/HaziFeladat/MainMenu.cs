using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Console.ResetColor();
        Console.Clear();
        Console.WriteLine();
        while (true)
        {
            short input = -1;
            while (input == -1)
            {
                Console.ResetColor();
                Console.WriteLine("-== Mastermind ==-");
                Console.WriteLine("Válassz egy lehetőséget:");
                Console.WriteLine(" 1. - Új játék");
                Console.WriteLine(" 2. - Ranglista");
                Console.WriteLine(" 3. - Szabályok");
                Console.WriteLine(" 0. - Kilépés");

                try
                {
                    input = Convert.ToInt16(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Kérlek egy számot adj meg!");
                    continue;
                }
                catch (OverflowException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A megadott szám túl nagy vagy túl kicsi!");
                    continue;
                }

            }

            switch (input)
            {
                case 1:
                    GameLogic.StartNewGame("asd");
                    Console.Clear();
                    break;
                case 2:
                    LeaderboardLogic.ViewLeaderboards();
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("A játék célja, hogy kitaláld a titkos színkombinációt, mely 4 színből áll.");
                    Console.WriteLine("A színek a következők lehetnek:");
                    Console.WriteLine("  k - kék, z - zöld, p - piros, s - sárga, l - lila, n - narancs");
                    Console.WriteLine("A színkombinációban lehetnek ismétlődő színek, és a sorrendjük is számít.");
                    Console.WriteLine("Minden tipp után kapsz egy visszajelzést, ami megmutatja, hogy a tippelt színek közül hány helyes, de rossz helyen (fehér) vagy helyes szín, és jó helyen (fekete).");
                    Console.WriteLine("A játék addig tart, amíg ki nem találod a színkombinációt, vagy a 10. tippre sem sikerül.");
                    Console.ReadKey(true);
                    break;
                case 0:
                    return;
                default:
                Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A megadott parancs nem érvényes.");
                    break;
            }
        }
    }
}
