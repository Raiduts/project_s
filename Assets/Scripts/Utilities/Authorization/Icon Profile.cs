using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconProfile : MonoBehaviour
{
    //private FirebaseFirestore db;
    private FirebaseAuth auth;

    [Header("Icons")]
    [SerializeField] private TextMeshProUGUI nameText, levelText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Sprite[] iconSprites;

    // Start is called before the first frame update
    void Start()
    {
        //db = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;

        //auth.StateChanged += OnStateChanged;

        //UserData.Instance.DataSaved += LoadProfile;

        Profile.Instance.ChangeProfileIcon += LoadIcon;

        UserData.Instance.FinishLoading += LoadProfile;

        //LoadIcon((iconIndex) => {
        //    iconImage.sprite = iconSprites[iconIndex];
        //    PlayerPrefs.SetInt("IconIndex", iconIndex);
        //});

        try { 
            LoadProfile();
        }
        catch
        {

        }
    }

    private void OnStateChanged(object sender, EventArgs e)
    {
        //print($"Try Loading User Profile {auth.CurrentUser.DisplayName}");

        if (auth.CurrentUser == null)
        {
            nameText.text = "Memuat...";
            return;
        }

        print("Loading User Profile");

        //LoadProfile();
    }

    private void LoadProfile()
    {
        //if (auth.CurrentUser == null) 
        //    return;
        //if (auth.CurrentUser == null)
        //{
        //    nameText.text = "Memuat...";
        //    return;
        //}

        LoadName();
        LoadLevel();
        LoadIcon();

        //UserData.Instance.SaveProgress();
    }

    public void LoadName()
    {
        nameText.text = auth.CurrentUser.DisplayName;
    }

    public void LoadLevel()
    {
        if (levelText)
        {
            levelText.text = $"Level {UserData.Instance.level}";        
        }
    }

    //private void OnChangeProfileIcon()
    //{
    //    int iconIndex = PlayerPrefs.GetInt("IconIndex");
    //    iconImage.sprite = iconSprites[iconIndex];
    //    SaveIcon(iconIndex);
    //}

    public void SaveIcon()
    {
        //UserData.Instance.iconIndex = iconIndex;

        UserData.Instance.SaveProgress();

        //string userId = auth.CurrentUser.Email;

        //DocumentReference userRef = db.Collection("Users").Document(userId);

        //Dictionary<string, object> data = new Dictionary<string, object>()
        //{
        //    { "iconIndex", iconIndex }
        //};

        //userRef.SetAsync(data, SetOptions.MergeAll);
    }

    public void LoadIcon(int index = -1)
    {
        if (index == -1)
        {
            index = UserData.Instance.iconIndex;
        }
        else
        {
            UserData.Instance.iconIndex = index;        
            UserData.Instance.SaveProgress();
        }

        iconImage.sprite = iconSprites[index];
    }

    private void OnDestroy()
    {
        Profile.Instance.ChangeProfileIcon -= LoadIcon;
        UserData.Instance.FinishLoading -= LoadProfile;
    }
}
