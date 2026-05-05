using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowHidePassword : MonoBehaviour
{
    public TMP_InputField passwordInputField;

    public Image eyeIcon;
    public Sprite hidePass, showPass;

    private void Start()
    {
        eyeIcon = GetComponent<Image>();
    }

    public void ChangeVisibility()
    {
        TMP_InputField.ContentType inputType = passwordInputField.contentType;

        UpdatePassVisibility(inputType);
    }

    private void UpdatePassVisibility(TMP_InputField.ContentType inputType)
    {
        if (inputType == TMP_InputField.ContentType.Password)
        {
            passwordInputField.contentType = TMP_InputField.ContentType.Standard;
            eyeIcon.sprite = showPass;
        } else
        {
            passwordInputField.contentType = TMP_InputField.ContentType.Password;
            eyeIcon.sprite = hidePass;
        }

        passwordInputField.ForceLabelUpdate();
    }
}
