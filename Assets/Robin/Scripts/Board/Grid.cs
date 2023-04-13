using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject interactable;

    public GameObject floorSpawner;
    public GameObject floorGridObject;

    public int xCount;
    public int yCount;
    public float posDif;
    public float posDifFloor;

    public List<GameObject> grid;
    public List<GameObject> floorGrid;

    public TMP_Text tRevenue;
    public TMP_Text tTotalCost;
    public TMP_Text tgrossProfit;
    public TMP_Text tgrossMargin;

    public TMP_Text[] amount;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()    
    {
        gameManager.InitializeTmp(tRevenue, tTotalCost, tgrossProfit, tgrossMargin, amount);
        gameManager.TmpVisualize(gameManager.layers[(int)gameManager.layer].revenue, gameManager.layers[(int)gameManager.layer].totalCost, gameManager.layers[(int)gameManager.layer].grossProfit, gameManager.layers[(int)gameManager.layer].grossMargin);
        CreateGrid();
        CreateFloorGrid();
        gameManager.InitializeGrid(grid, floorGrid);
    }

    void CreateGrid()
    {
        for(int x = 0; x < xCount; x++)
        {
            for(int y = 0; y < yCount; y++)
            {
                GameObject current = Instantiate(interactable, transform.position, Quaternion.identity, transform);
                current.transform.localPosition = new Vector3(current.transform.localPosition.x + posDif * x, current.transform.localPosition.y + posDif * y, 0);

                current.GetComponent<Placement>().gameManager = gameManager;
                current.GetComponent<Placement>().x = x;
                current.GetComponent<Placement>().y = y;
                grid.Add(current);
            }
        }
    }

    void CreateFloorGrid()
    {
        for(int x = 0; x < xCount; x++)
        {
            for(int y = 0; y < yCount; y++)
            {
                GameObject current = Instantiate(floorGridObject, floorSpawner.transform.position, floorGridObject.transform.rotation, floorSpawner.transform);
                current.transform.position = new Vector3(current.transform.position.x + posDifFloor * y, current.transform.position.y, current.transform.position.z + posDifFloor * x);

                current.GetComponent<Placement>().gameManager = gameManager;
                current.GetComponent<Placement>().x = x;
                current.GetComponent<Placement>().y = y;
                floorGrid.Add(current);
            }
        }
    }
}
