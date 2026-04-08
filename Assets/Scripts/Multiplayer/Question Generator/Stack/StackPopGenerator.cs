using System.Collections.Generic;
using UnityEngine;

public class StackPopGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        // Randomize isi awal (3-4 elemen)
        int count = Random.Range(3, 5);
        List<int> stackValues = new List<int>();
        for (int i = 0; i < count; i++) stackValues.Add(Random.Range(10, 99));

        string initialStr = "[" + string.Join(", ", stackValues) + "]";

        // Jawaban Benar: Ambil elemen terakhir (TOP)
        int poppedValue = stackValues[stackValues.Count - 1];
        List<int> remaining = new List<int>(stackValues);
        remaining.RemoveAt(remaining.Count - 1);
        string remainingStr = "[" + string.Join(", ", remaining) + "]";

        string correctAns = $"Keluar: {poppedValue}, Sisa: {remainingStr}";

        // Distractor 1: Salah ambil dari bawah (Bottom/indeks 0)
        int bottomVal = stackValues[0];
        List<int> fakeRemaining = new List<int>(stackValues);
        fakeRemaining.RemoveAt(0);
        string fake1 = $"Keluar: {bottomVal}, Sisa: [" + string.Join(", ", fakeRemaining) + "]";

        // Distractor 2: Nilai keluar benar, tapi sisa stack tidak berubah
        string fake2 = $"Keluar: {poppedValue}, Sisa: {initialStr}";

        List<string> ops = new List<string> {
            correctAns,
            fake1,
            fake2,
            $"Keluar: {stackValues[0]}, Sisa: {remainingStr}"
        };

        var (shuffledOps, correctIdx) = ShuffleOptions(ops, correctAns);

        return new QuestionData
        {
            questionText = $"Diberikan Stack (Bottom to Top): {initialStr}.\nBerapakah nilai yang keluar dan bagaimana sisa Stack jika dilakukan operasi `Pop()`?",
            options = shuffledOps,
            answerKey = correctIdx
        };
    }
}