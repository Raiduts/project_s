using System.Collections.Generic;
using UnityEngine;

public class LinkedListAddFirstGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        int[] initialValues = { 10, 20, 30 };
        int newValue = Random.Range(40, 60);

        string initialList = "[10] -> [20] -> [30] -> NULL";
        string correctResult = $"[{newValue}] -> [10] -> [20] -> [30] -> NULL";
        string fake1 = $"[10] -> [20] -> [30] -> [{newValue}] -> NULL";
        string fake2 = $"[10] -> [{newValue}] -> [20] -> [30] -> NULL";
        string fake3 = $"[{newValue}] -> NULL";

        List<string> ops = new List<string> { correctResult, fake1, fake2, fake3 };
        // Shuffle ops...

        return new QuestionData
        {
            questionText = $"Diberikan Linked List: \n{initialList}.\n" +
                           $"Jika dilakukan operasi `InsertFront({newValue})`, bagaimana urutan list yang baru?",
            options = string.Join(";", ops),
            answerKey = 0 // Cari index setelah shuffle
        };
    }
}