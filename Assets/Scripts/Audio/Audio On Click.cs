using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    private Button btn;
    [SerializeField] private SFXType soundType;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        AudioManager.Instance.PlaySFX(soundType);
    }
}