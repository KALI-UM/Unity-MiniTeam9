using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TowerRecipeTable;

public class RecipeProgressTracker
{
    private TowerManager towerManager;


    public RecipeProgressTracker(TowerManager mgr, TowerRecipeData data)
    {
        towerManager = mgr;
        Data = data;
        foreach (var recipe in Data.Recipes)
        {
            for (int i = 0; i < recipe.count; i++)
            {
                ProgressList.Add((recipe.Id, false));
            }
        }
    }


    public TowerRecipeData Data
    {
        get;
        private set;
    }

    public float ProgressValue
    {
        get;
        private set;
    }

    public List<(eTower Id, bool exist)> ProgressList
    {
        get;
        private set;
    } = new();

    public bool CanFusion
    {
        get => ProgressValue >= 1f;
    }

    public void UpdateRecipeProgress()
    {
        int sum = 0;
        int index = 0;
        foreach (var recipe in Data.Recipes)
        {
            int count = Mathf.Clamp(towerManager.GetTowerCount(recipe.Id), 0, recipe.count);
            sum += count;
            for (int i = 1; i <= recipe.count; i++, index++)
            {
                bool isExist = (i <= count);
                ProgressList[index] = (ProgressList[index].Id, isExist);
            }
        }
        ProgressValue = (float)sum / Data.RecipeSum;
    }
}
