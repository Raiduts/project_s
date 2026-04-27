using System.Collections.Generic;
using UnityEngine;

public class QueueSequenceGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        List<int> queue = new List<int>();
        string log = "";

        // Simulasi 4 langkah acak
        for (int i = 0; i < 4; i++)
        {
            // Jika kosong, wajib Enqueue. Jika ada isinya, 70% chance Enqueue.
            if (queue.Count == 0 || Random.value > 0.3f)
            {
                int val = Random.Range(10, 99);
                queue.Add(val);
                log += $"{i + 1}. Enqueue({val})\n";
            }
            else
            {
                log += $"{i + 1}. Dequeue()\n";
                queue.RemoveAt(0);
            }
        }

        string resultStr = queue.Count == 0 ? "Kosong" : "[" + string.Join(", ", queue) + "]";

        // Buat pilihan jawaban
        List<string> ops = new List<string> { resultStr };
        while (ops.Count < 4)
        {
            // Generate fake result dengan merandom ulang isi queue sedikit
            List<int> fakeQueue = new List<int>(queue);
            if (fakeQueue.Count > 0) fakeQueue[Random.Range(0, fakeQueue.Count)] += Random.Range(-5, 5);
            else fakeQueue.Add(Random.Range(10, 99));

            string fakeStr = "[" + string.Join(", ", fakeQueue) + "]";
            if (!ops.Contains(fakeStr)) ops.Add(fakeStr);
        }

        var (shuffledOps, correctIdx) = ShuffleOptions(ops, resultStr);

        return new QuestionData
        {
            questionText = $"Perhatikan urutan operasi Queue berikut (Depan ke Belakang):\n{log}\nBagaimanakah isi Queue terakhir?",
            options = shuffledOps,
            answerKey = correctIdx
        };
    }
}