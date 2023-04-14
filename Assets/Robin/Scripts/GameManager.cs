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
    public List<GameObject> gridFloor;

    public TMP_Text tRevenue;
    public TMP_Text tTotalCost;
    public TMP_Text tGrossProfit;
    public TMP_Text tGrossMargin;

    public string sRevenue;
    public string sTotalCost;
    public string sGrossProfit;
    public string sGrossMargin;

    public ParticleSystem confetti;
    public AudioSource winSound;

    public Layers[] layers;

    void Awake()
    {
        InitializeLayers();
    }

    void Start()
    {
        CurrentAmount();
    }

    void WinCondition()
    {
        for(int i = 0; i < layers[(int)layer].currentAmount.Count - 2; i++)
        {
            if(layers[(int)layer].currentAmount[i] > 0)
            {
                return;
            }
        }

        confetti.Play();
        winSound.Play();
    }

    void Calculate()
    {
        layers[(int)layer].totalCost = 0;
        float totalDemand = 0;

        for(int i = 0; i < product.Length; i++)
        {
            layers[(int)layer].totalCost += product[i].demand * pickUpCost;
            totalDemand += product[i].demand;
        }

        int xDistance = layers[(int)layer].sealer.GetComponent<Pallet>().x - layers[(int)layer].door.GetComponent<Pallet>().x;

        if(xDistance < 0)
            xDistance *= -1;

        int yDistance = layers[(int)layer].sealer.GetComponent<Pallet>().y - layers[(int)layer].door.GetComponent<Pallet>().y;

        if(yDistance < 0)
            yDistance *= -1;

        layers[(int)layer].totalCost += ((xDistance + yDistance) * costPerMeter) * totalDemand;

        for(int i = 0; i < layers[(int)layer].placed.Count; i++)
        {
            for(int j = 0; j < product.Length; j++)
            {
                if(layers[(int)layer].placed[i].GetComponent<Pallet>().pallet == product[j].colour)
                {
                    int xDistanceToSealer = layers[(int)layer].placed[i].GetComponent<Pallet>().x - layers[(int)layer].sealer.GetComponent<Pallet>().x;

                    if(xDistanceToSealer < 0)
                        xDistanceToSealer *= -1;

                    int yDistanceToSealer = layers[(int)layer].placed[i].GetComponent<Pallet>().y - layers[(int)layer].sealer.GetComponent<Pallet>().y;

                    if(yDistanceToSealer < 0)
                        yDistanceToSealer *= -1;

                    layers[(int)layer].totalCost += ((xDistanceToSealer + yDistanceToSealer) * costPerMeter) * (product[j].demand / product[j].amount);
                }
            }
        }

        layers[(int)layer].grossProfit = layers[(int)layer].revenue - layers[(int)layer].totalCost;
        layers[(int)layer].grossMargin = (layers[(int)layer].grossProfit / layers[(int)layer].revenue) * 100;

        layers[(int)layer].grossMargin = (float)System.Math.Round(layers[(int)layer].grossMargin, 2);

        TmpVisualize(layers[(int)layer].revenue, layers[(int)layer].totalCost, layers[(int)layer].grossProfit, layers[(int)layer].grossMargin);
        WinCondition();
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
        if(product[(int)colour].amountText != null)
        {
            product[(int)colour].amountText.color = Color.black;
        }

        colour = pallet;

        if(product[(int)colour].amountText != null)
        {
            product[(int)colour].amountText.color = Color.green;
        }

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

    public void TmpVisualize(float rev, float cost, float profit, float margin)
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
        gridFloor = f;

        for(int i = 0; i < layers.Length; i++)
        {
            for(int j = 0; j < grid.Count; j++)
            {
                layers[i].gridBool.Add(grid[j].GetComponent<Placement>().placed);
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

        for(int i = 0; i < layers[(int)layer].placedFloor.Count; i++)
        {
            Destroy(layers[(int)layer].placedFloor[i]);
        }

        layers[(int)layer].placedFloor.Clear();

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
            grid[i].GetComponent<Placement>().placed = false;

            layers[(int)layer].gridBool[i] = grid[i].GetComponent<Placement>().placed;
        }

        ResetCurrentAmount();
        TmpVisualize(layers[(int)layer].revenue, layers[(int)layer].totalCost, layers[(int)layer].grossProfit, layers[(int)layer].grossMargin);

    }

    public void LayerSelect(LayerNumber layerSelected)
    {
        for(int i = 0; i < layers[(int)layer].placed.Count; i++)
        {
            layers[(int)layer].placed[i].SetActive(false);
        }

        for(int i = 0; i < layers[(int)layer].placedFloor.Count; i++)
        {
            layers[(int)layer].placedFloor[i].SetActive(false);
        }

        for(int i = 0; i < grid.Count; i++)
        {
            layers[(int)layer].gridBool[i] = grid[i].GetComponent<Placement>().placed;
            grid[i].GetComponent<Placement>().pallet = null;
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

        for(int i = 0; i < layers[(int)layer].placedFloor.Count; i++)
        {
            layers[(int)layer].placedFloor[i].SetActive(true);
        }

        for(int i = 0; i < grid.Count; i++)
        {
            grid[i].GetComponent<Placement>().placed = layers[(int)layer].gridBool[i];

            for(int j = 0; j < layers[(int)layer].placed.Count; j++)
            {
                if(layers[(int)layer].placed[j].GetComponent<Pallet>().x == grid[i].GetComponent<Placement>().x)
                {
                    if(layers[(int)layer].placed[j].GetComponent<Pallet>().y == grid[i].GetComponent<Placement>().y)
                    {
                        grid[i].GetComponent<Placement>().pallet = layers[(int)layer].placed[j];
                    }
                }
            }
        }

        if(layers[(int)layer].sealer != null)
            layers[(int)layer].sealer.SetActive(true);

        if(layers[(int)layer].door != null)
            layers[(int)layer].door.SetActive(true);

        CurrentAmount();
        TmpVisualize(layers[(int)layer].revenue, layers[(int)layer].totalCost, layers[(int)layer].grossProfit, layers[(int)layer].grossMargin);
    }

    public void DeleteFloor(int x, int y)
    {
        for(int i = 0; i < gridFloor.Count; i++)
        {
            if(gridFloor[i].GetComponent<Placement>().x == x)
            {
                if(gridFloor[i].GetComponent<Placement>().y == y)
                {
                    for(int j = 0; j < layers[(int)layer].placedFloor.Count; j++)
                    {
                        if(layers[(int)layer].placedFloor[j] == gridFloor[i].transform.GetChild(0).gameObject)
                        {
                            layers[(int)layer].placedFloor.RemoveAt(j);
                            Destroy(gridFloor[i].transform.GetChild(0).gameObject);

                            return;
                        }
                    }
                }
            }
        }
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

    public void FloorPlacment(int x, int y)
    {
        for(int i = 0; i < gridFloor.Count; i++)
        {
            if(gridFloor[i].GetComponent<Placement>().x == x)
            {
                if(gridFloor[i].GetComponent<Placement>().y == y)
                {
                    GameObject pallet = Instantiate(product[(int)colour].floorPallet, gridFloor[i].transform.position, Quaternion.identity, gridFloor[i].transform);
                    layers[(int)layer].placedFloor.Add(pallet);
                    return;
                }
            }
        }
    }

    public bool StartValidation(bool isValid, int x, int y)
    {
        //Debug.Log(x + "-" + y);

        for(int i = 0; i < layers[(int)layer].placed.Count; i++)
        {
            isValid = CheckForPos(false, isValid, layers[(int)layer].placed[i].GetComponent<Pallet>().x, layers[(int)layer].placed[i].GetComponent<Pallet>().y, x, y);

            if(!isValid)
            {
                return isValid;
            }
        }
        
        if(isValid)
        {
            isValid = CheckForPos(true, isValid, x, y, 0, 0);
        }
        else if(!isValid)
        {
            if(layers[(int)layer].placed.Count == 0)
            {
                isValid = true;
            }
        }

        return isValid;
    }

    bool CheckForPos(bool isCurrent ,bool isValid, int x, int y, int potentialX, int potentialY)
    {
        int n = 0;

        if(x == 0)
        {
            if(y == 0)
            {
                n = Validate(isCurrent, x, y, 1, 1, 0, 0, n, potentialX, potentialY);

                if(n == 2)
                {
                    isValid = false;
                }
                else if(n < 2)
                {
                    isValid = true;
                }
            }
            else if(y == 13)
            {
                n = Validate(isCurrent, x, y, 1, 0, 0, 1, n, potentialX, potentialY);

                if(n == 2)
                {
                    isValid = false;
                }
                else if(n < 2)
                {
                    isValid = true;
                }
            }
            else
            {
                n = Validate(isCurrent, x, y, 1, 1, 0, 1, n, potentialX, potentialY);

                if(n == 3)
                {
                    isValid = false;
                }
                else if(n < 3)
                {
                    isValid = true;
                }
            }
        }
        else if(x == 15)
        {
            if(y == 0)
            {
                n = Validate(isCurrent, x, y, 0, 1, 1, 0, n, potentialX, potentialY);

                if(n == 2)
                {
                    isValid = false;
                }
                else if(n < 2)
                {
                    isValid = true;
                }
            }
            else if(y == 13)
            {
                n = Validate(isCurrent, x, y, 0, 0, 1, 1, n, potentialX, potentialY);

                if(n == 2)
                {
                    isValid = false;
                }
                else if(n < 2)
                {
                    isValid = true;
                }
            }
            else
            {
                n = Validate(isCurrent, x, y, 0, 1, 1, 1, n, potentialX, potentialY);

                if(n == 3)
                {
                    isValid = false;
                }
                else if(n < 3)
                {
                    isValid = true;
                }
            }
        }
        else
        {
            if(y == 0)
            {
                n = Validate(isCurrent, x, y, 1, 1, 1, 0, n, potentialX, potentialY);

                if(n == 3)
                {
                    isValid = false;
                }
                else if(n < 3)
                {
                    isValid = true;
                }
            }
            else if(y == 13)
            {
                n = Validate(isCurrent, x, y, 1, 0, 1, 1, n, potentialX, potentialY);

                if(n == 3)
                {
                    isValid = false;
                }
                else if(n < 3)
                {
                    isValid = true;
                }
            }
            else
            {
                n = Validate(isCurrent ,x, y, 1, 1, 1, 1, n, potentialX, potentialY);

                if(n == 4)
                {
                    isValid = false;
                }
                else if(n < 4)
                {
                    isValid = true;
                }
            }
        }

        return isValid;
    }

    int Validate(bool isCurrent ,int x, int y, int plusX, int plusY, int minX, int minY, int n, int potentialX, int potentialY)
    {
        for(int i = 0; i < grid.Count; i++)
        {
            if(plusX == 1)
            {
                if(grid[i].GetComponent<Placement>().x == x + 1 && grid[i].GetComponent<Placement>().y == y)
                {
                    if(grid[i].GetComponent<Placement>().placed == true)
                    {
                        n += 1;
                    }
                    else if(!isCurrent)
                    {
                        if(x + 1 == potentialX && y == potentialY)
                        {
                            //Debug.Log("X + 1");
                            n += 1;
                        }
                    }
                }
            }

            if(minX == 1)
            {
                if(grid[i].GetComponent<Placement>().x == x - 1 && grid[i].GetComponent<Placement>().y == y)
                {
                    if(grid[i].GetComponent<Placement>().placed == true)
                    {
                        n += 1;

                        //Debug.Log("Min X: " + (x - 1));
                    }
                    else if(!isCurrent)
                    {
                        if(x - 1 == potentialX && y == potentialY)
                        {
                            n += 1;
                        }
                    }
                }
            }

            if(plusY == 1)
            {
                if(grid[i].GetComponent<Placement>().y == y + 1 && grid[i].GetComponent<Placement>().x == x)
                {
                    if(grid[i].GetComponent<Placement>().placed == true)
                    {
                        n += 1;

                        //Debug.Log("Plus Y: " + (y + 1));
                    }
                    else if(!isCurrent)
                    {
                        if(y + 1 == potentialY && x == potentialX)
                        {
                            n += 1;
                        }
                    }
                }
            }

            if(minY == 1)
            {
                if(grid[i].GetComponent<Placement>().y == y - 1 && grid[i].GetComponent<Placement>().x == x)
                {
                    if(grid[i].GetComponent<Placement>().placed == true)
                    {
                        n += 1;

                        //Debug.Log("Min Y: " + (y - 1));
                    }
                    else if(!isCurrent)
                    {
                        if(y - 1 == potentialY && x == potentialX)
                        {
                            n += 1;
                        }
                    }
                }
            }
        }

        //Debug.Log("N: " + n);
        return n;
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
    public List<GameObject> placedFloor;
    public GameObject sealer;
    public GameObject door;
    public bool bDoor, bSealer;
    public List<int> currentAmount;

    public float revenue;
    public float grossMargin;
    public float totalCost;
    public float grossProfit;
}
