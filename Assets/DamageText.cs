using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(TextMeshPro))]
public class DamageText : Effect
{
    static readonly private string defaultFormat = "{0:N0}";
    static readonly private string kFormat = "{0:N1}K";
    static readonly private string mFormat = "{0:N2}M";

    [SerializeField]
    private TextMeshPro textMesh;

    [SerializeField]
    private  List<float> fontSize;
    [ReadOnly]
    public UnityEngine.Color color;

    private void Awake()
    {
        color = textMesh.color;
        var startColor = textMesh.color;
        color.a = 1;
        startColor.a = 0;
        textMesh.color = startColor;
    }

    static public string IntToDamageText(int value)
    {
        string damageText;
        switch (value)
        {
            case > 10000:
                float m = value / 10000;
                damageText = string.Format(mFormat, m);
                
                break;

            case > 1000:
                float k = value / 1000;
                damageText = string.Format(kFormat, k);
                break;

            default:
                damageText = string.Format(defaultFormat, value);
                break;

        }

        return damageText;
    }

    public override void Play(Vector3 position)
    {
        base.Play(position);
        textMesh.color = color;
        textMesh.DOFade(0, duration).OnComplete(() => Release());
    }

    public void SetDamageText(int value)
    {
        textMesh.text = IntToDamageText(value);
    }
}
