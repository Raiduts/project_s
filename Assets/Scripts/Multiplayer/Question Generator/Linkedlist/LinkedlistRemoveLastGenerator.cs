using System.Collections.Generic;

public class LinkedListRemoveLastGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        string initialList = "[8] -> [16] -> [24] -> NULL";
        string correct = "[8] -> [16] -> NULL";
        string fake1 = "[16] -> [24] -> NULL"; // Ini RemoveFirst
        string fake2 = "[8] -> [24] -> NULL";
        string fake3 = "[8] -> [16] -> [24] -> [NULL]"; // Tidak berubah

        List<string> ops = new List<string> { correct, fake1, fake2, fake3 };
        // Shuffle ops...

        return new QuestionData
        {
            questionText = $"Diberikan Linked List: {initialList}.\n" +
                           $"Jika dijalankan fungsi `RemoveLast()`, manakah representasi list yang benar?",
            options = string.Join(";", ops),
            answerKey = 0
        };
    }
}