using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallet : MonoBehaviour
{
    public GameManager.PalletColour pallet;
    public int x, y;

    public void Pos(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
