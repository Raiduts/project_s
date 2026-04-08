using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestionGenerator : MonoBehaviour
{
    public QuestionCategory category;
    public int difficulty;
    public float weight;

    public abstract QuestionData Generate();

    protected int[] CreateRandomIntArray(int length)
    {
        int[] array = new int[length];

        for (int i = 0; i < length; i++)
        {
            array[i] = Random.Range(0, 10);
        }

        return array;
    }

    protected string GetContains(int[] array)
    {
        return "[" + string.Join(",", array) + "]";
    }

    protected (string options, int correctIndex) GenerateOptionsFromArray(int[] array, int correctAnswer)
    {
        List<string> optionList = new List<string>();

        // ambil semua nilai unik dari array
        HashSet<int> uniqueValues = new HashSet<int>(array);

        // pastikan jawaban benar masuk
        optionList.Add(correctAnswer.ToString());
        uniqueValues.Remove(correctAnswer);

        // ambil dari array dulu (lebih natural)
        foreach (var val in uniqueValues)
        {
            if (optionList.Count >= 4) break;
            optionList.Add(val.ToString());
        }

        // kalau masih kurang → baru generate fake
        while (optionList.Count < 4)
        {
            int fake = correctAnswer + Random.Range(-3, 4);
            if (!optionList.Contains(fake.ToString()))
            {
                optionList.Add(fake.ToString());
            }
        }

        // shuffle
        for (int i = 0; i < optionList.Count; i++)
        {
            int rand = Random.Range(i, optionList.Count);
            (optionList[i], optionList[rand]) = (optionList[rand], optionList[i]);
        }

        int correctIndex = optionList.IndexOf(correctAnswer.ToString());

        return (string.Join(";", optionList), correctIndex);
    }

    protected (string shuffled, int index) ShuffleOptions(List<string> options, string correct)
    {
        for (int i = 0; i < options.Count; i++)
        {
            int r = Random.Range(i, options.Count);
            (options[i], options[r]) = (options[r], options[i]);
        }
        return (string.Join(";", options), options.IndexOf(correct));
    }
}

public enum QuestionCategory
{
    Array,
    Stack,
    Queue,
    Syntax
}
