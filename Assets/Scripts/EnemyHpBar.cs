using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private Image barImage;

    public void OnHpChanged(float value)
    {
        hpBar.value = value;
        SetColor();
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
