using System.Collections.Generic;
using UnityEngine;

public class ArraySwapGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        int length = 4;
        int[] array = CreateRandomIntArray(length);

        int idxA = Random.Range(0, 2); // Indeks awal
        int idxB = Random.Range(2, 4); // Indeks akhir

        int[] swappedArray = (int[])array.Clone();
        (swappedArray[idxA], swappedArray[idxB]) = (swappedArray[idxB], swappedArray[idxA]);

        var (options, correctIndex) = GenerateOptionsForArrayResult(swappedArray);

        return new QuestionData
        {
            questionText = $"Diberikan array `arr` = {GetContains(array)}.\n" +
                           $"Jika dilakukan operasi:\n" +
                           $"int temp = arr[{idxA}];\n" +
                           $"arr[{idxA}] = arr[{idxB}];\n" +
                           $"arr[{idxB}] = temp;\n" +
                           $"Bagaimana isi array sekarang?",
            options = options,
            answerKey = correctIndex
        };
    }

    // Helper untuk generate pilihan jawaban berupa string array
    private (string options, int correctIndex) GenerateOptionsForArrayResult(int[] correctArr)
    {
        List<string> optionList = new List<string> { GetContains(correctArr) };

        while (optionList.Count < 4)
        {
            int[] fake = (int[])correctArr.Clone();
            fake[Random.Range(0, fake.Length)] = Random.Range(0, 10);
            string fakeStr = GetContains(fake);
            if (!optionList.Contains(fakeStr)) optionList.Add(fakeStr);
        }

        // Shuffle logic (bisa dipindah ke parent)
        for (int i = 0; i < optionList.Count; i++)
        {
            int r = Random.Range(i, optionList.Count);
            (optionList[i], optionList[r]) = (optionList[r], optionList[i]);
        }

        return (string.Join(";", optionList), optionList.IndexOf(GetContains(correctArr)));
    }
}