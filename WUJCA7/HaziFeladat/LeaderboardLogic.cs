using System;
using System.Collections.Generic;
using System.Linq;

public static class LeaderboardLogic
{
    public static void ViewLeaderboards()
    {
        Console.Clear();
        string filePath = "leaderboard.json";
        var recordsTask = FileReader.ReadLeaderboardAsync(filePath);
        recordsTask.Wait();
        var records = recordsTask.Result;
        if (records.Count == 0)
        {
            Console.WriteLine("Nincs elérhető játék a ranglistán.");
            Console.WriteLine("Nyomj egy gombot a visszatéréshez");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Ranglista rendezése:");
        Console.WriteLine(" 1. Összes Játék");
        Console.WriteLine(" 2. Játékosok átlaga");
        var key = Console.ReadKey(true).KeyChar;
        Console.Clear();
        if (key == '2')
        {
            var playerStats = records
                .Where(r => r.Guesses.Count > 0 && r.Guesses.Last() == r.Solution)
                .GroupBy(r => r.PlayerName)
                .Select(g => new {
                    Player = g.Key,
                    Games = g.Count(),
                    AvgGuesses = g.Average(r => r.Guesses.Count)
                })
                .OrderBy(s => s.AvgGuesses)
                .ToList();
            Console.WriteLine("Játékosok átlagos tippjei szerint rendezve:");
            foreach (var stat in playerStats)
            {
                Console.WriteLine($"{stat.Player}: átlag {stat.AvgGuesses:F2} tipp ({stat.Games} játék)");
            }
        }
        else
        {
            var ordered = records.OrderBy(r => r.Guesses.Count).ToList();
            Console.WriteLine("Összes Játék (legkevesebb tipp szerint):");
            foreach (var record in ordered)
            {
                if (record.Guesses[record.Guesses.Count - 1] != record.Solution) Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"{record.PlayerName}: {record.Guesses.Count} tipp, megoldás: {record.Solution}");
                Console.BackgroundColor = ConsoleColor.DarkGray;
            }
        }
        Console.WriteLine("Nyomj egy gombot a visszatéréshez");
        Console.ReadKey();
    }
}