using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSelect : MonoBehaviour
{
    public GameManager gamaManager;
    public GameManager.PalletColour pallet;

    public void Select()
    {
        gamaManager.SelectColour(pallet);
    }
}
