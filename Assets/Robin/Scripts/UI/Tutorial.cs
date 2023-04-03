using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Transform tutorial;
    public float moveSpeed;
    public float rotSpeed;

    public bool tutorialActive;

    public GameObject[] engVersion;
    public GameObject[] nlVersion;

    public int pageNumber;
    public bool language;

    void Start()
    {
        tutorial.gameObject.SetActive(false);
        pageNumber = 0;
    }

    void Update()
    {
        if(tutorialActive)
        {
            tutorial.SetPositionAndRotation(Vector3.MoveTowards(tutorial.position, transform.GetChild(0).position, moveSpeed * Time.deltaTime), Quaternion.Slerp(tutorial.rotation, transform.GetChild(0).rotation, rotSpeed * Time.deltaTime));
        }
    }

    public void TutorialButton()
    {
        if(!tutorialActive)
        {
            tutorialActive = true;

            tutorial.SetPositionAndRotation(transform.GetChild(0).position, transform.GetChild(0).rotation);

            tutorial.gameObject.SetActive(true);

            pageNumber = 0;

            if(!language)
            {
                engVersion[pageNumber].SetActive(true);
            }
            else if(language)
            {
                nlVersion[pageNumber].SetActive(true);
            }
        }
        else if(tutorialActive)
        {
            tutorialActive = false;

            tutorial.gameObject.SetActive(false);

            for(int i = 0; i < engVersion.Length; i++)
            {
                engVersion[i].SetActive(false);
                nlVersion[i].SetActive(false);
            }
        }
    }

    public void Forward()
    {       
        pageNumber++;

        if(!language)
        {           
            if(pageNumber > engVersion.Length - 1)
            {
                pageNumber = engVersion.Length - 1;
            }
            else
            {
                engVersion[pageNumber - 1].SetActive(false);

                engVersion[pageNumber].SetActive(true);
            }            
        }
        else if(language)
        {
            if(pageNumber > nlVersion.Length - 1)
            {
                pageNumber = nlVersion.Length - 1;
            }
            else
            {
                nlVersion[pageNumber - 1].SetActive(false);

                nlVersion[pageNumber].SetActive(true);
            }
        }
    }

    public void Back()
    {
        pageNumber--;

        if(!language)
        {
            if(pageNumber < 0)
            {
                pageNumber = 0;
            }
            else
            {
                engVersion[pageNumber + 1].SetActive(false);

                engVersion[pageNumber].SetActive(true);
            }
        }
        else if(language)
        {
            if(pageNumber < 0)
            {
                pageNumber = 0;
            }
            else
            {
                nlVersion[pageNumber + 1].SetActive(false);

                nlVersion[pageNumber].SetActive(true);
            }
        }
    }

    public void English()
    {
        language = false;

        nlVersion[pageNumber].SetActive(false);
        engVersion[pageNumber].SetActive(true);
    }

    public void Dutch()
    {
        language = true;

        engVersion[pageNumber].SetActive(false);
        nlVersion[pageNumber].SetActive(true);
    }
}
