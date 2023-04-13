using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    Localization local;

    public GameObject computer;
    public GameObject tutorial;
    public GameObject setting;

    void Awake()
    {
        local = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Localization>();
    }

    public void Setting()
    {
        computer.SetActive(false);
        setting.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Dutch()
    {
        local.Dutch();

        computer.SetActive(true);
        setting.SetActive(false);
    }

    public void English()
    {
        local.English();

        computer.SetActive(true);
        setting.SetActive(false);
    }
}
