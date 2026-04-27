using System.Collections.Generic;
using UnityEngine;

public class ArrayUpdateGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        int length = Random.Range(3, 6);
        int[] array = CreateRandomIntArray(length);

        int index = Random.Range(0, length);
        int newValue = Random.Range(0, 20);

        int[] updatedArray = (int[])array.Clone();
        updatedArray[index] = newValue;

        var (options, correctIndex) = GenerateOptions(updatedArray);

        return new QuestionData
        {
            questionText = $"Diberikan array \n{GetContains(array)}\nJika arr[{index}] = {newValue}, bagaimana hasil akhirnya?",
            options = options,
            answerKey = correctIndex
        };
    }

    // generate options khusus array
    (string options, int correctIndex) GenerateOptions(int[] correctArray)
    {
        List<string> optionList = new List<string>();

        string correct = GetContains(correctArray);
        optionList.Add(correct);

        // bikin distractor
        while (optionList.Count < 4)
        {
            int[] fake = (int[])correctArray.Clone();

            int randIndex = Random.Range(0, fake.Length);
            fake[randIndex] += Random.Range(1, 5); // ubah dikit

            string fakeStr = GetContains(fake);

            if (!optionList.Contains(fakeStr))
                optionList.Add(fakeStr);
        }

        // shuffle
        for (int i = 0; i < optionList.Count; i++)
        {
            int rand = Random.Range(i, optionList.Count);
            (optionList[i], optionList[rand]) = (optionList[rand], optionList[i]);
        }

        int correctIndex = optionList.IndexOf(correct);

        return (string.Join(";", optionList), correctIndex);
    }
}