using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject pallet;
    public GameObject invalidGrid;
    public float waitForSecs;
    public bool placed;
    public int x, y;
    bool invalidActive;

    void Start()
    {
        if(invalidGrid != null)
        {
            invalidGrid = Instantiate(invalidGrid, transform.position, transform.rotation);
            invalidGrid.SetActive(false);
        }
    }

    public void Place()
    {
        if(!placed)
            placed = gameManager.StartValidation(false, x, y);

        if(placed && gameManager.selected != null && pallet == null)
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
        else if(placed && gameManager.colour == GameManager.PalletColour.delete && pallet != null)
        {
            gameManager.Delete(pallet);
            gameManager.DeleteFloor(x, y);
            placed = false;
        }
        else if(!placed && !invalidActive)
        {
            invalidActive = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            invalidGrid.SetActive(true);
            invalidGrid.GetComponent<AudioSource>().Play();

            Invoke("Wait", waitForSecs);
        }
    }

    void Wait()
    {
        invalidGrid.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        invalidActive = false;
    }
}
