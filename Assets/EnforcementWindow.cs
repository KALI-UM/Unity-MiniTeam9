using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnforcementWindow : FocusWindow
{
    [SerializeField]
    private Button attackPowerButton;
    [SerializeField]
    private Button attackSpeedButton;

    //[SerializeField]
    //private LocalizationText localizationAttackPower;

    //[SerializeField]
    //private LocalizationText localizationAttackSpeed;

    private TowerManager towerManager;

    public override void Initialize(UIManager mgr)
    {
        base.Initialize(mgr);
        towerManager = UIManager.GameManager.TowerManager;
    }
    private void Awake()
    {
        attackPowerButton.onClick.AddListener(() => OnClickAttackPowerButton());
        attackSpeedButton.onClick.AddListener(() => OnClickAttackSpeedButton());
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

    }

    public void OnClickAttackSpeedButton()
    {
    }
}
