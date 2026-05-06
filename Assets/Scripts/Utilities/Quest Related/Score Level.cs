using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private TextMeshProUGUI timeSpendText;
    private float timeSpend;
    private bool isTicking = true;

    // Start is called before the first frame update
    void Start()
    {
        QuestEvent.CompletedLevel += ShowScore;
    }

    private void Update()
    {
        if (isTicking)
        {
            timeSpend += Time.deltaTime;
        }
    }

    public void ShowScore()
    {
        isTicking = false;

        panel.SetActive(true);  
        
        SetTimeSpendText();
    }

    private void SetTimeSpendText()
    {
        int m = (int)timeSpend / 60;
        int s = (int)timeSpend % 60;

        string mS = m > 9? $"{m}" : $"0{m}";
        string sS = s > 9? $"{s}" : $"0{m}";

        timeSpendText.text = $"{mS}:{sS}";
    }

    public void BackToCampaign()
    {
        MySceneManager.instance.ChangeScene("Campaign");
        PlayerPrefs.DeleteKey("Prev Scene");
    }

    private void OnDestroy()
    {
        QuestEvent.CompletedLevel -= ShowScore;
    }
}
