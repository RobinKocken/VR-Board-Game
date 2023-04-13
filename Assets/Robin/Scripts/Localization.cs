using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Localization : MonoBehaviour
{
    public GameManager gameManager;
    public Language[] language;

    public string engRevenue;
    public string engTotalCost;
    public string engGrossProfit;
    public string engGrossMargin;

    public string nlRevenue;
    public string nlTotalCost;
    public string nlGrossProfit;
    public string nlGrossMargin;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        gameManager.sRevenue = engRevenue;
        gameManager.sTotalCost = engTotalCost;
        gameManager.sGrossProfit = engGrossProfit;
        gameManager.sGrossMargin = engGrossMargin;
    }

    void Start()
    {

    }

    public void Dutch()
    {
        for(int i = 0; i < language.Length; i++)
        {
            language[i].uiText.text = language[i].dutch;
            language[i].uiText.fontSize = language[i].fontSizeD;
        }

        gameManager.sRevenue = nlRevenue;
        gameManager.sTotalCost = nlTotalCost;
        gameManager.sGrossProfit = nlGrossProfit;
        gameManager.sGrossMargin = nlGrossMargin;

        gameManager.TmpLanguageUpdate();
    }
    public void English()
    {
        for(int i = 0; i < language.Length; i++)
        {
            language[i].uiText.text = language[i].english;
            language[i].uiText.fontSize = language[i].fontSizeE;
        }

        gameManager.sRevenue = engRevenue;
        gameManager.sTotalCost = engTotalCost;
        gameManager.sGrossProfit =engGrossProfit;
        gameManager.sGrossMargin = engGrossMargin;

        gameManager.TmpLanguageUpdate();
    }
}

[System.Serializable]
public class Language
{
    public string english;
    public float fontSizeE;
    public string dutch;
    public float fontSizeD;
    public TMP_Text uiText;
}
