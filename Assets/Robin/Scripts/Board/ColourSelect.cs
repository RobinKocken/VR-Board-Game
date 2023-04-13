using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSelect : MonoBehaviour
{
    public GameManager gameManager;
    public GameManager.PalletColour pallet;

    public float speed;
    public float height;
    float startFloat;
    public float intervals;

    public bool buttonActivate;
    public AudioSource buttonSelect;

    float time;
    Vector3 pos;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        pos = transform.position;
        startFloat = transform.position.x;
    }

    void Update()
    {
        Button();
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

    public void Select()
    {
        gameManager.SelectColour(pallet);
        buttonActivate = true;
        buttonSelect.Play();
    }
}
