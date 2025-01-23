using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnTest : MonoBehaviour
{
    public GameManager gameManager;

    public void OnClickSpawnTower()
    {
        if (gameManager.SlotManager.IsEmptySlotExist())
        {
            GameObject tower = gameManager.TowerManager.GetRandomTower(1);
            gameManager.SlotManager.AddTower(tower.GetComponent<Tower>());
        }
    }
}
