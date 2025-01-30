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

    public override void Initialize(UIManager mgr)
    {
       base.Initialize(mgr);

        uiManager.GameManager.coinGemSystem.onCoinCountChange += (int value) => OnChangeCoinValue(value);
        uiManager.GameManager.coinGemSystem.onGemCountChange += (int value) => OnChangeGemValue(value);
    }

    public void OnChangeCoinValue(int value)
    {
        coinText.text = value.ToString();
    }

    public void OnChangeGemValue(int value)
    {
        gemText.text = value.ToString();
    }
}
