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
            if(gameManager.layers[(int)gameManager.layer].currentAmount[(int)gameManager.colour] > 0)
            {
                GameObject pallet = Instantiate(gameManager.selected, transform.position, transform.rotation);
                gameManager.SetCurrentAmount(-1);
                placeable = false;

                gameManager.CheckIfCalculate(pallet);
            }
        }
    }
}
