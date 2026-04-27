using System.Collections.Generic;

public class QueueEnqueueGenerator : QuestionGenerator
{
    public override QuestionData Generate()
    {   
        string initial = "[10, 20]"; // Front to Rear
        int newValue = 30;

        // Visualisasi: Front di kiri, Rear di kanan
        string correct = "[10, 20, 30]";
        string fake1 = "[30, 10, 20]"; // Salah: masuk ke depan (seperti Stack/AddFirst)
        string fake2 = "[10, 30]";
        string fake3 = "[30]";

        List<string> ops = new List<string> { correct, fake1, fake2, fake3 };
        // Shuffle ops...

        return new QuestionData
        {
            questionText = $"Diberikan Queue (Depan ke Belakang): \n{initial}.\n" +
                           $"Jika dilakukan operasi `Enqueue({newValue})`, bagaimana isi Queue sekarang?",
            options = string.Join(";", ops),
            answerKey = 0
        };
    }
}