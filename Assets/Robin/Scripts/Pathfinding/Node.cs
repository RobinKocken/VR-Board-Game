using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;
    public int gridY;

    public bool isWall;

    public Vector3 vPosition;

    public Node parentNode;

    public int gCost;
    public int hCost;

    public int FCost { get { return gCost = hCost; } }

    public Node(bool nIsWall, Vector3 nPos, int nGridX, int nGridY)
    {
        isWall = nIsWall;
        vPosition = nPos;
        gridX = nGridX;
        gridY = nGridY;
    }
}
