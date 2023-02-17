using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum PalletColour
    {
        red,
        blue,
        green,
    }
    public PalletColour colour;

    public GameObject selected;
    public GameObject[] pallets;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void SelectPallet()
    {
        switch(colour)
        {
            case PalletColour.red:
                selected = pallets[0];
                break;
            case PalletColour.blue:
                selected = pallets[1];
                break;
            case PalletColour.green:
                selected = pallets[2];
                break;
        }
    }

    public void SelectColour(PalletColour pallet)
    {
        colour = pallet;
        SelectPallet();
    }
}
