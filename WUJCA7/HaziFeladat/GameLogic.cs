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
        Console.WriteLine("nyomj (ESC) gombot a kilépéshez");
        while (true)
        {
            ConsoleKey input = Console.ReadKey(true).Key;
            if(input == ConsoleKey.Escape)
            {
                return;
            }
            if (input == ConsoleKey.K ||
               input == ConsoleKey.Z ||
               input == ConsoleKey.P ||
               input == ConsoleKey.S ||
               input == ConsoleKey.L ||
               input == ConsoleKey.N)
            {
                if (guess.Count < 4)
                {
                    guess.Add((char)input);
                    drawSquare(consoleKeyToColor(input), guess.Count, guesses.Count);
                }
            }
            else if (input == ConsoleKey.Backspace && guess.Count != 0)
            {
                drawSquare(ConsoleColor.DarkGray, guess.Count, guesses.Count);
                guess.RemoveAt(guess.Count - 1);
            }
            else if (input == ConsoleKey.Enter && guess.Count == 4)
            {
                guesses.Add(new string(guess.ToArray()));
                if (new string(guess.ToArray()) == solution)
                {
                    Console.SetCursorPosition(2, 3 + guesses.Count * 3);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Gratulálok! Kitaláltad a megoldást {guesses.Count} tippből!");
                    await FileReader.AddRecord(GameConfig.LeaderboardFilePath, new GameRecord(playerName, solution, guesses));
                    Console.SetCursorPosition(2, 4 + guesses.Count * 3);
                    Console.WriteLine($"Nyomj egy gombot a visszatéréshez");
                    Console.ReadKey(true);
                    return;
                }
                else
                {
                    List<char> solution_colors = solution.ToCharArray().ToList();
                    List<char> guess_colors = guess.ToList();
                    short correct = 0;
                    short correct_color = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (guess[i] == solution[i])
                        {
                            correct++;
                            solution_colors.Remove(guess[i]);
                            guess_colors.Remove(guess[i]);
                        }
                    }
                    for (int i = 0; i < guess_colors.Count; i++)
                    {
                        if (solution_colors.Contains(guess_colors[i]))
                        {
                            correct_color++;
                            solution_colors.Remove(guess_colors[i]);
                        }
                    }

                    //visszajelzés kiírása
                    drawpegs(guesses.Count - 1, correct, correct_color);

                    if (guesses.Count == 10)
                    {
                        Console.WriteLine($"Sajnos nem sikerült kitalálni a megoldást: {solution}");

                        await FileReader.AddRecord(GameConfig.LeaderboardFilePath, new GameRecord(playerName, solution, guesses));
                        Console.ReadKey(true);
                        return;
                    }
                }
                guess.Clear();
            }
        }
    }

    public static ConsoleColor consoleKeyToColor(ConsoleKey ck)
    {
        switch (ck)
        {
            case ConsoleKey.K:
                return ConsoleColor.Blue;
            case ConsoleKey.Z:
                return ConsoleColor.Green;
            case ConsoleKey.P:
                return ConsoleColor.Red;
            case ConsoleKey.S:
                return ConsoleColor.Yellow;
            case ConsoleKey.L:
                return ConsoleColor.Magenta;
            case ConsoleKey.N:
                return ConsoleColor.DarkYellow;
            default:
                return ConsoleColor.DarkGray;
        }
    }

    public static void drawpegs(int y, int correct, int correct_color)
    {
        if (correct == 4)
        {
            drawSquare(ConsoleColor.Black, 5, y);
            return;
        }

        if (correct_color == 4)
        {
            drawSquare(ConsoleColor.White, 5, y);
            return;
        }
        Console.BackgroundColor = ConsoleColor.Black;
        int drawn = 0;
        while (correct > 0)
        {
            pegpos(y, drawn);
            Console.Write("  ");
            correct--;
            drawn++;
        }
        Console.BackgroundColor = ConsoleColor.White;
        while (correct_color > 0)
        {
            pegpos(y, drawn);
            Console.Write("  ");
            correct_color--;
            drawn++;
        }
    }

    public static void pegpos(int y, int drawn)
    {
        switch (drawn)
        {
            case 0:
                Console.SetCursorPosition(37, 3 + y * 3);
                break;
            case 1:
                Console.SetCursorPosition(39, 3 + y * 3);
                break;
            case 2:
                Console.SetCursorPosition(37, 4 + y * 3);
                break;
            case 3:
                Console.SetCursorPosition(39, 4 + y * 3);
                break;
        }
    }

    public static void drawSquare(ConsoleColor color, int x, int y)
    {
        Console.BackgroundColor = color;
        Console.SetCursorPosition(2 + x * 7, 3 + y * 3);
        Console.WriteLine("     ");
        Console.SetCursorPosition(2 + x * 7, 4 + y * 3);
        Console.WriteLine("     ");
    }

    public static string GenerateSolution()
    {
        var colors = new[] { 'K', 'Z', 'P', 'S', 'L', 'N' };
        var rand = new Random();
        var chars = new char[4];
        for (int i = 0; i < 4; i++)
        {
            chars[i] = colors[rand.Next(colors.Length)];
        }
        return new string(chars);
    }
}
