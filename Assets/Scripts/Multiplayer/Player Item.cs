using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    public TMP_Text nameText;
    public Image iconImage;
    public Sprite[] iconSprites;

    public void SetData(string playerName, int iconIndex)
    {
        nameText.text = playerName;
        iconImage.sprite = iconSprites[iconIndex];
    }
}