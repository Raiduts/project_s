using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayAccessGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        int length = Random.Range(3, 6);
        int index = Random.Range(0, length);

        int[] array = CreateRandomIntArray(length);

        int correctAnswer = array[index];

        var (options, correctIndex) = GenerateOptionsFromArray(array, correctAnswer);

        return new QuestionData
        {
            questionText = $"Berapa nilai index {index} dari {GetContains(array)}?",
            options = options,
            answerKey = correctIndex
        };
    }
}
