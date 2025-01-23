using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveWindow : PopWindow
{
    public TextMeshProUGUI text;
    private readonly string waveFormat = "Wave {0}";


    public void WaveUpdate(int wave)
    {
        text.text=string.Format(waveFormat, wave);
    }

    public override void Open()
    {
        WaveUpdate(windowManager.gameManager.CurrentWave);
        base.Open();
    }
}
