using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnTest : MonoBehaviour
{
    public GameManager gameManager;

    public void OnClickSpawnTower()
    {
        if (gameManager.slotManager.IsEmptySlotExist())
        {
            GameObject tower = gameManager.towerManager.GetTower();
            gameManager.slotManager.AddTower(tower.GetComponent<Tower>());
        }
    }
}
