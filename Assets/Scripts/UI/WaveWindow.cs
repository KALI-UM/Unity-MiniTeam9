using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static WaveTable;

public class WaveWindow : PopWindow
{
    public TextMeshProUGUI text;
    private readonly string waveFormat = "Wave {0}";


    public override void Open()
    {
        KALLogger.Log<WaveWindow>();
        base.Open();
    }

    public void OnWaveStart(WaveData data)
    {
        text.text = string.Format(waveFormat, data.waveNumber);
    }
}
