using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Clear();
        Console.Write("Adja meg a nevét: ");
        string playerName = "";
        while (playerName == "")
        {
            playerName = Console.ReadLine() ?? "";
        }
        Console.WriteLine();
        while (true)
        {
            int input = -1;
            while (input == -1)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.WriteLine("-== Mastermind ==-");
                Console.WriteLine("Válassz egy lehetőséget:");
                Console.WriteLine(" 1. - Új játék");
                Console.WriteLine(" 2. - Ranglista");
                Console.WriteLine(" 3. - Szabályok");
                Console.WriteLine(" 0. - Kilépés");

                try
                {
                    input = Convert.ToInt32(Console.ReadKey(true).KeyChar) - 48;
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Kérlek egy számot adj meg!");
                    continue;
                }
            }

            switch (input)
            {
                case 1:
                    try
                    {
                        await GameLogic.StartNewGame(playerName);
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Hiba történt a játék indításakor: " + ex.Message);
                        Console.ReadKey(true);
                    }
                    break;
                case 2:
                    LeaderboardLogic.ViewLeaderboards();
                    break;
                case 3:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    Console.WriteLine("A játék célja, hogy kitaláld a titkos színkombinációt, mely 4 színből áll.");
                    Console.WriteLine("A színek a következők lehetnek:");
                    Console.WriteLine("  k - kék, z - zöld, p - piros, s - sárga, l - lila, f - fehér");
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
