using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MaxLvFusionAlert : PopWindow
{
    [SerializeField]
    private LocalizationText localizationName;

    [SerializeField]
    private Image targetImage;

    [SerializeField]
    private Image glowEffect;

   // private Sequence popAnimation;

    //private void Start()
    //{
    //    popAnimation = DOTween.Sequence().SetAutoKill(false);
    //    popAnimation.Append(glowEffect.rectTransform.DORotate(new Vector3(0, 0, 1500f), popDuration));
    //    //animation.Join(glowEffect.rectTransform.DOScale(new Vector3(0, 0, 0), popDuration).SetEase(Ease.InOutBounce));
    //}

    public void UpdateTowerName(Sprite sprite, string key)
    {
        targetImage.sprite = sprite;
        localizationName.OnStringIdChange(key);
    }

    public void Open(Sprite sprite, string key)
    {
        Open();
        UpdateTowerName(sprite, key);
        //popAnimation.Restart();
    }

    public override void Close()
    {
        base.Close();
        //popAnimation.Pause();
    }

    private void Update()
    {
        glowEffect.transform.Rotate(new Vector3(0, 0, 50f * Time.deltaTime));
    }
}
