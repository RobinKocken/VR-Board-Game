using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlicker : MonoBehaviour
{
    public Image g;

    public float time;

    public void OnEnable()
    {
        StartCoroutine(Timer(time));
    }

    IEnumerator Timer(float time)
    {
        UnFlicker();
        yield return new WaitForSeconds(time);
        Flicker();
    }

    public void Flicker()
    {
        g.enabled = false;
        this.gameObject.SetActive(false);
    }

    public void UnFlicker()
    {
        g.enabled = true;
    }
}
