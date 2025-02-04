using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private Image barImage;

    [SerializeField]
    private TextMeshProUGUI hpText;

    private Action<int> onHpChange;

    private void Awake()
    {
        onHpChange = (int value) => SetColor();

        if (hpText!=null)
        {
            onHpChange += (int value) => hpText.text = value.ToString();
        }
    }

    public void OnHpChanged(int value, int max)
    {
        hpBar.value = (float)value/ max;
        onHpChange(value);
    }
    

    private void SetColor()
    {
        foreach(var colorValue in EnemyManager.HpColors)
        {
            if(hpBar.value>= colorValue.value)
            {
                barImage.color = colorValue.color;
                break;
            }
        }
    }
}
