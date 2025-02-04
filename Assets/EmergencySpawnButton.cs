using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EmergencySpawnButton : UIElement
{
    [SerializeField]
    private TextMeshProUGUI gemCostText;

    private Button spwanButton;

    public int spawnGrade;

    [SerializeField]
    private int gemCost;


    private void Awake()
    {
        spwanButton= GetComponent<Button>();
        gemCostText.text = gemCost.ToString();
        spwanButton.onClick.AddListener(() => OnClickSpawnGrade(spawnGrade, gemCost));
    }

    public void OnClickSpawnGrade(int grade, int gemCost)
    {
        if (!UIManager.GameManager.coinGemSystem.CanPayGem(gemCost))
        {
            return;
        }

        if (!UIManager.GameManager.SlotManager.IsEmptySlotExist())
        {
            return;
        }

        UIManager.GameManager.coinGemSystem.PayGem(gemCost);
        GameObject tower = UIManager.GameManager.TowerManager.GetRandomTower(grade);
        UIManager.GameManager.SlotManager.AddTower(tower.GetComponent<Tower>());
    }
}
