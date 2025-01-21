using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public string TowerId;
    private List<Tower> towers = new();

    private int columnIndex;
    private int rowIndex;

    public bool IsEmpty
    {
        get;
        private set;
    }

    public int maxSlotTowerCount = 3;
    public bool IsFull
    {
        get;
        private set;
    }

    private void Awake()
    {
        IsEmpty = true;
        IsFull = false;
    }

    public void SetIndex(int rowIndex, int colIndex)
    {
        this.columnIndex = colIndex;
        this.rowIndex = rowIndex;
    }

    public bool IsPossibleToAdd(string towerId)
    {
        if (IsEmpty)
            return true;

        return (TowerId.Equals(towerId) && !IsFull);
    }

    public void AddTower(Tower tower)
    {
        if (IsEmpty)
        {
            TowerId = tower.TowerId;
            IsEmpty = false;
        }

        towers.Add(tower);

        KALLogger.Log(tower.TowerId + $"»ðÀÔ slot({rowIndex},{columnIndex})", this);
        if (towers.Count >= maxSlotTowerCount)
        {
            IsFull = true;
        }
    }

    public void RemoveTower()
    {

    }
    public void RemoveAllTower()
    {

    }

}
