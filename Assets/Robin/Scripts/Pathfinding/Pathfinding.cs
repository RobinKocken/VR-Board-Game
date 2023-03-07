using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;

    public Transform startPos;
    public Transform targetPos;

    void Awake()
    {
        grid = GetComponent<Grid>();        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
