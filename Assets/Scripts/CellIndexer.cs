using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellIndexer
{
    static float CellSizeX = 1f;
    static float CellSizeY = 1f;

    private GameObject target;
    public CellIndexer(GameObject target)
    {
        this.target = target;
    }

    public int X
    {
        get => Mathf.FloorToInt(target.transform.position.x / CellSizeX);
    }

    public int Y
    {
        get => Mathf.FloorToInt(target.transform.position.y / CellSizeY);
    }
}
