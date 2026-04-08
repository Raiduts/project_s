using System.Collections.Generic;
using UnityEngine;

public class QueueDequeueGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        int count = Random.Range(3, 5);
        List<int> queueValues = new List<int>();
        for (int i = 0; i < count; i++) queueValues.Add(Random.Range(10, 99));

        string initialStr = "[" + string.Join(", ", queueValues) + "]";
        int removedValue = queueValues[0]; // Front

        // Sisa antrean setelah Dequeue
        List<int> remaining = new List<int>(queueValues);
        remaining.RemoveAt(0);
        string remainingStr = "[" + string.Join(", ", remaining) + "]";

        string correctAns = $"Keluar: {removedValue}, Sisa: {remainingStr}";

        // Distractor: Salah ambil (ambil dari belakang/Rear)
        int wrongRemoved = queueValues[queueValues.Count - 1];
        List<int> wrongRemaining = new List<int>(queueValues);
        wrongRemaining.RemoveAt(queueValues.Count - 1);
        string fake1 = $"Keluar: {wrongRemoved}, Sisa: [" + string.Join(", ", wrongRemaining) + "]";

        List<string> ops = new List<string> {
            correctAns,
            fake1,
            $"Keluar: {removedValue}, Sisa: {initialStr}",
            $"Keluar: {queueValues[1]}, Sisa: {remainingStr}"
        };

        var (shuffledOps, correctIdx) = ShuffleOptions(ops, correctAns);

        return new QuestionData
        {
            questionText = $"Diberikan Queue (Front to Rear): {initialStr}.\nBerapakah nilai yang keluar dan bagaimana sisa Queue jika dilakukan satu kali `Dequeue()`?",
            options = shuffledOps,
            answerKey = correctIdx
        };
    }
}