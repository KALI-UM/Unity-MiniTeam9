using DG.Tweening;
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

    private Sequence waveAnimation;


    public void Awake()
    {
        waveAnimation = DOTween.Sequence().SetAutoKill(false);
        waveAnimation.Append(transform.DOMoveX(transform.position.x-500f, popDuration));
    }

    public override void Open()
    {
        KALLogger.Log<WaveWindow>();
        base.Open();
        waveAnimation.Restart();
        SoundManager.Instance.PlayBGM("Bgm_battle01");
    }

    public void OnWaveStart(WaveData data)
    {
        localizationText.OnStringIdChange(data.waveTextFormat);
        localizationText.OnTextParamChange(data.waveNumber.ToString());
    }
}
