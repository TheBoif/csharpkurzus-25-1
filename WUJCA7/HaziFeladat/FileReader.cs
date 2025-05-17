using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public static class LeaderboardController
{
    public static async Task<List<GameRecord>> ReadLeaderboardAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<GameRecord>();
        }
        using FileStream stream = File.OpenRead(filePath);
        var records = await JsonSerializer.DeserializeAsync<List<GameRecord>>(stream) ?? new List<GameRecord>();
        return records;
    }

    public static async Task WriteLeaderboardAsync(string filePath, List<GameRecord> records)
    {
        using FileStream stream = File.Create(filePath);
        await JsonSerializer.SerializeAsync(stream, records, new JsonSerializerOptions { WriteIndented = true });
    }
}
