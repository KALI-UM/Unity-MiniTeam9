using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static WaveTable;

public class WaveWindow : PopWindow
{
    [SerializeField]
    private LocalizationText localizationText;


    public override void Open()
    {
        KALLogger.Log<WaveWindow>();
        base.Open();
    }

    public void OnWaveStart(WaveData data)
    {
        localizationText.OnStringIdChange(data.waveTextFormat);
        localizationText.OnTextParamChange(data.waveNumber.ToString());
    }
}
