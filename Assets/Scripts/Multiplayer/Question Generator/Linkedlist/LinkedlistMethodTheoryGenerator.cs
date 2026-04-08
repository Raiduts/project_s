using UnityEngine;
using System.Collections.Generic;

public class LinkedListMethodTheoryGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        // Tentukan tipe operasi secara random (0: AddFirst, 1: AddLast, 2: RemoveFirst, 3: RemoveLast)
        int operationType = Random.Range(0, 4);

        string before = "[10] -> [20] -> [30] -> NULL";
        string after = "";
        string correctAnswer = "";
        string questionAction = "";

        switch (operationType)
        {
            case 0:
                after = "[5] -> [10] -> [20] -> [30] -> NULL";
                correctAnswer = "AddFirst(5)";
                questionAction = "menambahkan elemen di posisi paling depan";
                break;
            case 1:
                after = "[10] -> [20] -> [30] -> [40] -> NULL";
                correctAnswer = "AddLast(40)";
                questionAction = "menambahkan elemen di posisi paling belakang";
                break;
            case 2:
                after = "[20] -> [30] -> NULL";
                correctAnswer = "RemoveFirst()";
                questionAction = "menghapus elemen pertama";
                break;
            case 3:
                after = "[10] -> [20] -> NULL";
                correctAnswer = "RemoveLast()";
                questionAction = "menghapus elemen terakhir";
                break;
        }

        List<string> ops = new List<string> { "AddFirst(x)", "AddLast(x)", "RemoveFirst()", "RemoveLast()" };
        // Kita sesuaikan sedikit list ops agar mengandung jawaban yang tepat (terutama untuk Add yang ada nilainya)
        for (int i = 0; i < ops.Count; i++)
        {
            if (operationType <= 1 && ops[i].Contains("Add"))
                ops[i] = ops[i].Replace("x", (operationType == 0 ? "5" : "40"));
        }

        return new QuestionData
        {
            questionText = $"Diberikan perubahan Linked List berikut:\n" +
                           $"Before: {before}\n" +
                           $"After : {after}\n\n" +
                           $"Operasi manakah yang digunakan untuk {questionAction} tersebut?",
            options = string.Join(";", ops),
            answerKey = ops.IndexOf(correctAnswer)
        };
    }
}