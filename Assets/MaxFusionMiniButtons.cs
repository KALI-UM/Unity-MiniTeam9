using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MaxFusionMiniButtons : UIElement
{
    [SerializeField]
    private GameObject maxFusionButtonPrefab;
    private List<GameObject> buttons = new();

    [SerializeField]
    private int maxButtonCount = 3;


    public override void Initialize(UIManager mgr)
    {
        base.Initialize(mgr);

        UIManager.GameManager.TowerManager.MaxFusionSystem.onUpdateRecipeProgress += () => UpdateCanFusionButtons();

        foreach (var tracker in UIManager.GameManager.TowerManager.MaxFusionSystem.ProgressTrackers)
        {
            var maxFusionButton = Instantiate(maxFusionButtonPrefab);
            maxFusionButton.transform.SetParent(gameObject.transform);
            maxFusionButton.SetActive(false);

            var button = maxFusionButton.GetComponent<UnityEngine.UI.Button>();
            buttons.Add(maxFusionButton);
            button.onClick.AddListener(() => UIManager.GameManager.TowerManager.MaxFusionSystem.SpawnMaxLvRecipe(tracker));

            maxFusionButton.GetComponent<MaxFuisionMiniButton>().targetSprite.sprite = tracker.TargetTowerSprite;
        }
    }

    public void UpdateCanFusionButtons()
    {
        int count = 0;
        for (int i = 0; i < UIManager.GameManager.TowerManager.MaxFusionSystem.ProgressTrackers.Count; i++)
        {
            var tracker = UIManager.GameManager.TowerManager.MaxFusionSystem.ProgressTrackers[i];
            if (tracker.CanFusion && count < maxButtonCount)
            {
                buttons[i].gameObject.SetActive(true);
                count++;
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
}
