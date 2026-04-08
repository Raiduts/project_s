using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestionManager : MonoBehaviour
{
    // List untuk menampung semua generator yang ada di GameObject ini
    private List<QuestionGenerator> masterGenerators = new List<QuestionGenerator>();
    public GameObject questionScriptContainer;

    void Awake()
    {
        // Otomatis mengambil semua script yang inherit dari QuestionGenerator
        // Mau ada 10 atau 100 script generator baru, tidak perlu ubah code ini lagi.
        masterGenerators = questionScriptContainer.GetComponents<QuestionGenerator>().ToList();

        if (masterGenerators.Count == 0)
        {
            Debug.LogWarning("Waduh, belum ada script Generator yang ditempel di GameObject ini!");
        }
    }

    /// <summary>
    /// Ambil soal secara acak dari semua tipe yang tersedia
    /// </summary>
    public List<QuestionData> GenerateRandomQuestions(int amount)
    {
        List<QuestionData> questions = new List<QuestionData>();

        for (int i = 0; i < amount; i++)
        {
            // Pilih satu generator secara acak dari list master
            int randomIndex = Random.Range(0, masterGenerators.Count);
            questions.Add(masterGenerators[randomIndex].Generate());
        }

        return questions;
    }

    /// <summary>
    /// Ambil soal spesifik kategori tertentu (misal: cuma mau soal Stack)
    /// </summary>
    public List<QuestionData> GenerateQuestionsByCategory(QuestionCategory targetCat, int amount)
    {
        // Filter generator yang enum category-nya cocok
        var filteredGenerators = masterGenerators
            .Where(g => g.category == targetCat)
            .ToList();

        List<QuestionData> questions = new List<QuestionData>();

        if (filteredGenerators.Count == 0) return questions;

        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, filteredGenerators.Count);
            questions.Add(filteredGenerators[randomIndex].Generate());
        }

        return questions;
    }
}