using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TowerRecipeTable;

public class RecipeButton : UIElement
{

    public Button button;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Slider percentBar;

    public RecipeProgressTracker ProgressTracker
    {
        get; 
        private set;
    }

    private RecipeProgressTracker progressTracker;

    public void SetProgressTracker(RecipeProgressTracker tracker)
    {
        ProgressTracker = tracker;
        icon.sprite= ProgressTracker.TargetTowerSprite;
    }

    public void UpdateRecipeButton()
    {
        percentBar.value = ProgressTracker.ProgressValue;
    }
}
