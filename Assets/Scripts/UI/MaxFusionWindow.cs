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

    [ReadOnly, SerializeField]
    private RecipeProgressTracker currentRecipe;

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
        var towerTable = DataTableManager.TowerTable;
        var towerRecipeTable = DataTableManager.Get<TowerRecipeTable>(DataTableIds.TowerRecipe);

        List<eTower> maxTowers = towerTable.GetTowerGradeList(towerTable.MaxGrade);

        foreach (eTower id in maxTowers)
        {
            var button = Instantiate(recipeButtonPrefab);
            var recipeButton = button.GetComponent<RecipeButton>();
            buttons.Add(recipeButton);
            var tracker = new RecipeProgressTracker(uiManager.GameManager.TowerManager, towerRecipeTable.Get(id));
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
        localizationTowerName.OnStringIdChange(DataTableManager.TowerTable.Get(currentRecipe.Data.Id).Strnig_Key);
        percentBar.value = currentRecipe.ProgressValue;

        for (int i = 0; i < currentRecipe.Data.RecipeSum; i++)
        {
            towerIngredients[i].SetActive(true);
            towerIngredients[i].GetComponent<TowerIngredientIcon>().SetIcon(currentRecipe.ProgressList[i].exist);
        }

        for (int i = currentRecipe.Data.RecipeSum; i < towerIngredients.Count; i++)
        {
            towerIngredients[i].SetActive(false);
        }
    }

    public void OnSpawnTower()
    {
        if (!currentRecipe.CanFusion || !UIManager.GameManager.SlotManager.IsEmptySlotExist())
        {
            return;
        }

        foreach (var ingredient in currentRecipe.ProgressList)
        {
            UIManager.GameManager.SlotManager.FindSlot(ingredient.Id).RemoveTower();
        }

        GameObject tower = UIManager.GameManager.TowerManager.GetTower(currentRecipe.Data.Id);
        UIManager.GameManager.SlotManager.AddTower(tower.GetComponent<Tower>());
        Close();
    }

}
