using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinGemBar : UIElement
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI towerText;

    private string towerCountFormat;

    public override void Initialize(UIManager mgr)
    {
       base.Initialize(mgr);

        UIManager.GameManager.goldGemSystem.onGoldCountChange += (int value) => OnChangeCoinValue(value);
        UIManager.GameManager.goldGemSystem.onGemCountChange += (int value) => OnChangeGemValue(value);

        towerCountFormat = "{0}/" + UIManager.GameManager.TowerManager.MaxTowerCount;
        UIManager.GameManager.TowerManager.onTowerCountChange += (int value) => OnChangeTowerCountValue(value);
    }

    public void OnChangeCoinValue(int value)
    {
        coinText.text = value.ToString();
    }

    public void OnChangeGemValue(int value)
    {
        gemText.text = value.ToString();
    }

    public void OnChangeTowerCountValue(int value)
    {
        towerText.text =string.Format(towerCountFormat, value);
    }
}
