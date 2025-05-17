using System;
using System.Collections.Generic;

public static class GameLogic
{
    public static void StartNewGame()
    {
        Console.Clear();
        Console.WriteLine("játék");
        Console.WriteLine("nyomj egy gombot a visszatéréshez");
        Console.ReadKey();
    }
}
