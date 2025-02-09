using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
[ExecuteAlways]
public class PercentBar : MonoBehaviour
{
    private Slider bar;
    [SerializeField]
    private TextMeshProUGUI percentText;

    private readonly string percentFormat = "{0}%";

    private void Awake()
    {
        bar = GetComponent<Slider>();
        bar.onValueChanged.AddListener((float value)=>OnValueChange(value));
    }
    
    public void OnValueChange(float value)
    {
        percentText.text = string.Format(percentFormat, ((int)(Mathf.Clamp((value* 100f), 0, 100))).ToString());
    }
}
