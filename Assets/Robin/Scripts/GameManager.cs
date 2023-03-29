using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;
using System.Diagnostics.Contracts;

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
        delete,
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
    public List<GameObject> floorGrid;

    public TMP_Text tRevenue;
    public TMP_Text tTotalCost;
    public TMP_Text tGrossProfit;
    public TMP_Text tGrossMargin;

    public string sRevenue;
    public string sTotalCost;
    public string sGrossProfit;
    public string sGrossMargin;

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

    void Calculate()
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

            Debug.Log((int)Mathf.Round(distanceSealer));
            Debug.Log(distanceSealer);

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
            layers[(int)layer].bDoor = true;
            layers[(int)layer].door = pallet;
        }
        else if(colour == PalletColour.sealer)
        {
            layers[(int)layer].bSealer = true;
            layers[(int)layer].sealer = pallet;
        }
        else
        {
            layers[(int)layer].placed.Add(pallet);
        }

        if(layers[(int)layer].door && layers[(int)layer].sealer)
        {
            Calculate();
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
        else if(colour == PalletColour.delete)
        {
            return;
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
        tRevenue.text = $"{sRevenue} {rev}";
        tTotalCost.text = $"{sTotalCost} {cost}";
        tGrossProfit.text = $"{sGrossProfit} {profit}";
        tGrossMargin.text = $"{sGrossMargin} {margin}%";
    }

    public void TmpLanguageUpdate()
    {
        TmpVisualize(layers[(int)layer].revenue, layers[(int)layer].totalCost, layers[(int)layer].grossProfit, layers[(int)layer].grossMargin);
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
        for(int i = 0; i < layers[(int)layer].currentAmount.Count - 2; i++)
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

    public void InitializeGrid(List<GameObject> l, List<GameObject> f)
    {
        grid = l;
        floorGrid = f;

        for(int i = 0; i < layers.Length; i++)
        {
            for(int j = 0; j < grid.Count; j++)
            {
                layers[i].gridBool.Add(grid[j].GetComponent<Placement>().placeable);
            }
        }
    }

    public void Delete(GameObject gb)
    {
        if(gb.GetComponent<Pallet>().pallet == PalletColour.door)
        {
            layers[(int)layer].bDoor = false;
            layers[(int)layer].door = null;

            layers[(int)layer].currentAmount[(int)gb.GetComponent<Pallet>().pallet] += 1;
            CurrentAmount();

            Destroy(gb);
        }
        else if(gb.GetComponent<Pallet>().pallet == PalletColour.sealer)
        {
            layers[(int)layer].bSealer = false;
            layers[(int)layer].sealer = null;

            layers[(int)layer].currentAmount[(int)gb.GetComponent<Pallet>().pallet] += 1;
            CurrentAmount();

            Destroy(gb);
        }
        else
        {
            for(int i = 0; i < layers[(int)layer].placed.Count; i++)
            {
                if(layers[(int)layer].placed[i] == gb)
                {
                    layers[(int)layer].currentAmount[(int)gb.GetComponent<Pallet>().pallet] += 1;
                    CurrentAmount();

                    layers[(int)layer].placed.RemoveAt(i);
                    Destroy(gb);

                    return;
                }
            }
        }
    }

    void Empty()
    {
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

        for(int i = 0; i < product.Length - 2; i++)
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

    void CheckIfValid()
    {
        for(int i = 0; i < layers[(int)layer].placed.Count; i++)
        {

        }
    }

    public void FloorPlacment()
    {
        for(int i = 0; i < floorGrid.Count; i++)
        {

        }
    }
}

[System.Serializable]
public class Product
{
    public GameManager.PalletColour colour;
    public GameObject pallet;
    public GameObject floorPallet;
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
