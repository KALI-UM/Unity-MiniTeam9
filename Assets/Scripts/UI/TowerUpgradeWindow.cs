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


    private int towerUpgradeLevel = 1;

    private List<TowerUpgradeRawData> towerUpgradeRawDatas;

    public override void Initialize(UIManager mgr)
    {
        base.Initialize(mgr);
        towerManager = UIManager.GameManager.TowerManager;

        windowOpen.onClick.AddListener(() => UIManager.Open(FocusWindows.TowerUpgrade));
    }
    private void Awake()
    {
        towerUpgradeRawDatas = DataTableManager.Get<TowerUpgradeTable>(DataTableIds.TowerUpgrade).GetTowerUpgradeDatas();


        attackPowerButton.onClick.AddListener(() => OnClickAttackPowerButton());
        attackSpeedButton.onClick.AddListener(() => OnClickAttackSpeedButton());
    }

    private void Start()
    {
        UpdateAttackPowerText(attackPowerLv);
        UpdateAttackSpeedText(attackSpeedLv);
    }

    public override void Open()
    {
        base.Open();
    }

    public override void OnOutFocus()
    {
        base.OnOutFocus();
        Close();
    }

    public void OnClickAttackPowerButton()
    {
        var goldGemSystem = uiManager.GameManager.coinGemSystem;
        bool canPayGold = goldGemSystem.CanPayGold(towerUpgradeRawDatas[attackPowerLv + 1].GoldCost);
        bool canPayGem = goldGemSystem.CanPayGem(towerUpgradeRawDatas[attackPowerLv + 1].GemCost);
        if (canPayGold && canPayGem)
        {
            attackPowerLv++;

            goldGemSystem.PayGold(towerUpgradeRawDatas[attackPowerLv].GoldCost);
            goldGemSystem.PayGem(towerUpgradeRawDatas[attackPowerLv].GemCost);

            uiManager.GameManager.TowerManager.SetAttackPowerUpgradeRate(towerUpgradeRawDatas[attackPowerLv].PowerBonus);
        }

        UpdateAttackPowerText(attackPowerLv);
    }

    public void UpdateAttackPowerText(int lv)
    {
        attackPowerLvText.text = lv.ToString();

        int nextLv = lv + 1;
        if (towerUpgradeRawDatas[nextLv].GoldCost == 0)
        {
            attackPowerGoldCostText.gameObject.SetActive(false);
        }
        else
        {
            attackPowerGoldCostText.gameObject.SetActive(true);
            attackPowerGoldCostText.text = towerUpgradeRawDatas[nextLv].GoldCost.ToString();
        }

        if (towerUpgradeRawDatas[nextLv].GemCost == 0)
        {
            attackPowerGemCostText.gameObject.SetActive(false);
        }
        else
        {
            attackPowerGemCostText.gameObject.SetActive(true);
            attackPowerGemCostText.text = towerUpgradeRawDatas[nextLv].GemCost.ToString();
        }
    }

    public void OnClickAttackSpeedButton()
    {
        var goldGemSystem = uiManager.GameManager.coinGemSystem;
        bool canPayGold = goldGemSystem.CanPayGold(towerUpgradeRawDatas[attackSpeedLv + 1].GoldCost);
        bool canPayGem = goldGemSystem.CanPayGem(towerUpgradeRawDatas[attackSpeedLv + 1].GemCost);
        if (canPayGold && canPayGem)
        {
            attackSpeedLv++;

            goldGemSystem.PayGold(towerUpgradeRawDatas[attackSpeedLv].GoldCost);
            goldGemSystem.PayGem(towerUpgradeRawDatas[attackSpeedLv].GemCost);

            uiManager.GameManager.TowerManager.SetAttackSpeedUpgradeRate(towerUpgradeRawDatas[attackSpeedLv].PowerBonus);
        }

        UpdateAttackSpeedText(attackSpeedLv);
    }

    public void UpdateAttackSpeedText(int lv)
    {
        attackSpeedLvText.text = lv.ToString();

        int nextLv = lv + 1;
        if (towerUpgradeRawDatas[nextLv].GoldCost == 0)
        {
            attackSpeedGoldCostText.gameObject.SetActive(false);
        }
        else
        {
            attackSpeedGoldCostText.gameObject.SetActive(true);
            attackSpeedGoldCostText.text = towerUpgradeRawDatas[nextLv].GoldCost.ToString();
        }

        if (towerUpgradeRawDatas[nextLv].GemCost == 0)
        {
            attackSpeedGemCostText.gameObject.SetActive(false);
        }
        else
        {
            attackSpeedGemCostText.gameObject.SetActive(true);
            attackSpeedGemCostText.text = towerUpgradeRawDatas[nextLv].GemCost.ToString();
        }
    }
}
