using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInteraction : FocusWindow
{
    [SerializeField]
    private
    GameObject towerInteraction;

    [SerializeField]
    private Button sellButton;
    [SerializeField]
    private Button fusionButton;

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
        fusionButton.onClick.AddListener(()=> OnClickFusion());
    }

    public override void Open()
    {
        UpdateTowerInteractionPosition();
        fusionButton.interactable = slotManager.SelectedSlot.TowerGroup.CanFusion;
        base.Open();
        towerInformation.UpdateTowerInformation(slotManager.SelectedSlot.TowerGroup.Data);
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
        slotManager.SelectedSlot.OnDeselected();
    }


    public void OnClickSell()
    {
        slotManager.GameManager.goldGemSystem.AddGold(slotManager.SelectedSlot.TowerGroup.Data.saleGold);
        slotManager.GameManager.goldGemSystem.AddGem(slotManager.SelectedSlot.TowerGroup.Data.saleGem);

        slotManager.SelectedSlot.RemoveTower();
        Close();
    }

    public void OnClickFusion()
    {
        slotManager.SelectedSlot.FusionTower();
        Close();
    }

    protected override void EnableInput()
    {
        sellButton.enabled = true;
        fusionButton.enabled = true;
    }

    protected override void DisableInput()
    {
        sellButton.enabled = false;
        fusionButton.enabled=false;
    }
}
