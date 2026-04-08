using System.Collections.Generic;
using UnityEngine;

public class ArrayIndexLookupGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        int length = 5;
        int[] array = { 10, 20, 30, 40, 50 }; // Contoh statis yang diacak posisinya
        // Shuffle array dulu agar letak angka berubah-ubah
        for (int i = 0; i < array.Length; i++)
        {
            int r = Random.Range(i, array.Length);
            (array[i], array[r]) = (array[r], array[i]);
        }

        int targetIndex = Random.Range(0, length);
        int targetValue = array[targetIndex];

        // Opsi jawaban adalah indeksnya (0, 1, 2, 3...)
        List<string> ops = new List<string> { "0", "1", "2", "3", "4" };
        // Ambil 4 saja untuk pilihan ganda
        while (ops.Count > 4) ops.RemoveAt(Random.Range(0, ops.Count));
        if (!ops.Contains(targetIndex.ToString())) ops[0] = targetIndex.ToString();

        return new QuestionData
        {
            questionText = $"Pada array {GetContains(array)}, di indeks berapakah nilai {targetValue} berada?",
            options = string.Join(";", ops),
            answerKey = ops.IndexOf(targetIndex.ToString())
        };
    }
}