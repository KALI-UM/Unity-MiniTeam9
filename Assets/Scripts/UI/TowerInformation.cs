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
    private TextMeshProUGUI towerAttackPowerText;

    [SerializeField]
    private TextMeshProUGUI towerAttackSpeedText;


    public void UpdateTowerInformation(TowerData data)
    {
        towerImage.sprite = data.towerSprite;
        towerNameText.OnStringIdChange(data.key);
        //towerGradeText.OnStringIdChange(data.attackType)
        towerAttackPowerText.text  = data.attackPower.ToString();
        towerAttackSpeedText.text  = data.attackSpeed.ToString();
    }

}
