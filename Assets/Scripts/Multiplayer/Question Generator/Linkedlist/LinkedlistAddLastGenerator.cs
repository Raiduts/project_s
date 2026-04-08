using System.Collections.Generic;

public class LinkedListAddLastGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {
        int[] initial = { 5, 10, 15 };
        int newValue = 20;

        string initialList = "[5] -> [10] -> [15] -> NULL";
        string correct = "[5] -> [10] -> [15] -> [20] -> NULL";
        string fake1 = "[20] -> [5] -> [10] -> [15] -> NULL"; // Ini AddFirst
        string fake2 = "[5] -> [20] -> [10] -> [15] -> NULL";
        string fake3 = "[5] -> [10] -> [20] -> NULL"; // Data hilang

        List<string> ops = new List<string> { correct, fake1, fake2, fake3 };
        // Shuffle ops...

        return new QuestionData
        {
            questionText = $"Diberikan Linked List: {initialList}.\n" +
                           $"Jika dilakukan operasi `AddLast({newValue})`, bagaimana urutan list yang baru?",
            options = string.Join(";", ops),
            answerKey = 0 // Cari index setelah shuffle
        };
    }
}