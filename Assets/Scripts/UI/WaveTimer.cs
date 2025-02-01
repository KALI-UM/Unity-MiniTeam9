using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static WaveTable;

public class WaveTimer : UIElement
{
    [SerializeField]
    private  TextMeshProUGUI timeText;
    [SerializeField]
    private  TextMeshProUGUI waveText;

    private string timeFormat = "{0:D2}:{1:D2}";
    private string waveFormat = "WAVE {0:D2}";

    private float waveTime;
    
    // Update is called once per frame
    void Update()
    {
        int tempTime = Mathf.CeilToInt(waveTime);
        timeText.text = string.Format(timeFormat, tempTime / 60, tempTime % 60);
        waveTime -= Time.deltaTime;
    }

    public void OnWaveStart(WaveData data)
    {
        waveText.text = string.Format(waveFormat, data.waveNumber);
        waveTime = data.waveDuration;
    }
}
