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
        if (UIManager.GameManager.TowerManager.IsMaxTowrCount||!UIManager.GameManager.SlotManager.IsEmptySlotExist())
        {
            return;
        }

        if (!UIManager.GameManager.GoldGemSystem.TryPayGold(spawnCost))
        {
            return;
        }


        GameObject tower = UIManager.GameManager.TowerManager.GetRandomTower(1);
        UIManager.GameManager.SlotManager.AddTower(tower.GetComponent<Tower>());

        spawnCost += 2;
        spawnCostText.text = spawnCost.ToString();

        SoundManager.Instance.PlaySFX("BattleEffect_01_Call");
    }

    public void OnClickSpawn(eTower Id)
    {
        if (UIManager.GameManager.TowerManager.IsMaxTowrCount || !UIManager.GameManager.SlotManager.IsEmptySlotExist())
        {
            return;
        }

        if (!UIManager.GameManager.GoldGemSystem.TryPayGold(spawnCost))
        {
            return;
        }

        GameObject tower = UIManager.GameManager.TowerManager.GetTower(Id);
        UIManager.GameManager.SlotManager.AddTower(tower.GetComponent<Tower>());

        spawnCost += 2;
        spawnCostText.text = spawnCost.ToString();
    }

}
