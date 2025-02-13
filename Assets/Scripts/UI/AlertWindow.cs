using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveTable;

public class AlertWindow : PopWindow
{
    [SerializeField]
    private LocalizationText localizationText;
    private Sequence popAnimation;


    private void Start()
    {
        popAnimation = DOTween.Sequence().SetAutoKill(false);
        popAnimation.OnPlay(() => transform.localScale = new Vector3(1,0.5f,1));
        popAnimation.Append(transform.DOScale(Vector3.one, popDuration).SetEase(Ease.OutBack));
        //animation.Join(glowEffect.rectTransform.DOScale(new Vector3(0, 0, 0), popDuration).SetEase(Ease.InOutBounce));
    }

    public override void Open()
    {
        base.Open();
        popAnimation.Restart();
    }

    public void SetString(string key)
    {
        localizationText.OnStringIdChange(key);
    }
}
