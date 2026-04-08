using System.Collections.Generic;
using UnityEngine;

public class StackSequenceGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        List<int> stack = new List<int>();
        string log = "";

        // Generate 4 langkah acak
        for (int i = 0; i < 4; i++)
        {
            if (stack.Count == 0 || Random.value > 0.4f)
            {
                int val = Random.Range(10, 99);
                stack.Add(val);
                log += $"{i + 1}. Push({val})\n";
            }
            else
            {
                log += $"{i + 1}. Pop()\n";
                stack.RemoveAt(stack.Count - 1);
            }
        }

        int finalTop = stack.Count > 0 ? stack[stack.Count - 1] : -1;
        string correct = finalTop == -1 ? "Kosong" : finalTop.ToString();

        // Buat pilihan jawaban
        List<string> ops = new List<string> { correct };
        while (ops.Count < 4)
        {
            string fake = Random.Range(10, 99).ToString();
            if (!ops.Contains(fake)) ops.Add(fake);
        }

        var (shuffledOps, correctIdx) = ShuffleOptions(ops, correct);

        return new QuestionData
        {
            questionText = $"Perhatikan urutan operasi Stack berikut:\n{log}\nBerapakah nilai TOP sekarang?",
            options = shuffledOps,
            answerKey = correctIdx
        };
    }
}