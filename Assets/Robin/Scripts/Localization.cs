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

    public Transform flagPos;
    public GameObject englishFlag;
    public GameObject dutchFlag;

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
        englishFlag = Instantiate(englishFlag, flagPos.position, flagPos.rotation);
        dutchFlag = Instantiate(dutchFlag, flagPos.position, flagPos.rotation);
        dutchFlag.SetActive(false);
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

        englishFlag.SetActive(false);
        dutchFlag.SetActive(true);

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

        dutchFlag.SetActive(false);
        englishFlag.SetActive(true);

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
