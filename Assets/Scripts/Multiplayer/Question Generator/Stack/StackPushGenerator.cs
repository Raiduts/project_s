using System.Collections.Generic;
using UnityEngine;

public class StackPushGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        // Randomize isi awal (2-3 elemen)
        int initialCount = Random.Range(2, 4);
        List<int> stackValues = new List<int>();
        for (int i = 0; i < initialCount; i++) stackValues.Add(Random.Range(10, 50));

        // Nilai baru yang di-push
        int newValue = Random.Range(51, 99);

        string initialStr = "[" + string.Join(", ", stackValues) + "]";

        // Jawaban Benar: Elemen baru di paling kanan (TOP)
        List<int> correctStack = new List<int>(stackValues);
        correctStack.Add(newValue);
        string correctStr = "[" + string.Join(", ", correctStack) + "]";

        // Distractor 1: Malah masuk ke Bottom (indeks 0)
        List<int> fake1 = new List<int>(stackValues);
        fake1.Insert(0, newValue);

        // Distractor 2: Menimpa nilai terakhir
        List<int> fake2 = new List<int>(stackValues);
        fake2[fake2.Count - 1] = newValue;

        List<string> ops = new List<string> {
            correctStr,
            "[" + string.Join(", ", fake1) + "]",
            "[" + string.Join(", ", fake2) + "]",
            "[" + newValue + "]"
        };

        // Shuffle ops di sini...
        var (shuffledOps, correctIdx) = ShuffleOptions(ops, correctStr);

        return new QuestionData
        {
            questionText = $"Sebuah Stack memiliki isi (Bawah to Atas): \n{initialStr}.\nJika dilakukan `Push({newValue})`, bagaimana kondisi Stack sekarang?",
            options = shuffledOps,
            answerKey = correctIdx
        };
    }
}