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

    public float totalCost;
    public float costPerMeter;
    public float pickUpCost;
    public float maxCost;
    public float profit;       

    public Product[] product;

    List<Transform> placed;
    Transform door;
    Transform sealer;
    public bool bDoor, bSealer;

    void Start()
    {
        
    }

    void CheckDistance()
    {
        totalCost = 0;

        for(int i = 0; i < placed.Count; i++)
        {
            float distanceSealer = Vector3.Distance(placed[i].position, sealer.position);
            float distanceDoor = Vector3.Distance(placed[i].position, door.position);

            totalCost += (int)Mathf.Round(distanceSealer) * costPerMeter + (int)Mathf.Round(distanceDoor) * costPerMeter + pickUpCost;
        }
    }

    public void CheckIfCalculate(GameObject pallet)
    {
        if(PalletColour.door == colour)
        {
            bDoor = true;
            door = pallet.transform;
        }
        else if(PalletColour.sealer == colour)
        {
            bSealer = true;
            sealer = pallet.transform;
        }
        else
        {
            placed.Add(pallet.transform);
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
