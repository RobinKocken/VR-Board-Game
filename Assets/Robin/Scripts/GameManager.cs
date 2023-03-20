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

    public enum LayerNumber
    {
        layer1,
        layer2,
        layer3,
        layer4,
        layer5,
    }

    public PalletColour colour;
    public LayerNumber layer;

    public GameObject selected;

    public float costPerMeter;
    public float pickUpCost;

    public Product[] product;

    public List<GameObject> grid;

    public TMP_Text tRevenue;
    public TMP_Text tTotalCost;
    public TMP_Text tGrossProfit;
    public TMP_Text tGrossMargin;

    public Layers[] layers;

    void Awake()
    {
        InitializeLayers();
    }

    void Start()
    {
        TmpVisualize(layers[(int)layer].revenue, layers[(int)layer].totalCost, layers[(int)layer].grossProfit, layers[(int)layer].grossMargin);
        CurrentAmount();
    }

    void CheckDistance()
    {
        layers[(int)layer].totalCost = 0;

        for(int i = 0; i < product.Length; i++)
        {
            if(layers[(int)layer].currentAmount[i] < product[i].amount)
            {
                layers[(int)layer].totalCost += (product[i].demand * pickUpCost);
                Debug.Log($"Total Cost: {product[i].demand * pickUpCost}");
            }
        }

        for(int i = 0; i < layers[(int)layer].placed.Count; i++)
        {
            float distanceSealer = Vector3.Distance(layers[(int)layer].placed[i].transform.position, layers[(int)layer].sealer.transform.position) * 10;
            float distanceDoor = Vector3.Distance(layers[(int)layer].sealer.transform.position, layers[(int)layer].door.transform.position) * 10;

            float f = product[(int)layers[(int)layer].placed[i].GetComponent<Pallet>().pallet].demand /
                product[(int)layers[(int)layer].placed[i].GetComponent<Pallet>().pallet].amount -
                layers[(int)layer].currentAmount[(int)layers[(int)layer].placed[i].GetComponent<Pallet>().pallet];

            layers[(int)layer].totalCost += ((int)Mathf.Round(distanceSealer) * costPerMeter) * f + ((int)Mathf.Round(distanceDoor) * costPerMeter) * f;

            layers[(int)layer].grossProfit = layers[(int)layer].revenue - layers[(int)layer].totalCost;
            layers[(int)layer].grossMargin = (layers[(int)layer].grossProfit / layers[(int)layer].revenue) * 100;
        }

        TmpVisualize(layers[(int)layer].revenue, layers[(int)layer].totalCost, layers[(int)layer].grossProfit, layers[(int)layer].grossMargin);
    }

    public void CheckIfCalculate(GameObject pallet)
    {
        if(colour == PalletColour.door)
        {
            Debug.Log("Door");

            layers[(int)layer].bDoor = true;
            layers[(int)layer].door = pallet;
        }
        else if(colour == PalletColour.sealer)
        {
            Debug.Log("Sealer");

            layers[(int)layer].bSealer = true;
            layers[(int)layer].sealer = pallet;
        }
        else
        {
            Debug.Log("Placed");

            layers[(int)layer].placed.Add(pallet);
        }

        if(layers[(int)layer].door && layers[(int)layer].sealer)
        {
            CheckDistance();
        }
    }

    void SelectPallet(GameObject pallet)
    {
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
            product[i].amountText.text = $"x {layers[(int)layer].currentAmount[i]}";
        }
    }

    public void CurrentAmount()
    {
        for(int i = 0; i < layers[(int)layer].currentAmount.Count - 1; i++)
        {
            product[i].amountText.text = $"x {layers[(int)layer].currentAmount[i]}";
        }
    }

    public void SetCurrentAmount(int number)
    {
        layers[(int)layer].currentAmount[(int)colour] -= number;
        product[(int)colour].amountText.text = $"x {layers[(int)layer].currentAmount[(int)colour]}";
    }

    void ResetCurrentAmount()
    {
        for(int i = 0; i < product.Length; i++)
        {
            layers[(int)layer].currentAmount[i] = product[i].amount;
        }
    }

    public void InitializeGrid(List<GameObject> l)
    {
        grid = l;

        for(int i = 0; i < layers.Length; i++)
        {
            for(int j = 0; j < grid.Count; j++)
            {
                layers[i].gridBool.Add(grid[j].GetComponent<Placement>().placeable);
            }
        }
    }

    void Empty()
    {
        Debug.Log("Empty;");

        for(int i = 0; i < layers[(int)layer].placed.Count; i++)
        {
            Destroy(layers[(int)layer].placed[i]);
        }

        layers[(int)layer].placed.Clear();

        Destroy(layers[(int)layer].sealer);
        Destroy(layers[(int)layer].door);

        layers[(int)layer].bSealer = false;
        layers[(int)layer].bDoor = false;


        layers[(int)layer].grossMargin = 0;
        layers[(int)layer].grossProfit = 0;
        layers[(int)layer].totalCost = 0;

        for(int i = 0; i < product.Length - 1; i++)
        {
            layers[(int)layer].currentAmount[i] = product[i].amount;
            product[i].amountText.text = $"x {layers[(int)layer].currentAmount[i]}";
        }

        for(int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<Placement>().placeable = true;

            layers[(int)layer].gridBool[i] = grid[i].GetComponent<Placement>().placeable;
        }

        ResetCurrentAmount();
    }

    public void LayerSelect(LayerNumber layerSelected)
    {
        for(int i = 0; i < layers[(int)layer].placed.Count; i++)
        {
            layers[(int)layer].placed[i].SetActive(false);
        }

        for(int i = 0; i < grid.Count; i++)
        {
            layers[(int)layer].gridBool[i] = grid[i].GetComponent<Placement>().placeable;
        }

        if(layers[(int)layer].sealer != null)   
            layers[(int)layer].sealer.SetActive(false);

        if(layers[(int)layer].door != null)
            layers[(int)layer].door.SetActive(false);

        layer = layerSelected;

        for(int i = 0; i < layers[(int)layer].placed.Count; i++)
        {
            layers[(int)layer].placed[i].SetActive(true);
        }

        for(int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<Placement>().placeable = layers[(int)layer].gridBool[i];
        }

        if(layers[(int)layer].sealer != null)
            layers[(int)layer].sealer.SetActive(true);

        if(layers[(int)layer].door != null)
            layers[(int)layer].door.SetActive(true);

        CurrentAmount();
        TmpVisualize(layers[(int)layer].revenue, layers[(int)layer].totalCost, layers[(int)layer].grossProfit, layers[(int)layer].grossMargin);
    }

    void InitializeLayers()
    {
        for(int i = 0; i < layers.Length; i++)
        {

            for(int j = 0; j < product.Length; j++)
            {
                layers[i].currentAmount.Add(product[j].amount);
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
    public TMP_Text amountText;
}

[System.Serializable]
public class Layers
{
    public GameManager.LayerNumber layer;
    public List<bool> gridBool;
    public List<GameObject> placed;
    public GameObject sealer;
    public GameObject door;
    public bool bDoor, bSealer;
    public List<int> currentAmount;

    public float revenue;
    public float grossMargin;
    public float totalCost;
    public float grossProfit;
}
