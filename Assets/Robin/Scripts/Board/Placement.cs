using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject pallet;
    public bool placeable = true;
    public int x, y;

    public void Place()
    {
        if(placeable && gameManager.selected != null)
        {
            if(gameManager.layers[(int)gameManager.layer].currentAmount[(int)gameManager.colour] > 0)
            {
                pallet = Instantiate(gameManager.selected, transform.GetChild(0).position, transform.rotation);
                gameManager.SetCurrentAmount(1);
                placeable = false;

                gameManager.CheckIfCalculate(pallet);
            }
        }
        else if(!placeable && gameManager.colour == GameManager.PalletColour.delete)
        {
            gameManager.Delete(pallet);
            placeable = true;
        }
    }
}
