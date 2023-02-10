using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum UIState
    {
        Disabled,
        MainMenu,
        PauseMenu,
        Settings
    }

    public SaveData saveData;

    public UIState uiState;

    [Header("Main Menu")]
    public string mainMenuName;
    public string mMenuMarkup;
    [Space]
    public GameObject mainMenu = null;
    [Space]
    public TMP_Text menuText;

    [Header("Pause Menu")]
    public string pauseMenuName;
    public string pMenuMarkup;
    [Space]
    public GameObject pauseUI = null;
    [Space]
    public TMP_Text pMenuText;

    [Header("Settings Menu")]
    public string settingsMenuName;
    public string sMenuMarkup;
    [Space]
    public GameObject settingsMenu = null;
    [Space]
    public TMP_Text sMenuText;

    public void Awake()
    {
        SaveManager.Load();

        Initialize();
    }

    private void Update()
    {
        CheckUIState();
    }

    public void CheckUIState()
    {
        switch (uiState)
        {
            case UIState.Disabled:
                mainMenu.SetActive(false);
                pauseUI.SetActive(false);
                settingsMenu.SetActive(false);
                break;

            case UIState.MainMenu:
                mainMenu.SetActive(true);

                if(menuText.text != mainMenuName)
                {
                    menuText.text = GetName(menuText, mainMenuName, mMenuMarkup);
                }

                pauseUI.SetActive(false);
                settingsMenu.SetActive(false);
                break;

            case UIState.PauseMenu:
                pauseUI.SetActive(true);

                if (pMenuText.text != pauseMenuName)
                {
                    pMenuText.text = GetName(pMenuText, pauseMenuName, pMenuMarkup);
                }

                mainMenu.SetActive(false);
                settingsMenu.SetActive(false);
                break;

            case UIState.Settings:
                settingsMenu.SetActive(true);

                if (sMenuText.text != settingsMenuName)
                {
                    sMenuText.text = GetName(sMenuText, settingsMenuName, sMenuMarkup);
                }

                mainMenu.SetActive(false);
                pauseUI.SetActive(false);
                break;
        }
    }

    public void StartSimulation(string lvlToLoad)
    {
        SceneManager.LoadScene(lvlToLoad);
    }

    public void OpenSettings()
    {
        uiState = UIState.Settings;
    }

    public void BackToMain(string sceneToLoad)
    {
        SaveManager.Save(saveData);

        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitSimulation()
    {
        SaveManager.Save(saveData);

        Application.Quit();
    }

    public void Initialize()
    {
        switch(uiState)
        {
            case UIState.Disabled:
                settingsMenu.SetActive(false);
                mainMenu.SetActive(false);
                pauseUI.SetActive(false);
                break;

            case UIState.MainMenu:
                mainMenu.SetActive(true);
                menuText.text = GetName(menuText, mainMenuName, mMenuMarkup);

                settingsMenu.SetActive(false);
                pauseUI.SetActive(false);
                break;

            case UIState.PauseMenu:
                pauseUI.SetActive(true);
                settingsMenu.SetActive(false);
                mainMenu.SetActive(false);
                break;
        }
    }

    public string GetName(TMP_Text inputText, string title, string markupType)
    {
        return inputText.text.Replace(markupType, title);
    }
}
