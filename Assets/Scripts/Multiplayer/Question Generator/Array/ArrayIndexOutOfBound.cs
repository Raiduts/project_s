using UnityEngine;

public class ArrayOutOfBoundsGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        int length = Random.Range(3, 5);
        int[] array = CreateRandomIntArray(length);

        // Sengaja akses index = length (yang mana pasti error)
        return new QuestionData
        {
            questionText = $"Apa hasil dari kode berikut?\n" +
                           $"int[] arr = {GetContains(array)};\n" +
                           $"print(arr[{length}]);",
            options = $"0;NULL;IndexOutOfRangeException;{array[length-1]}",
            answerKey = 2 // Index ke-2 adalah "IndexOutOfRangeException"
        };
    }
}