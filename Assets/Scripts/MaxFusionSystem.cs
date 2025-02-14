using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaxFusionSystem
{
    private TowerManager towerManager;
    private MaxLvFusionAlert fusionAlertPop;

    public List<RecipeProgressTracker> ProgressTrackers
    {
        get;
        private set;
    } = new();

    public Action onUpdateRecipeProgress;

    public MaxFusionSystem(TowerManager towerManager)
    {
        this.towerManager = towerManager;
        var towerTable = DataTableManager.TowerTable;
        var towerRecipeTable = DataTableManager.Get<TowerRecipeTable>(DataTableIds.TowerRecipe);

        foreach (var data in towerRecipeTable.RecipeDatas)
        {
            var tracker = new RecipeProgressTracker(towerManager, towerRecipeTable.Get(data.Id));
            ProgressTrackers.Add(tracker);
        }

        fusionAlertPop = towerManager.GameManager.UIManager.popWindows[(int)PopWindows.MaxLvAlert] as MaxLvFusionAlert;
    }

    public void UpdateRecipeProgress()
    {
        foreach (var tracker in ProgressTrackers)
        {
            tracker.UpdateRecipeProgress();
        }

        onUpdateRecipeProgress?.Invoke();
    }

    public void SpawnMaxLvRecipe(RecipeProgressTracker target)
    {
        foreach (var ingredient in target.Data.Recipes)
        {
            for (int i = 0; i < ingredient.count; i++)
            {
                towerManager.GameManager.SlotManager.FindSlot(ingredient.Id).RemoveTower();
            }
        }

        GameObject tower = towerManager.GetTower(target.Data.Id);
        towerManager.GameManager.SlotManager.AddTower(tower.GetComponent<Tower>());

        
        fusionAlertPop.Open(target.TargetTowerSprite, DataTableManager.TowerTable.Get(target.Data.Id).String_Key);
        SoundManager.Instance.PlaySFX("BattleEffect_01_Call_Legend");
    }
}
