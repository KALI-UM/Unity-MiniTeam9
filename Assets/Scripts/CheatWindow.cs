using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatWindow : MonoBehaviour
{
    [SerializeField]
    private Button reset;

    [SerializeField]
    private Slider timeScale;



    private void Awake()
    {
        timeScale.minValue = 0f;
        timeScale.maxValue = 5f;

        OnReset();

        reset.onClick.AddListener(OnReset);
        timeScale.onValueChanged.AddListener(OnChangeTimeScale);
    }

    public void OnReset()
    {
        timeScale.value = 1f;
    }

    public void OnChangeTimeScale(float value)
    {
        Time.timeScale = value;
    }


}
