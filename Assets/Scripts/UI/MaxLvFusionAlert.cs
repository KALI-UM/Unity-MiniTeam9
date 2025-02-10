
using UnityEngine;
using UnityEngine.UI;

public class MaxLvFusionAlert : PopWindow
{
    [SerializeField]
    private LocalizationText localizationName;

    [SerializeField]
    private Image targetImage;

    public void UpdateTowerName(Sprite sprite, string key)
    {
        targetImage.sprite = sprite;
        localizationName.OnStringIdChange(key);
    }

    public void Open(Sprite sprite, string key)
    {
        Open();
        UpdateTowerName(sprite, key);
    }
}
