using UnityEngine;
using System.Collections.Generic;

public class LinkedListTailGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        int length = Random.Range(3, 5);
        int[] values = CreateRandomIntArray(length);

        // Visualisasi: [10] -> [20] -> [30] -> NULL
        string linkedListVisual = "";
        for (int i = 0; i < values.Length; i++)
        {
            linkedListVisual += $"[{values[i]}] -> ";
        }
        linkedListVisual += "NULL";

        int correctAnswer = values[values.Length - 1];
        var (options, correctIndex) = GenerateOptionsForNumeric(values, correctAnswer);

        return new QuestionData
        {
            questionText = $"Diberikan sebuah Singly Linked List: \n{linkedListVisual}.\n" +
                           $"Jika kita melakukan traversal dari Head hingga `node.next == NULL`, nilai apakah yang tersimpan di node tersebut?",
            options = options,
            answerKey = correctIndex
        };
    }

    private (string options, int correctIndex) GenerateOptionsForNumeric(int[] values, int correct)
    {
        List<string> ops = new List<string> { correct.ToString() };
        foreach (int v in values)
        {
            if (v != correct && !ops.Contains(v.ToString())) ops.Add(v.ToString());
            if (ops.Count >= 4) break;
        }
        // Shuffle...
        return (string.Join(";", ops), 0); // Implementasi shuffle seperti sebelumnya
    }
}