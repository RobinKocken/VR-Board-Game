using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public GameManager gameManager;
    public bool placeable = true;

    public void Place()
    {
        if(placeable && gameManager.selected != null)
        {
            if(gameManager.product[(int)gameManager.colour].amount > 0)
            {
                Instantiate(gameManager.selected, transform.position, transform.rotation);
                gameManager.product[(int)gameManager.colour].amount -= 1;
                placeable = false;

                gameManager.CheckIfCalculate();
            }
        }
    }
}
