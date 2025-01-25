using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Slot : MonoBehaviour
{
    private EnemyManager enemyManager;

    private List<Tower> towers = new();

    private int columnIndex;
    private int rowIndex;

    public TowerGroup TowerGroup
    {
        get; 
        private set;
    }

    public void Initialize(TowerGroup group)
    {
        this.TowerGroup = group;
        group.transform.position=transform.position;
    }

    public void SetIndex(int rowIndex, int colIndex)
    {
        this.columnIndex = colIndex;
        this.rowIndex = rowIndex;
    }

    public bool IsPossibleToAdd(eTower towerId)
    {
        if (TowerGroup.IsEmpty)
        {
            return true;
        }

        return (!TowerGroup.IsFull)&&(TowerGroup.TowerId == towerId);
    }

    public void AddTower(Tower tower)
    {
        TowerGroup.AddTower(tower);
    }

    public void RemoveTower()
    {

    }

    public void RemoveAllTower()
    {

    }
}
