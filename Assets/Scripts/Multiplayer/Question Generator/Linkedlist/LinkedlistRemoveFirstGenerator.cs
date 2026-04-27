using System.Collections.Generic;

public class LinkedListRemoveFirstGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        string initialList = "[10] -> [20] -> [30] -> NULL";
        string correct = "[20] -> [30] -> NULL";
        string fake1 = "[10] -> [20] -> NULL"; // Ini RemoveLast
        string fake2 = "[10] -> [30] -> NULL"; // Ini Remove Middle
        string fake3 = "NULL";

        List<string> ops = new List<string> { correct, fake1, fake2, fake3 };
        // Shuffle ops...

        return new QuestionData
        {
            questionText = $"Diberikan Linked List: \n{initialList}.\n" +
                           $"Jika dijalankan fungsi `RemoveFirst()`, bagaimana hasil list akhirnya?",
            options = string.Join(";", ops),
            answerKey = 0
        };
    }
}