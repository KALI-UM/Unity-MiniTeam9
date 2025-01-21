using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnTest : MonoBehaviour
{
    public SlotManager slotManager;
    public TowerManager towerManager;

    public void OnClickSpawnTower()
    {
        if (slotManager.IsEmptySlotExist())
        {
            GameObject tower = towerManager.GetTower();
            slotManager.AddTower(tower.GetComponent<Tower>());
        }
    }
}
