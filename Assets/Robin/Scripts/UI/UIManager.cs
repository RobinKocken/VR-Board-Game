using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("SettingsButton")]
    public Transform shutdownButton;
    public Transform languageButton;
    public Transform shutdownTargetPos, languageTargetPos;
    Vector3 shutdownStartPos, languageStartPos;
    public float settingsSpeed;
    bool settingsAnimationOut;
    bool settingsAnimationIn;
    bool settingsActive;

    [Header("Credits")]
    public Transform border;
    public Transform credits;
    public Transform borderTargetPos, creditsTargetPos;
    Vector3 borderStartPos, creditsStartPos;
    public float borderSpeed;
    public float creditsSpeed;
    bool creditsAnimationOut;
    bool creditsAnimationIn;
    bool creditsActive;

    [Header("Language")]
    public Transform englishButton;
    public Transform dutchButton;
    public Transform languageQuestion;
    public Transform englishTargetPos, dutchTargetPos, languageQuestionTargetPos;
    Vector3 englishStartPos, dutchStartPos, languageQustionStartPos;
    public float languageSpeed;
    public float languageQuestionSpeed;
    bool languageAnimationOut;
    bool languageAnimationIn;
    bool languageActive;

    [Header("Shutdown")]
    public Transform yesButton;
    public Transform noButton;
    public Transform shutdownQuestion;
    public Transform yesButtonTargetPos, noButtonTargetPos, shutdownQuestionTargetPos;
    Vector3 yesButtonStartPos, noButtonStartPos, shutdownQuestionStartPos;
    public float shutdownSpeed;
    public float shutdownQuestionSpeed;
    bool shutdownAnimationOut;
    bool shutdownAnimationIn;
    bool shutdownActive;

    void Start()
    {
        shutdownStartPos = shutdownButton.localPosition;
        languageStartPos = languageButton.localPosition;

        englishStartPos = englishButton.localPosition;
        dutchStartPos = dutchButton.localPosition;
        languageQustionStartPos = languageQuestion.localPosition;

        yesButtonStartPos = yesButton.localPosition;
        noButtonStartPos = noButton.localPosition;
        shutdownQuestionStartPos = shutdownQuestion.localPosition;

        borderStartPos = border.localPosition;
        creditsStartPos = credits.localPosition;
    }

    void Update()
    {
        if(settingsAnimationOut)
        {
            SettingsAnimationOpen();
        }
        else if(settingsAnimationIn)
        {
            SettingsAnimationClose();
        }

        if(languageAnimationOut)
        {
            LanguageAnimationOpen();
        }
        else if(languageAnimationIn)
        {
            LanguageAnimationClose();
        }

        if(shutdownAnimationOut)
        {
            ShutdownAnimationOpen();
        }
        else if(shutdownAnimationIn)
        {
            ShutdownAnimationClose();
        }

        if(creditsAnimationOut)
        {
            CreditsAnimationOpen();
        }
        else if(creditsAnimationIn)
        {
            CreditsAnimationClose();
        }
    }

    public void HomeButton()
    {

        if(creditsActive)
        {
            InfoButton();
        }

        if(languageActive)
        {
            LanguageButton();
        }

        if(shutdownActive)
        {
            ShutdownButton();
        }

        if(settingsActive)
        {
            SettingsButton();
        }
    }

    public void TutorialButton()
    {

    }

    public void SettingsButton()
    {
        if(!languageActive && !shutdownActive && !creditsActive)
        {
            if(!settingsActive)
            {
                settingsAnimationOut = true;
                settingsAnimationIn = false;
                settingsActive = true;
            }
            else if(settingsActive)
            {
                settingsAnimationOut = false;
                settingsAnimationIn = true;
                settingsActive = false;
            }
        }
    }

    void SettingsAnimationOpen()
    {
        shutdownButton.localPosition = Vector2.MoveTowards(shutdownButton.localPosition, shutdownTargetPos.localPosition, settingsSpeed * Time.deltaTime);
        languageButton.localPosition = Vector2.MoveTowards(languageButton.localPosition, languageTargetPos.localPosition, settingsSpeed * Time.deltaTime);
        

        if(shutdownButton.position == shutdownTargetPos.position && languageButton.position == languageTargetPos.position)
        {
            settingsAnimationOut = false;
        }
    }

    void SettingsAnimationClose()
    {
        shutdownButton.localPosition = Vector2.MoveTowards(shutdownButton.localPosition, shutdownStartPos, settingsSpeed * Time.deltaTime);
        languageButton.localPosition = Vector2.MoveTowards(languageButton.localPosition, languageStartPos, settingsSpeed * Time.deltaTime);        

        if(shutdownButton.position == shutdownStartPos && languageButton.position == languageStartPos)
        {
            settingsAnimationIn = false;
        }
    }

    public void InfoButton()
    {
        if(!settingsActive)
        {
            if(!creditsActive)
            {
                creditsAnimationOut = true;
                creditsAnimationIn = false;
                creditsActive = true;
            }
            else if(creditsActive)
            {
                creditsAnimationOut = false;
                creditsAnimationIn = true;
                creditsActive = false;
            }
        }
    }

    void CreditsAnimationOpen()
    {
        border.localPosition = Vector2.MoveTowards(border.localPosition, borderTargetPos.localPosition, borderSpeed * Time.deltaTime);
        credits.localPosition = Vector2.MoveTowards(credits.localPosition, creditsTargetPos.localPosition, creditsSpeed * Time.deltaTime);

        if(border.localPosition == borderTargetPos.localPosition && credits.localPosition == creditsTargetPos.localPosition)
        {
            creditsAnimationOut = false;
        }
    }

    void CreditsAnimationClose()
    {
        border.localPosition = Vector2.MoveTowards(border.localPosition, borderStartPos, borderSpeed * Time.deltaTime);
        credits.localPosition = Vector2.MoveTowards(credits.localPosition, creditsStartPos, creditsSpeed * Time.deltaTime);

        if(border.localPosition == borderStartPos && credits.localPosition == creditsStartPos)
        {
            creditsAnimationIn = false;
        }
    }

    public void LanguageButton()
    {
        if(!shutdownActive)
        {
            if(!languageActive)
            {
                languageAnimationOut = true;
                languageAnimationIn = false;
                languageActive = true;
            }
            else if(languageActive)
            {
                languageAnimationOut = false;
                languageAnimationIn = true;
                languageActive = false;
            }
        }
        else if(shutdownActive)
        {
            ShutdownButton();
            LanguageButton();            
        }

    }

    void LanguageAnimationOpen()
    {
        englishButton.localPosition = Vector2.MoveTowards(englishButton.localPosition, englishTargetPos.localPosition, languageSpeed * Time.deltaTime);
        dutchButton.localPosition = Vector2.MoveTowards(dutchButton.localPosition, dutchTargetPos.localPosition, languageSpeed * Time.deltaTime);
        languageQuestion.localPosition = Vector2.MoveTowards(languageQuestion.localPosition, languageQuestionTargetPos.localPosition, languageQuestionSpeed * Time.deltaTime);

        if(englishButton.position == englishTargetPos.position && dutchButton.position == dutchTargetPos.position && languageQuestion.position == languageQuestionTargetPos.position)
        {
            languageAnimationOut = false;
        }
    }

    void LanguageAnimationClose()
    {
        englishButton.localPosition = Vector2.MoveTowards(englishButton.localPosition, englishStartPos, languageSpeed * Time.deltaTime);
        dutchButton.localPosition = Vector2.MoveTowards(dutchButton.localPosition, dutchStartPos, languageSpeed * Time.deltaTime);
        languageQuestion.localPosition = Vector2.MoveTowards(languageQuestion.localPosition, languageQustionStartPos, languageQuestionSpeed * Time.deltaTime);

        if(englishButton.localPosition == englishStartPos && dutchButton.localPosition == dutchStartPos && languageQuestion.localPosition == languageQustionStartPos)
        {
            languageAnimationIn = false;
        }
    }

    public void ShutdownButton()
    {
        if(!languageActive)
        {
            if(!shutdownActive)
            {
                shutdownAnimationOut = true;
                shutdownAnimationIn = false;
                shutdownActive = true;
            }
            else if(shutdownActive)
            {
                shutdownAnimationOut = false;
                shutdownAnimationIn = true;
                shutdownActive = false;
            }
        }
        else if(languageActive)
        {
            LanguageButton();
            ShutdownButton();
        }
    }

    public void ShutdownYesButton()
    {
        Application.Quit();
    }

    public void ShutdownNoButton()
    {
        ShutdownButton();
    }

    void ShutdownAnimationOpen()
    {
        yesButton.localPosition = Vector2.MoveTowards(yesButton.localPosition, yesButtonTargetPos.localPosition, shutdownSpeed * Time.deltaTime);
        noButton.localPosition = Vector2.MoveTowards(noButton.localPosition, noButtonTargetPos.localPosition, shutdownSpeed * Time.deltaTime);
        shutdownQuestion.localPosition = Vector2.MoveTowards(shutdownQuestion.localPosition, shutdownQuestionTargetPos.localPosition, shutdownQuestionSpeed * Time.deltaTime);

        if(yesButton.position == yesButtonTargetPos.position && noButton.position == noButtonTargetPos.position && shutdownQuestion.position == shutdownQuestionTargetPos.position)
        {
            shutdownAnimationOut = false;
        }        
    }

    void ShutdownAnimationClose()
    {
        yesButton.localPosition = Vector2.MoveTowards(yesButton.localPosition, yesButtonStartPos, shutdownSpeed * Time.deltaTime);
        noButton.localPosition = Vector2.MoveTowards(noButton.localPosition, noButtonStartPos, shutdownSpeed * Time.deltaTime);
        shutdownQuestion.localPosition = Vector2.MoveTowards(shutdownQuestion.localPosition, shutdownQuestionStartPos, shutdownQuestionSpeed * Time.deltaTime);

        if(yesButton.localPosition == yesButtonStartPos && noButton.localPosition == noButtonStartPos && shutdownQuestion.localPosition == shutdownQuestionStartPos)
        {
            shutdownAnimationIn = false;
        }
    }
}
