using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Testing : MonoBehaviour
{
    public XRRayInteractor rightRay;


    void Start()
    {
        Grid grid = new Grid(4, 2, 1);

        
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
