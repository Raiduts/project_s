using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;

public class CampaignLevelManager : MonoBehaviour
{
    [Header("Object")]
    [SerializeField]
    private LevelButton levelButtonPref;
    private List<LevelButton> levelButtons;
    [SerializeField]
    private SelectLevelButton selectLevelButton;
    [SerializeField]
    private Transform contentView;
    private int selectedLevelInt;

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

    private int selectedCampaign;
    private string[] sceneNames = { "Array","Linkedlist","Stack","Queue"};

    private void Start()
    {
        selectedCampaign = PlayerPrefs.GetInt("campaignType");

        levelButtons = new List<LevelButton>();

        selectLevelButton.OnStartLevel += StartLevel;

        switch(selectedCampaign)
        {
            case 0:
                currentLevelData = ArrayQuest;
                break;
            case 1:
                currentLevelData = LinkedlistQuest;
                break;
            case 2:
                currentLevelData = StackQuest;
                break;
            case 3:
                currentLevelData = QueueQuest;
                break;
        }

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
        }
    }

    public void SelectLevel(int x) 
    { 
        DeselectPrev(selectedLevelInt);
        selectedLevelInt = x;
    }

    private void DeselectPrev(int x)
    {
        foreach (LevelButton item in levelButtons)
        {
            if (item.GetLevel() == x)
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

        MySceneManager.instance.ChangeScene(sceneNames[selectedCampaign]);

        LevelData selectedLevel = Instantiate(currentLevelData[selectedLevelInt - 1]);
        DontDestroyOnLoad(selectedLevel);
    }
}
