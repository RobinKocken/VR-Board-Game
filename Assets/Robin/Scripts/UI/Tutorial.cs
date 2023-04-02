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

    void Start()
    {
        tutorial.gameObject.SetActive(false);
    }

        
    void Update()
    {
        tutorial.SetPositionAndRotation(Vector3.MoveTowards(tutorial.position, transform.GetChild(0).position, moveSpeed * Time.deltaTime), Quaternion.Slerp(tutorial.rotation, transform.GetChild(0).rotation, rotSpeed * Time.deltaTime));
    }

    public void TutorialButton()
    {
        if(!tutorialActive)
        {
            tutorialActive = true;

            tutorial.SetPositionAndRotation(transform.GetChild(0).position, transform.GetChild(0).rotation);

            tutorial.gameObject.SetActive(true);
        }
        else if(tutorialActive)
        {
            tutorialActive = false;

            tutorial.gameObject.SetActive(false);
        }
    }
}
