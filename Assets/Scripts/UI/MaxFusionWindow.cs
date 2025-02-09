using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TowerRecipeTable;

public class MaxFusionWindow : FocusWindow
{
    [SerializeField]
    private Button windowOpen;

    [SerializeField]
    private Button fusionButton;

    [ReadOnly]
    public RecipeProgressTracker currentRecipe;

    [SerializeField]
    private Image towerSprite;

    [SerializeField]
    private LocalizationText localizationTowerName;

    [SerializeField]
    private Slider percentBar;

    [SerializeField]
    private List<GameObject> towerIngredients;

    [SerializeField]
    private GameObject recipeScrollContent;

    [SerializeField]
    private UIElement recipeButtonPrefab;

    private List<RecipeButton> buttons = new();


    public override void Initialize(UIManager mgr)
    {
        base.Initialize(mgr);
        windowOpen.onClick.AddListener(() => UIManager.Open(FocusWindows.MaxLvFusion));
        fusionButton.onClick.AddListener(() => OnSpawnTower());
    }

    private void Awake()
    {
        foreach (var tracker in UIManager.GameManager.TowerManager.MaxFusionSystem.ProgressTrackers)
        {
            var button = Instantiate(recipeButtonPrefab);
            var recipeButton = button.GetComponent<RecipeButton>();
            buttons.Add(recipeButton);
            recipeButton.SetProgressTracker(tracker);
            recipeButton.button.onClick.AddListener(() => OnSelectRecipe(tracker));
            button.transform.SetParent(recipeScrollContent.transform);
        }
    }

    public override void Open()
    {
        base.Open();
        foreach (RecipeButton button in buttons)
        {
            button.UpdateRecipeButton();
        }

        OnSelectRecipe(buttons[0].ProgressTracker);
    }

    public override void OnOutFocus()
    {
        base.OnOutFocus();
        Close();
    }

    public void OnSelectRecipe(RecipeProgressTracker recipe)
    {
        currentRecipe = recipe;
        towerSprite.sprite = currentRecipe.TargetTowerSprite;
        localizationTowerName.OnStringIdChange(DataTableManager.TowerTable.Get(currentRecipe.Data.Id).Strnig_Key);
        percentBar.value = currentRecipe.ProgressValue;

        for (int i = 0; i < currentRecipe.Data.RecipeSum; i++)
        {
            towerIngredients[i].SetActive(true);
            towerIngredients[i].GetComponent<TowerIngredientIcon>().SetIcon(currentRecipe.ProgressList[i].exist, currentRecipe.IngredientSpriteList[i]);
        }

        for (int i = currentRecipe.Data.RecipeSum; i < towerIngredients.Count; i++)
        {
            towerIngredients[i].SetActive(false);
        }
    }

    public void OnSpawnTower()
    {
        if (!currentRecipe.CanFusion || UIManager.GameManager.TowerManager.IsMaxTowrCount || !UIManager.GameManager.SlotManager.IsEmptySlotExist())
        {
            return;
        }

        UIManager.GameManager.TowerManager.MaxFusionSystem.SpawnMaxLvRecipe(currentRecipe);
        Close();
    }

}
