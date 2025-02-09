using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawn : UIElement
{
    public Button spawnButton;
    public TextMeshProUGUI spawnCostText;

    public int initialSpawnCost;
    [ReadOnly]
    private int spawnCost;


    private void Awake()
    {
        spawnButton.onClick.AddListener(() => OnClickSpawn());
    }

    public override void Initialize(UIManager mgr)
    {
        base.Initialize(mgr);
        spawnCost = initialSpawnCost;

        spawnCostText.text = spawnCost.ToString();
    }

    public void OnClickSpawn()
    {
        if (!UIManager.GameManager.goldGemSystem.CanPayGold(spawnCost))
        {
            return;
        }

        if (UIManager.GameManager.TowerManager.IsMaxTowrCount||!UIManager.GameManager.SlotManager.IsEmptySlotExist())
        {
            return;
        }

        UIManager.GameManager.goldGemSystem.PayGold(spawnCost);
        GameObject tower = UIManager.GameManager.TowerManager.GetRandomTower(1);
        UIManager.GameManager.SlotManager.AddTower(tower.GetComponent<Tower>());

        spawnCost += 2;
        spawnCostText.text = spawnCost.ToString();
    }

    public void OnClickSpawn(eTower Id)
    {
        if (!UIManager.GameManager.goldGemSystem.CanPayGold(spawnCost))
        {
            return;
        }

        if (UIManager.GameManager.TowerManager.IsMaxTowrCount || !UIManager.GameManager.SlotManager.IsEmptySlotExist())
        {
            return;
        }

        UIManager.GameManager.goldGemSystem.PayGold(spawnCost);
        GameObject tower = UIManager.GameManager.TowerManager.GetTower(Id);
        UIManager.GameManager.SlotManager.AddTower(tower.GetComponent<Tower>());

        spawnCost += 2;
        spawnCostText.text = spawnCost.ToString();
    }

}
