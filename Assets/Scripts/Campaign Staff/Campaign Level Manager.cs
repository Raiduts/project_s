using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CampaignLevelManager : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private TextMeshProUGUI chamberNameText;
    [SerializeField]
    private LevelButton levelButtonPref;
    private List<LevelButton> levelButtons;
    [SerializeField]
    private SelectLevelButton selectLevelButton;
    [SerializeField]
    private Transform contentView;
    private int selectedLevelInt;
    private int currentUnlockedLevel;

    [Header("Level")]
    [SerializeField]
    private LevelData[] ArrayQuest;
    [SerializeField]
    private LevelData[] LinkedlistQuest;
    [SerializeField]
    private LevelData[] StackQuest;
    [SerializeField]
    private LevelData[] QueueQuest;
    
    private LevelData[] currentLevelData;

    private DSType selectedCampaign;
    private string[] sceneNames = { "Array","Linkedlist","Stack","Queue"};

    private void Start()
    {
        if (UserData.Instance)
        {
            selectedCampaign = UserData.Instance.campaignDSType;        
        }
        else
        {
            return;
        }

        levelButtons = new List<LevelButton>();

        selectLevelButton.OnStartLevel += StartLevel;

        switch(selectedCampaign)
        {
            case DSType.Array:
                currentLevelData = ArrayQuest;
                currentUnlockedLevel = UserData.Instance.arrayLevel;
                break;
            case DSType.Linkedlist:
                currentLevelData = LinkedlistQuest;
                currentUnlockedLevel = UserData.Instance.linkedlistLevel;
                break;
            case DSType.Stack:
                currentLevelData = StackQuest;
                currentUnlockedLevel = UserData.Instance.stackLevel;  
                break;
            case DSType.Queue:
                currentLevelData = QueueQuest;
                currentUnlockedLevel = UserData.Instance.queueLevel;
                break;
        }

        chamberNameText.text = $"School of {sceneNames[(int) selectedCampaign]}";

        int pos = 200;
        int direction = 1;
        
        for (int i = 0; i < currentLevelData.Length; i++)
        {
            if (pos == 200)
            {
                direction = -1;
            } else if (pos == -200)
            {
                direction = 1;
            }

            pos += direction * UnityEngine.Random.Range(1, 5) * 50;

            pos = math.clamp(pos, -200, 200);

            LevelButton tempButton = Instantiate(levelButtonPref, contentView);
            levelButtons.Add(tempButton);
            tempButton.SetLevel(i + 1);
            tempButton.OnSelectLevel += SelectLevel;
            tempButton.Reposition(pos);

            bool isUnlocked = i <= currentUnlockedLevel;

            // Set Interactable
            tempButton.SetInteractable(isUnlocked);

            // Set Level Data
            currentLevelData[i].level = i;
        }
    }

    public void SelectLevel(int x) 
    { 
        selectLevelButton.button.interactable = true;

        selectedLevelInt = x;
        DeselectPrev(selectedLevelInt);
    }

    private void DeselectPrev(int x)
    {
        foreach (LevelButton item in levelButtons)
        {
            if (item.GetLevel() != selectedLevelInt)
            {
                item.Deselect();
            }
        }
    }

    public void StartLevel()
    {
        if (selectedLevelInt == 0)
        {
            return;
        }

        print($"Starting Level {selectedLevelInt}");

        MySceneManager.instance.ChangeScene(sceneNames[(int) selectedCampaign]);

        LevelData selectedLevel = Instantiate(currentLevelData[selectedLevelInt - 1]);
        DontDestroyOnLoad(selectedLevel);
    }
}
