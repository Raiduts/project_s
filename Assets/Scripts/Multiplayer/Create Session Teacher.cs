using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Untuk teks kode
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Firestore;
using System.Threading.Tasks;
using Firebase.Auth;

public class CreateSessionTeacher : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI codeText;
    public Button buatRoomButton;
    public string nextSceneName; // Ganti dengan nama scene-mu

    private FirebaseFirestore db;
    private FirebaseAuth auth;
    private string generatedCode;

    private void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;    
    }
    void Start()
    {

        // Setup Button
        buatRoomButton.onClick.AddListener(OnBuatRoomClicked);

        // Generate kode awal saat buka popup
        GenerateUniqueCode();
    }

    async void GenerateUniqueCode()
    {
        buatRoomButton.interactable = false;
        codeText.text = "Checking...";

        bool isUnique = false;
        string newCode = "";

        // Loop sampai dapet kode yang bener-bener belum ada di database
        while (!isUnique)
        {
            newCode = Random.Range(100000, 999999).ToString(); // Generate 6 digit angka

            DocumentReference quizRef = db.Collection("Quizzes").Document(newCode);
            DocumentSnapshot snapshot = await quizRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                isUnique = true;
            }
        }

        generatedCode = newCode;
        codeText.text = $"Kode Ruang: {generatedCode}";
        buatRoomButton.interactable = true;
    }

    public async void OnBuatRoomClicked()
    {
        buatRoomButton.interactable = false;

        // 1. Simpan ke Database (Meta Data) supaya kode ini "Terpakai"
        // Cara "Biasa" yang tadi kita bahas
        DocumentReference quizRef = db.Collection("Quizzes").Document(generatedCode);

        Dictionary<string, object> metaData = new Dictionary<string, object>
        {
            { "createdAt", FieldValue.ServerTimestamp },
            { "status", "active" },
            { "createdBy", auth.CurrentUser.Email } // Opsional: catat siapa pembuatnya
        };

        try
        {
            // Set meta data dulu
            await quizRef.SetAsync(metaData);

            // 2. Generate soal (Opsional: kalau soal mau digenerate sekarang)
            await GenerateQuestionsBatch(quizRef);

            // 3. Simpan ke PlayerPrefs
            PlayerPrefs.SetInt("IsTeacher", 1);
            PlayerPrefs.SetString("Code", generatedCode);
            PlayerPrefs.Save();

            Debug.Log($"Room {generatedCode} berhasil dibuat!");

            // 4. Pindah Scene
            MySceneManager.instance.ChangeScene(nextSceneName);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Gagal buat room: {e.Message}");
            buatRoomButton.interactable = true;
        }
    }

    // Fungsi helper buat isi soal (Batch)
    private async Task GenerateQuestionsBatch(DocumentReference quizRef)
    {
        WriteBatch batch = db.StartBatch();

        for (int i = 1; i <= 20; i++)
        {
            DocumentReference qRef = quizRef.Collection("Questions").Document($"Q{i}");

            // Dummy soal, nanti kamu sesuaikan isinya
            Dictionary<string, object> question = new Dictionary<string, object>
            {
                { "questionText", $"Pertanyaan nomor {i}?" },
                { "options", "Opsi A;Opsi B;Opsi C;Opsi D" },
                { "answerKey", Random.Range(0, 4) }
            };

            batch.Set(qRef, question);
        }

        await batch.CommitAsync();
    }

    public void CloseTab()
    {
        Destroy(gameObject);
    }
}