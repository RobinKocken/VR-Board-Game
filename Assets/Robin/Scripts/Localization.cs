using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Localization : MonoBehaviour
{
    public Language[] language;

    public void Dutch()
    {
        for(int i = 0; i < language.Length; i++)
        {
            language[i].uiText.text = language[i].dutch;
            language[i].uiText.fontSize = language[i].fontSizeD;
        }
    }

    public void English()
    {
        for(int i = 0; i < language.Length; i++)
        {
            language[i].uiText.text = language[i].english;
            language[i].uiText.fontSize = language[i].fontSizeE;
        }
    }
}

[System.Serializable]
public class Language
{
    public string description;
    public string english;
    public float fontSizeE;
    public string dutch;
    public float fontSizeD;
    public TMP_Text uiText;
}
