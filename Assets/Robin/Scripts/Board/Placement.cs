using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject pallet;
    public bool placed;
    public int x, y;

    public void Place()
    {
        if(!placed)
            placed = gameManager.StartValidation(false, x, y);

        //Debug.Log(placed);

        if(placed && gameManager.selected != null)
        {
            if(gameManager.layers[(int)gameManager.layer].currentAmount[(int)gameManager.colour] > 0)
            {
                pallet = Instantiate(gameManager.selected, transform.GetChild(0).position, transform.rotation);
                pallet.GetComponent<Pallet>().Pos(x, y);

                gameManager.SetCurrentAmount(1);
                placed = true;

                gameManager.CheckIfCalculate(pallet);
                gameManager.FloorPlacment(x, y);
            }
        }
        else if(placed && gameManager.colour == GameManager.PalletColour.delete)
        {
            gameManager.Delete(pallet);
            gameManager.DeleteFloor(x, y);
            placed = false;
        }
    }
}
