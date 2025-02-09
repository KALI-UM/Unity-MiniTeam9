using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerInformation : UIElement
{
    [SerializeField]
    private SpriteRenderer towerIconRederer;

   [SerializeField]
    private LocalizationText towerNameText;

    [SerializeField]
    private LocalizationText towerGradeText;

    [SerializeField]
    private TextMeshProUGUI towerAttackPowerText;

    [SerializeField]
    private TextMeshProUGUI towerAttackSpeedText;


    public void UpdateTowerInformation(TowerData data)
    {
        towerNameText.OnStringIdChange(data.key);
        //towerGradeText.OnStringIdChange(data.attackType)
        towerAttackPowerText.text  = data.attackPower.ToString();
        towerAttackSpeedText.text  = data.attackSpeed.ToString();
    }

}
