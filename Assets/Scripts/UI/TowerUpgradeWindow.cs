using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TowerUpgradeTable;

public class TowerUpgradeWindow : FocusWindow
{
    [SerializeField]
    private Button windowOpen;

    [SerializeField]
    private Button attackPowerButton;
    [SerializeField]
    private TextMeshProUGUI attackPowerLvText;
    [SerializeField]
    private TextMeshProUGUI attackPowerGoldCostText;
    [SerializeField]
    private TextMeshProUGUI attackPowerGemCostText;
    [ReadOnly, SerializeField]
    private int attackPowerLv = 1;


    [SerializeField]
    private Button attackSpeedButton;
    [SerializeField]
    private TextMeshProUGUI attackSpeedLvText;
    [SerializeField]
    private TextMeshProUGUI attackSpeedGoldCostText;
    [SerializeField]
    private TextMeshProUGUI attackSpeedGemCostText;
    [ReadOnly, SerializeField]
    private int attackSpeedLv = 1;

    //[SerializeField]
    //private LocalizationText localizationAttackPower;

    //[SerializeField]
    //private LocalizationText localizationAttackSpeed;

    private TowerManager towerManager;
   
    private int maxUpgradeLv;


    private List<TowerUpgradeRawData> towerUpgradeRawDatas;

    public override void Initialize(UIManager mgr)
    {
        base.Initialize(mgr);
        towerManager = UIManager.GameManager.TowerManager;

        windowOpen.onClick.AddListener(() => UIManager.Open(FocusWindows.TowerUpgradeWindow));
    }
    private void Awake()
    {
        var dataTable = DataTableManager.Get<TowerUpgradeTable>(DataTableIds.TowerUpgrade);
        towerUpgradeRawDatas = dataTable.GetTowerUpgradeDatas();
        maxUpgradeLv = dataTable.MaxUpgradeLv;

        attackPowerButton.onClick.AddListener(() => OnClickAttackPowerButton());
        attackSpeedButton.onClick.AddListener(() => OnClickAttackSpeedButton());
    }

    private void Start()
    {
        UpdateAttackPowerText(attackPowerLv);
        UpdateAttackSpeedText(attackSpeedLv);
    }

    public override void OnOutFocus()
    {
        base.OnOutFocus();
        Close();
    }

    public void OnClickAttackPowerButton()
    {
        var goldGemSystem = uiManager.GameManager.GoldGemSystem;
        bool canPayGold = goldGemSystem.TryPayGold(towerUpgradeRawDatas[attackPowerLv + 1].GoldCost);
        bool canPayGem = goldGemSystem.TryPayGem(towerUpgradeRawDatas[attackPowerLv + 1].GemCost);
        if (canPayGold && canPayGem)
        {
            attackPowerLv++;

            uiManager.GameManager.TowerManager.SetAttackPowerUpgradeRate(towerUpgradeRawDatas[attackPowerLv].PowerBonus);
            UpdateAttackPowerText(attackPowerLv);

            SoundManager.Instance.PlaySFX("BattleEffect_08_GainGold");
        }
    }

    public void UpdateAttackPowerText(int lv)
    {
        attackPowerLvText.text = "LV" + lv.ToString();

        if (lv == maxUpgradeLv)
        {
            attackPowerButton.interactable = false;
            return;
        }

        int nextLv = lv + 1;
        if (towerUpgradeRawDatas[nextLv].GoldCost == 0)
        {
            attackPowerGoldCostText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            attackPowerGoldCostText.transform.parent.gameObject.SetActive(true);
            attackPowerGoldCostText.text = towerUpgradeRawDatas[nextLv].GoldCost.ToString();
        }

        if (towerUpgradeRawDatas[nextLv].GemCost == 0)
        {
            attackPowerGemCostText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            attackPowerGemCostText.transform.parent.gameObject.SetActive(true);
            attackPowerGemCostText.text = towerUpgradeRawDatas[nextLv].GemCost.ToString();
        }
    }

    public void OnClickAttackSpeedButton()
    {
        var goldGemSystem = uiManager.GameManager.GoldGemSystem;
        bool canPayGold = goldGemSystem.TryPayGold(towerUpgradeRawDatas[attackSpeedLv + 1].GoldCost);
        bool canPayGem = goldGemSystem.TryPayGem(towerUpgradeRawDatas[attackSpeedLv + 1].GemCost);
        if (canPayGold && canPayGem)
        {
            attackSpeedLv++;
            uiManager.GameManager.TowerManager.SetAttackSpeedUpgradeRate(towerUpgradeRawDatas[attackSpeedLv].PowerBonus);
            UpdateAttackSpeedText(attackSpeedLv);
            SoundManager.Instance.PlaySFX("BattleEffect_08_GainGold");
        }
    }

    public void UpdateAttackSpeedText(int lv)
    {
        attackSpeedLvText.text = "LV"+lv.ToString();

        if(lv== maxUpgradeLv)
        {
            attackSpeedButton.interactable = false;
            return;
        }
        

        int nextLv = lv + 1;
        if (towerUpgradeRawDatas[nextLv].GoldCost == 0)
        {
            attackSpeedGoldCostText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            attackSpeedGoldCostText.transform.parent.gameObject.SetActive(true);
            attackSpeedGoldCostText.text = towerUpgradeRawDatas[nextLv].GoldCost.ToString();
        }

        if (towerUpgradeRawDatas[nextLv].GemCost == 0)
        {
            attackSpeedGemCostText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            attackSpeedGemCostText.transform.parent.gameObject.SetActive(true);
            attackSpeedGemCostText.text = towerUpgradeRawDatas[nextLv].GemCost.ToString();
        }
    }
}
