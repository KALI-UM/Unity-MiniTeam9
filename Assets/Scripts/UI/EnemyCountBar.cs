using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCountBar : MonoBehaviour
{
    [SerializeField]
    private Slider countBar;
    [SerializeField]
    private TextMeshProUGUI countText;

    private string countFormat;

    private void Awake()
    {
        SetMaxValue(100);
    }

    public void SetMaxValue(int max)
    {
        countBar.maxValue = max;
        countFormat = "{0:D2}/" + max;
    }

    public void OnCountChanged(int value)
    {
        countBar.value = value;
        SetCountText(value);
    }

    private void SetCountText(int value)
    {
        countText.text = string.Format(countFormat, value);
    }
}
