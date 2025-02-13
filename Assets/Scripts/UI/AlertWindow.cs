using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveTable;

public class AlertWindow : PopWindow
{
    [SerializeField]
    private LocalizationText localizationText;

    public override void Open()
    {
        base.Open();
    }

    public void SetString(string key)
    {
        localizationText.OnStringIdChange(key);
    }
}
