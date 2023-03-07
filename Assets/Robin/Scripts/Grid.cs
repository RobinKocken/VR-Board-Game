using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject interactable;

    public int xCount;
    public int yCount;
    public float posDif;


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        CreateGrid();
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
            }
        }
    }
}
