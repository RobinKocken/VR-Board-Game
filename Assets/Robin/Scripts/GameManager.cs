using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum PalletColour
    {
        yellow,
        blue,
        red,
        green,
        white,
        lightGreen,
        darkGrey,
        lightGrey,
        black,
        door,
        sealer,
        empty,
    }
    public PalletColour colour;

    public GameObject selected;

    public Product[] product;

    public float costPerMeter;
    public float pickUpCost;   

    GameObject[] placed;

    public bool door, sealer;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void CheckDistance()
    {
        


    }

    public void CheckIfCalculate()
    {
        if(PalletColour.door == colour)
        {
            door = true;
        }

        if(PalletColour.sealer == colour)
        {
            sealer = true;
        }

        if(door && sealer)
        {
            CheckDistance();
        }
    }

    void SelectPallet(GameObject pallet)
    {
        //selected = product[(int)colour].pallet;
        selected = pallet;
    }

    public void SelectColour(PalletColour pallet)
    {
        colour = pallet;

        for(int i = 0; i < product.Length; i++)
        {
            if(product[i].colour == pallet)
            {
                SelectPallet(product[i].pallet);
                return;
            }
        }        
    }
}

[System.Serializable]
public class Product
{
    public GameManager.PalletColour colour;
    public GameObject pallet;
    public int amount;
    public int demand;
}
