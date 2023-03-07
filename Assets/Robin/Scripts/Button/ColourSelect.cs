using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSelect : MonoBehaviour
{
    public GameManager gameManager;
    public GameManager.PalletColour pallet;

    public void Select()
    {
        gameManager.SelectColour(pallet);
    }
}
