using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizeInitialize : MonoBehaviour
{
    public UIManager uiManager;

    public TMP_Text text;

    public LocalizeFile localization;

    private void Start()
    {
        text = GetComponent<TMP_Text>();

        SetLanguage();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            uiManager.language = UIManager.UILanguage.English;


        }else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            uiManager.language = UIManager.UILanguage.Dutch;
        }

        if(uiManager.previousUILanguage != uiManager.language)
        {
            text.text = "";
            text.text = localization.markupToReplace;

            uiManager.previousUILanguage = uiManager.language;

            SetLanguage();
        }
    }

    public void SetLanguage()
    {
        switch (uiManager.language)
        {
            case UIManager.UILanguage.English:
                if (text.text != localization.multiLanguageText.englishTxt)
                {
                    text.text = GetName(text, localization.multiLanguageText.englishTxt, localization.markupToReplace);
                }
                break;

            case UIManager.UILanguage.Dutch:
                if (text.text != localization.multiLanguageText.dutchTxt)
                {
                    text.text = text.text.Replace(localization.markupToReplace, localization.multiLanguageText.dutchTxt);
                }
                break;
        }
    }

    public string GetName(TMP_Text inputText, string title, string markupType)
    {
        text.text = localization.markupToReplace;

        return inputText.text.Replace(markupType, title);
    }
}
