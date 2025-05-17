using System.Collections.Generic;

public record GameRecord(
    string PlayerName,
    string Solution,
    List<string> Guesses
);
