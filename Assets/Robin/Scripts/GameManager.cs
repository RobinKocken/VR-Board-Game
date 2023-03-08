using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum PalletColour
    {
        yellow,
        blue,
        red,
        darkGreen,
        white,
        lightGreen,
        darkGrey,
        lightGrey,
        black,
        sealer,
        door,
        empty,
    }

    public PalletColour colour;
    public GameObject selected;

    public float costPerMeter;
    public float pickUpCost;
    public float revenue;
    public float grossMargin;
    public float totalCost;
    public float grossProfit;

    public Product[] product;

    List<GameObject> placed;
    GameObject door;
    GameObject sealer;
    public bool bDoor, bSealer;

    public TMP_Text tRevenue;
    public TMP_Text tTotalCost;
    public TMP_Text tGrossProfit;
    public TMP_Text tGrossMargin;

    void Start()
    {
        TmpVisualize(revenue, totalCost, grossProfit, grossMargin);
    }

    void CheckDistance()
    {
        totalCost = 0;

        for(int i = 0; i < placed.Count; i++)
        {
            float distanceSealer = Vector3.Distance(placed[i].transform.position, sealer.transform.position);
            float distanceDoor = Vector3.Distance(sealer.transform.position, door.transform.position);

            totalCost += (int)Mathf.Round(distanceSealer) * costPerMeter + (int)Mathf.Round(distanceDoor) * costPerMeter + pickUpCost;

            grossProfit = revenue - totalCost;
            grossMargin = (grossProfit / revenue) * 100;
        }

        TmpVisualize(revenue, totalCost, grossProfit, grossMargin);
    }

    public void CheckIfCalculate(GameObject pallet)
    {
        if(PalletColour.door == colour)
        {
            bDoor = true;
            door = pallet;
        }
        else if(PalletColour.sealer == colour)
        {
            bSealer = true;
            sealer = pallet;
        }
        else if(PalletColour.empty == colour)
        {
            for(int i = 0; i < placed.Count; i++)
            {
                Destroy(placed[i]);
                placed.RemoveAt(i);
            }

            Destroy(sealer);
            Destroy(door);

            bSealer = false;
            bDoor = false;
        }
        else
        {
            placed.Add(pallet);
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

    void TmpVisualize(float rev, float cost, float profit, float margin)
    {
        tRevenue.text = $"Revenue: {rev}";
        tTotalCost.text = $"Total Cost: {cost}";
        tGrossProfit.text = $"Gross Profit {profit}";
        tGrossMargin.text = $"Gross Margine {margin}%";
    }

    public void InitializeTmp(TMP_Text rev, TMP_Text cost, TMP_Text profit, TMP_Text margin)
    {
        tRevenue = rev;
        tTotalCost = cost;
        tGrossProfit = profit;
        tGrossMargin = margin;
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
