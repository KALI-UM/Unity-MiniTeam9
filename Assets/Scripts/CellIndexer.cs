using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellIndexer
{
    //static float CellSizeX = 1f;
    //static float CellSizeY = 1f;
    public static readonly float CellSize = 1f;

    private GameObject target;
    public CellIndexer(GameObject target)
    {
        this.target = target;
    }

    private int x;
    private int y;

    public Action cellIndexChanged;

    public int X
    {
        get
        {
            int prevx = x;
            x = Mathf.FloorToInt(target.transform.position.x / CellSize);
            if (prevx != x)
            {
                cellIndexChanged?.Invoke();
            }
            return x;
        }
    }

    public int Y
    {
        get
        {
            int prevy = y;
            y=Mathf.FloorToInt(target.transform.position.y / CellSize);
            if (prevy!=y)
            {
                cellIndexChanged?.Invoke();
            }
            return y;
        }
    }
}
