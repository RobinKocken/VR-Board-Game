using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("SettingsButton")]
    public Transform shutdownButton;
    public Transform languageButton;
    public Transform wallpaperButton;
    public Transform shutdownTargetPos, languageTargetPos, wallpaperTargetPos;
    Vector3 shutdownStartPos, languageStartPos, wallpaperStartPos;
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
    public bool languageActive;

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
    public bool shutdownActive;

    [Header("Wallpaper")]
    public Image wallpaper;
    public Sprite[] wallpapers;
    public int wallpaperPage;

    public Transform forwardButton;
    public Transform backwardButton;
    public Transform wallpaperQuestion;
    public Transform forwardButtonTargetPos, backwardButtonTarget, wallpaperQuestionTargetPos;
    Vector3 forwardButtonStartPos, backwardButtonStartPos, wallpaperQuestionStartPos;
    public float wallpaperSpeed;
    public float wallpaperQuestionSpeed;
    bool wallpaperAnimationOut;
    bool wallpaperAnimationIn;
    public bool wallpaperActive;

    void Start()
    {
        wallpaperPage = 0;

        shutdownStartPos = shutdownButton.localPosition;
        languageStartPos = languageButton.localPosition;
        wallpaperStartPos = wallpaperButton.localPosition;

        englishStartPos = englishButton.localPosition;
        dutchStartPos = dutchButton.localPosition;
        languageQustionStartPos = languageQuestion.localPosition;

        yesButtonStartPos = yesButton.localPosition;
        noButtonStartPos = noButton.localPosition;
        shutdownQuestionStartPos = shutdownQuestion.localPosition;

        borderStartPos = border.localPosition;
        creditsStartPos = credits.localPosition;

        forwardButtonStartPos = forwardButton.localPosition;
        backwardButtonStartPos = backwardButton.localPosition;
        wallpaperQuestionStartPos = wallpaperQuestion.localPosition;
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

        if(wallpaperAnimationOut)
        {
            WallpaperAnimationOpen();
        }
        else if(wallpaperAnimationIn)
        {
            WallpaperAnimationClose();
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

        if(wallpaperActive)
        {
            WallpaperButton();
        }

        if(settingsActive)
        {
            SettingsButton();
        }

    }

    public void SettingsButton()
    {
        if(!languageActive && !shutdownActive && !creditsActive && !wallpaperActive)
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
        else
        {
            HomeButton();
            SettingsButton();
        }
    }

    void SettingsAnimationOpen()
    {
        shutdownButton.localPosition = Vector2.MoveTowards(shutdownButton.localPosition, shutdownTargetPos.localPosition, settingsSpeed * Time.deltaTime);
        languageButton.localPosition = Vector2.MoveTowards(languageButton.localPosition, languageTargetPos.localPosition, settingsSpeed * Time.deltaTime);
        wallpaperButton.localPosition = Vector2.MoveTowards(wallpaperButton.localPosition, wallpaperTargetPos.localPosition, settingsSpeed * Time.deltaTime);
        

        if(shutdownButton.position == shutdownTargetPos.position && languageButton.position == languageTargetPos.position && wallpaperButton.position == wallpaperTargetPos.position)
        {
            settingsAnimationOut = false;
        }
    }

    void SettingsAnimationClose()
    {
        shutdownButton.localPosition = Vector2.MoveTowards(shutdownButton.localPosition, shutdownStartPos, settingsSpeed * Time.deltaTime);
        languageButton.localPosition = Vector2.MoveTowards(languageButton.localPosition, languageStartPos, settingsSpeed * Time.deltaTime);        
        wallpaperButton.localPosition = Vector2.MoveTowards(wallpaperButton.localPosition, wallpaperStartPos, settingsSpeed * Time.deltaTime);        

        if(shutdownButton.position == shutdownStartPos && languageButton.position == languageStartPos && wallpaperButton.position == wallpaperStartPos)
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
        else if(settingsActive)
        {
            HomeButton();
            InfoButton();
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
        if(!shutdownActive && !wallpaperActive)
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
        else if(wallpaperActive)
        {
            WallpaperButton();
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
        if(!languageActive && !wallpaperActive)
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
        else if(wallpaperActive)
        {
            WallpaperButton();
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

    public void WallpaperButton()
    {
        if(!languageActive && !shutdownActive)
        {
            if(!wallpaperActive)
            {
                wallpaperAnimationOut = true;
                wallpaperAnimationIn = false;
                wallpaperActive = true;
            }
            else if(wallpaperActive)
            {
                wallpaperAnimationOut = false;
                wallpaperAnimationIn = true;
                wallpaperActive = false;
            }
        }
        else if(languageActive)
        {
            LanguageButton();
            WallpaperButton();
        }
        else if(shutdownActive)
        {
            ShutdownButton();
            WallpaperButton();
        }
    }

    public void WallpaperSelect(int number)
    {
        wallpaperPage += number;

        if(wallpaperPage < 0)
        {
            wallpaperPage = 0;
        }
        else if(wallpaperPage > wallpapers.Length - 1)
        {
            wallpaperPage = wallpapers.Length - 1;
        }

        wallpaper.sprite = wallpapers[wallpaperPage];
    }

    void WallpaperAnimationOpen()
    {
        forwardButton.localPosition = Vector2.MoveTowards(forwardButton.localPosition, forwardButtonTargetPos.localPosition, wallpaperSpeed * Time.deltaTime);
        backwardButton.localPosition = Vector2.MoveTowards(backwardButton.localPosition, backwardButtonTarget.localPosition, shutdownSpeed * Time.deltaTime);
        wallpaperQuestion.localPosition = Vector2.MoveTowards(wallpaperQuestion.localPosition, wallpaperQuestionTargetPos.localPosition, wallpaperQuestionSpeed * Time.deltaTime);

        if(forwardButton.position == forwardButtonTargetPos.position && backwardButton.position == backwardButtonTarget.position && wallpaperQuestion.position == wallpaperQuestionTargetPos.position)
        {
            wallpaperAnimationOut = false;
        }
    }

    void WallpaperAnimationClose()
    {
        forwardButton.localPosition = Vector2.MoveTowards(forwardButton.localPosition, forwardButtonStartPos, wallpaperSpeed * Time.deltaTime);
        backwardButton.localPosition = Vector2.MoveTowards(backwardButton.localPosition, backwardButtonStartPos, wallpaperSpeed * Time.deltaTime);
        wallpaperQuestion.localPosition = Vector2.MoveTowards(wallpaperQuestion.localPosition, wallpaperQuestionStartPos, wallpaperQuestionSpeed * Time.deltaTime);


        if(forwardButton.localPosition == forwardButtonStartPos && backwardButton.localPosition == backwardButtonStartPos && wallpaperQuestion.position == wallpaperQuestionStartPos)
        {
            wallpaperAnimationIn = false;
        }
    }
}
