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

    float count;

    public Product[] product;

    public List<GameObject> placed;
    public List<GameObject> grid;
    GameObject door;
    GameObject sealer;
    public bool bDoor, bSealer;

    public TMP_Text tRevenue;
    public TMP_Text tTotalCost;
    public TMP_Text tGrossProfit;
    public TMP_Text tGrossMargin;

    void Start()
    {
        SetCurrentAmount();
        TmpVisualize(revenue, totalCost, grossProfit, grossMargin);
    }

    void CheckDistance()
    {
        totalCost = 0;
        count = 0;

        for(int i = 0; i < product.Length; i++)
        {
            if(product[i].currentAmount < product[i].amount)
            {
                totalCost += product[i].demand * pickUpCost;
            }
        }

        for(int i = 0; i < placed.Count; i++)
        {
            float distanceSealer = Vector3.Distance(placed[i].transform.position, sealer.transform.position) * 10;
            float distanceDoor = Vector3.Distance(sealer.transform.position, door.transform.position) * 10;

            Debug.Log($"S: {(int)Mathf.Round(distanceSealer)} D: {(int)Mathf.Round(distanceDoor)}");

            totalCost += ((int)Mathf.Round(distanceSealer) * costPerMeter) + (int)Mathf.Round(distanceDoor) * costPerMeter;

            float f = product[(int)placed[i].GetComponent<Pallet>().pallet].demand / (product[(int)placed[i].GetComponent<Pallet>().pallet].amount - product[(int)placed[i].GetComponent<Pallet>().pallet].currentAmount);

            totalCost += ((int)Mathf.Round(distanceSealer) * costPerMeter) * f + ((int)Mathf.Round(distanceDoor) * costPerMeter) * f;

            grossProfit = revenue - totalCost;
            grossMargin = (grossProfit / revenue) * 100;
        }

        TmpVisualize(revenue, totalCost, grossProfit, grossMargin);
    }

    public void CheckIfCalculate(GameObject pallet)
    {
        if(colour == PalletColour.door)
        {
            Debug.Log("Door");
            bDoor = true;
            door = pallet;
        }
        else if(colour == PalletColour.sealer)
        {
            Debug.Log("Sealer");
            bSealer = true;
            sealer = pallet;
        }
        else
        {
            Debug.Log("Placed");
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

        if(colour == PalletColour.empty)
        {
            Empty();
        }
        else
        {
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

    void TmpVisualize(float rev, float cost, float profit, float margin)
    {
        tRevenue.text = $"Revenue: {rev}";
        tTotalCost.text = $"Total Cost: {cost}";
        tGrossProfit.text = $"Gross Profit {profit}";
        tGrossMargin.text = $"Gross Margine {margin}%";
    }

    public void InitializeTmp(TMP_Text rev, TMP_Text cost, TMP_Text profit, TMP_Text margin, TMP_Text[] amount)
    {
        tRevenue = rev;
        tTotalCost = cost;
        tGrossProfit = profit;
        tGrossMargin = margin;

        for(int i = 0; i < amount.Length; i++)
        {
            product[i].amountText = amount[i];
            product[i].amountText.text = $"x {product[i].amount}";
        }
    }

    public void CurrentAmount()
    {
        product[(int)colour].currentAmount -= 1;
        product[(int)colour].amountText.text = $"x {product[(int)colour].currentAmount}";
    }

    void SetCurrentAmount()
    {
        for(int i = 0; i < product.Length; i++)
        {
            product[i].currentAmount = product[i].amount;
        }
    }

    public void InitializeGrid(List<GameObject> l)
    {
        grid = l;
    }

    void Empty()
    {
        Debug.Log("Empty;");
        for(int i = 0; i < placed.Count; i++)
        {
            Destroy(placed[i]);
        }

        placed.Clear();

        Destroy(sealer);
        Destroy(door);

        bSealer = false;
        bDoor = false;

        grossMargin = 0;
        grossProfit = 0;
        totalCost = 0;

        for(int i = 0; i < product.Length - 1; i++)
        {
            product[i].currentAmount = product[i].amount;
            product[i].amountText.text = $"x {product[i].currentAmount}";
        }

        for(int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<Placement>().placeable = false;
        }

        SetCurrentAmount();
    }
}

[System.Serializable]
public class Product
{
    public GameManager.PalletColour colour;
    public GameObject pallet;
    public int amount;
    public int currentAmount;
    public int demand;
    public TMP_Text amountText;
}
