using System.Collections.Generic;
using UnityEngine;

public class StackPeekGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        int count = Random.Range(3, 5);
        List<int> stackValues = new List<int>();
        for (int i = 0; i < count; i++) stackValues.Add(Random.Range(10, 99));

        string initialStr = "[" + string.Join(", ", stackValues) + "]";

        // Jawaban Benar: Elemen paling kanan (TOP)
        int topValue = stackValues[stackValues.Count - 1];
        string correctAns = $"{topValue}";

        // Distractor: Ambil elemen bawah, atau elemen kedua dari atas
        List<string> ops = new List<string> {
            correctAns,
            stackValues[0].ToString(),
            (topValue + Random.Range(1, 5)).ToString(),
            "NULL"
        };

        var (shuffledOps, correctIdx) = ShuffleOptions(ops, correctAns);

        return new QuestionData
        {
            questionText = $"Diberikan Stack (Bottom to Top): {initialStr}.\nJika kita memanggil fungsi `Peek()`, nilai apakah yang akan dikembalikan (return) tanpa mengubah isi Stack?",
            options = shuffledOps,
            answerKey = correctIdx
        };
    }
}