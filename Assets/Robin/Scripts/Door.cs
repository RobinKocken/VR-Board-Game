using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Transform player;
    public Transform telPos;

    public AudioSource doorSound;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Teleport()
    {
        player.position = telPos.position;
        doorSound.Play();
    }

}
