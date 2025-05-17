using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-== Mastermind ==-");
            Console.WriteLine("Válassz egy lehetőséget:");
            Console.WriteLine(" start - Új játék");
            Console.WriteLine(" ranglista - Ranglista");
            Console.WriteLine(" exit - Kilépés");
            string? input = Console.ReadLine();
            if (input == null) input = string.Empty;

            switch (input)
            {
                case "start":
                    GameLogic.StartNewGame();
                    break;
                case "ranglista":
                    LeaderboardLogic.ViewLeaderboards();
                    break;
                case "exit":
                    return;
                default:
                    Console.WriteLine("A megadott parancs nem érvényes.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
