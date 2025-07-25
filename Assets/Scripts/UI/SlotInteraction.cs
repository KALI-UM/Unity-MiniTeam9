using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotInteraction : FocusWindow
{
    [SerializeField]
    private
    GameObject towerInteraction;

    public Button sellButton;
    public Button fusionButton;

    [SerializeField]
    private TextMeshProUGUI costText;

    [SerializeField]
    private GameObject gold;

    [SerializeField]
    private GameObject gem;

    [SerializeField]
    private LocalizationText localizationTowerName;

    [SerializeField]
    private TowerInformation towerInformation;

    private TowerManager towerManager;
    private SlotManager slotManager;


    public override void Initialize(UIManager mgr)
    {
        base.Initialize(mgr);
        slotManager = UIManager.GameManager.SlotManager;
        towerManager = UIManager.GameManager.TowerManager;
    }


    private void Awake()
    {
        sellButton.onClick.AddListener(() => OnClickSell());
        fusionButton.onClick.AddListener(() => OnClickFusion());
    }

    public override void Open()
    {
        UpdateTowerInteractionPosition();
        sellButton.gameObject.SetActive(slotManager.SelectedSlot.TowerGroup.Data.grade != towerManager.MaxGrade);
        fusionButton.gameObject.SetActive(slotManager.SelectedSlot.TowerGroup.CanFusion);

        base.Open();
        towerInformation.gameObject.SetActive(true);
        towerInformation.UpdateTowerInformation(slotManager.SelectedSlot.TowerGroup.Data);

        var currData = slotManager.SelectedSlot.TowerGroup.Data;

        gold.SetActive(currData.saleGold != 0);
        gem.SetActive(currData.saleGem != 0);
        costText.text = Mathf.Max(currData.saleGold, currData.saleGem).ToString();

        StartCoroutine(CoInputThreshold());
    }

    public void UpdateTowerInteractionPosition()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(slotManager.SelectedSlot.transform.position);
        towerInteraction.transform.position = screenPosition;
    }

    public override void OnOutFocus()
    {
        base.OnOutFocus();
        Close();
    }

    public override void Close()
    {
        base.Close();
        towerInformation.gameObject.SetActive(false);
        slotManager.SelectedSlot.OnDeselected();
    }


    public void OnClickSell()
    {
        slotManager.GameManager.GoldGemSystem.AddGold(slotManager.SelectedSlot.TowerGroup.Data.saleGold);
        slotManager.GameManager.GoldGemSystem.AddGem(slotManager.SelectedSlot.TowerGroup.Data.saleGem);

        slotManager.SelectedSlot.RemoveTower();
        Close();
    }

    public void OnClickFusion()
    {
        slotManager.SelectedSlot.FusionTower();
        Close();
        SoundManager.Instance.PlaySFX("BattleEffect_06_Merge");
    }

    protected override void EnableInput()
    {
        sellButton.enabled = true;
        fusionButton.enabled = true;
    }

    protected override void DisableInput()
    {
        sellButton.enabled = false;
        fusionButton.enabled = false;
    }
}
