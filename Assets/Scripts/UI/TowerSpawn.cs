using DG.Tweening.Core.Easing;
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
        if (!uiManager.GameManager.coinGemSystem.CanPayCoin(spawnCost))
        {
            return;
        }

        if (!uiManager.GameManager.SlotManager.IsEmptySlotExist())
        {
            return;
        }

        uiManager.GameManager.coinGemSystem.PayCoin(spawnCost);
        GameObject tower = uiManager.GameManager.TowerManager.GetRandomTower(1);
        uiManager.GameManager.SlotManager.AddTower(tower.GetComponent<Tower>());

        spawnCost += 2;
        spawnCostText.text = spawnCost.ToString();
    }

}
