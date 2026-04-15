using UnityEngine;
using TMPro;

public class PlayerItem : MonoBehaviour
{
    public TMP_Text nameText;

    public void SetData(string playerName)
    {
        nameText.text = playerName;
    }
}