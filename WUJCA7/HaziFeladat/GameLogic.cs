using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HaziFeladat;

public static class GameLogic
{
    public static async Task StartNewGame(string playerName)
    {
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Clear();
        string solution = GenerateSolution();
        List<char> guess = new List<char>();
        List<string> guesses = new List<string>();
        Console.WriteLine("Írj be egy színt: (K)kék, (Z)zöld, (P)piros, (S)sárga, (L)lila, (N)narancs");
        while (true)
        {
            ConsoleKey input = Console.ReadKey(true).Key;
            switch (input)
            {
                case ConsoleKey.K:
                    if (guess.Count == 4) break;
                    guess.Add('k');
                    drawSquare(ConsoleColor.Blue, guess.Count, guesses.Count);
                    break;
                case ConsoleKey.Z:
                    if (guess.Count == 4) break;
                    guess.Add('z');
                    drawSquare(ConsoleColor.Green, guess.Count, guesses.Count);
                    break;
                case ConsoleKey.P:
                    if (guess.Count == 4) break;
                    guess.Add('p');
                    drawSquare(ConsoleColor.Red, guess.Count, guesses.Count);
                    break;
                case ConsoleKey.S:
                    if (guess.Count == 4) break;
                    guess.Add('s');
                    drawSquare(ConsoleColor.Yellow, guess.Count, guesses.Count);
                    break;
                case ConsoleKey.L:
                    if (guess.Count == 4) break;
                    guess.Add('l');
                    drawSquare(ConsoleColor.Magenta, guess.Count, guesses.Count);
                    break;
                case ConsoleKey.N:
                    if (guess.Count == 4) break;
                    guess.Add('n');
                    drawSquare(ConsoleColor.DarkYellow, guess.Count, guesses.Count);
                    break;
                case ConsoleKey.Backspace:
                    if (guess.Count == 0) break;
                    drawSquare(ConsoleColor.DarkGray, guess.Count, guesses.Count);
                    guess.RemoveAt(guess.Count - 1);
                    break;
                case ConsoleKey.Enter:
                    if (guess.Count != 4) break;

                    guesses.Add(new string(guess.ToArray()));
                    if (new string(guess.ToArray()) == solution)
                    {
                        Console.WriteLine($"Gratulálok! Kitaláltad a megoldást: {solution}");
                        
                        await FileReader.AddRecord(GameConfig.LeaderboardFilePath, new GameRecord(playerName, solution, guesses));
                        return;
                    }
                    else
                    {
                        string solution_colors = solution;
                        short correct = 0;
                        short correct_color = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            if (guess[i] == solution[i])
                            {
                                correct++;
                                solution_colors = solution_colors.Remove(i, 1);
                            }
                            else if (solution_colors.Contains(guess[i]))
                            {
                                correct_color++;
                                solution_colors = solution_colors.Remove(solution_colors.IndexOf(guess[i]), 1);
                            }
                        }
                        if (correct_color == 4) drawSquare(ConsoleColor.Black, guess.Count + 1, guesses.Count);

                        if (guesses.Count == 10)
                        {
                            Console.WriteLine($"Sajnos nem sikerült kitalálni a megoldást: {solution}");

                            await FileReader.AddRecord(GameConfig.LeaderboardFilePath, new GameRecord(playerName, solution, guesses));
                            return;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public static void drawSquare(ConsoleColor color, int x, int y)
    {
        Console.BackgroundColor = color;
        Console.SetCursorPosition(2 + x * 7, 2 + y * 3);
        Console.WriteLine("     ");
        Console.SetCursorPosition(2 + x * 7, 3 + y * 3);
        Console.WriteLine("     ");
    }

    public static string GenerateSolution()
    {
        var colors = new[] { 'k', 'z', 'p', 's', 'l', 'n' };
        var rand = new Random();
        var chars = new char[4];
        for (int i = 0; i < 4; i++)
        {
            chars[i] = colors[rand.Next(colors.Length)];
        }
        return new string(chars);
    }
}
