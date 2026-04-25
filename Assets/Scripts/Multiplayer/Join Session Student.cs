using UnityEngine;
using TMPro; // Untuk InputField
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Firestore;

public class JoinSessionStudent : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField]
    private TMP_InputField codeInputField; // Tempat siswa ngetik kode
    public Button joinRoomButton;
    public TextMeshProUGUI errorText; // Teks buat munculin "Kode Salah"
    public string quizSceneName; // Ganti ke nama scene kuis-mu

    private FirebaseFirestore db;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;

        // Setup awal
        joinRoomButton.onClick.AddListener(OnJoinClicked);
        if (errorText != null) errorText.gameObject.SetActive(false);
    }

    public async void OnJoinClicked()
    {
        string inputCode = codeInputField.text.Trim();

        // Validasi input kosong
        if (string.IsNullOrEmpty(inputCode))
        {
            ShowError("Masukkan kode dulu!");
            return;
        }

        joinRoomButton.interactable = false;
        if (errorText != null) errorText.gameObject.SetActive(false);

        try
        {
            // 1. Cek ke Firebase apakah dokumen kode ini ada
            DocumentReference quizRef = db.Collection("Quizzes").Document(inputCode);
            DocumentSnapshot snapshot = await quizRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                // KODE DITEMUKAN!
                Debug.Log($"Berhasil join ke room: {inputCode}");

                // 2. Simpan kode ke PlayerPrefs supaya scene Gameplay tahu harus ambil soal mana
                //PlayerPrefs.SetInt("IsTeacher", 0);
                PlayerPrefs.SetString("Code", inputCode);
                PlayerPrefs.Save();

                // 3. Pindah ke Scene Gameplay
                MySceneManager.instance.ChangeScene(quizSceneName);
            }
            else
            {
                // KODE TIDAK DITEMUKAN
                ShowError("Kode Room tidak ditemukan!");
                joinRoomButton.interactable = true;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error pas join: {e.Message}");
            ShowError("Gagal menyambung ke server");
            joinRoomButton.interactable = true;
        }
    }

    private void ShowError(string message)
    {
        if (errorText != null)
        {
            errorText.text = message;
            errorText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning(message);
        }
    }

    public void CloseTab()
    {
        Destroy(gameObject);
    }
}