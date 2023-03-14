using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSelect : MonoBehaviour
{
    public GameManager gameManager;

    public GameManager.LayerNumber layer;

    public float speed;
    public float height;
    float startFloat;
    public float intervals;

    public bool buttonActivate;
    float time;
    Vector3 pos;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        Button();
    }

    public void SelectLayer()
    {
        gameManager.LayerSelect(layer);
        buttonActivate = true;
    }

    void Button()
    {
        if(buttonActivate)
        {
            time += intervals * Time.deltaTime;

            float newFloat = startFloat + height * ((Mathf.Sin(time * speed) + 1) / 2);
            transform.position = new Vector3(newFloat, transform.position.y, transform.position.z);

            if(transform.position == pos && time > 0.05f)
            {
                time = 0;
                buttonActivate = false;
            }
        }
    }
}
