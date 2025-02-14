using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerInformation : UIElement
{
    [SerializeField]
    private Image towerImage;

    [SerializeField]
    private LocalizationText towerNameText;

    [SerializeField]
    private LocalizationText towerGradeText;

    [SerializeField]
    private LocalizationText towerTypeText;

    [SerializeField]
    private TextMeshProUGUI towerAttackPowerText;

    [SerializeField]
    private TextMeshProUGUI towerAttackPowerUpgradeText;

    [SerializeField]
    private TextMeshProUGUI towerAttackSpeedText;

    [SerializeField]
    private TextMeshProUGUI towerAttackSpeedUpgradeText;

    public void UpdateTowerInformation(TowerData data)
    {
        towerImage.sprite = data.towerSprite;
        towerNameText.OnStringIdChange(data.key);
        towerGradeText.OnStringIdChange("Tower_Grade_" + data.grade.ToString());
        towerTypeText.OnStringIdChange("Tower_AttackType_" + data.attackType.ToString());
        towerAttackPowerText.text = data.attackPower.ToString();
        towerAttackSpeedText.text = data.attackSpeed.ToString();

        if (UIManager.TowerManager.AttackPowerUpgradeRate == 0)
        {
            towerAttackPowerUpgradeText.gameObject.SetActive(false);
        }
        else
        {
            towerAttackPowerUpgradeText.gameObject.SetActive(true);
            towerAttackPowerUpgradeText.text = "+ " + (UIManager.TowerManager.AttackPowerUpgradeRate * data.attackPower).ToString();
        }

        if (UIManager.TowerManager.AttackSpeedUpgradeRate == 0)
        {
            towerAttackSpeedUpgradeText.gameObject.SetActive(false);
        }
        else
        {
            towerAttackSpeedUpgradeText.gameObject.SetActive(true);
            towerAttackSpeedUpgradeText.text = "+ " + (UIManager.TowerManager.AttackSpeedUpgradeRate * data.attackSpeed).ToString();
        }
    }
}
