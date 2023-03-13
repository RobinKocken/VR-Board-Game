using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Grid : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject interactable;

    public int xCount;
    public int yCount;
    public float posDif;

    public List<GameObject> grid;

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
        CreateGrid();
        gameManager.InitializeGrid(grid);
    }

    void CreateGrid()
    {
        for(int x = 0; x < xCount; x++)
        {
            for(int y = 0; y < yCount; y++)
            {
                GameObject current = Instantiate(interactable, transform.position, Quaternion.identity, transform);
                current.transform.localPosition = new Vector3(current.transform.localPosition.x + posDif * x, current.transform.localPosition.y + posDif * y, transform.localPosition.z);

                current.GetComponent<Placement>().gameManager = gameManager;
                grid.Add(current);
            }
        }
    }
}
