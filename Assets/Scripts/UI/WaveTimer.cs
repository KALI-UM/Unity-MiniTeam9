using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
    [SerializeField]
    private  TextMeshProUGUI timeText;
    [SerializeField]
    private  TextMeshProUGUI waveText;

    private string timeFormat = "mm:ss";
    private string waveFormat = "WAVE {0:D2}";


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = string.Format(timeFormat, Time.time);
    }

    private void OnWaveStarted(int waveValue)
    {
        waveText.text = string.Format(waveFormat, waveValue);
    }
}
