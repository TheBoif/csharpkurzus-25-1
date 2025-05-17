using System.Collections.Generic;

//egy lejátszott játék adatait tárolja, a ranglistában ezek a rekordok jelennek meg
//a színek: kék (k), zöld(z), piros(p), sárga(s), lila(l), narancs(n)
//a megoldás és a tippek is 4 karakteres stringben vannak tárolva, pl.: "kzpsl"
public record GameRecord(
    string PlayerName,
    string Solution,
    List<string> Guesses
);
