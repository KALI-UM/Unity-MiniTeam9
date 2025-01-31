using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInteraction : FocusWindow
{
    [SerializeField]
    private Button sellButton;
    [SerializeField]
    private Button fusionButton;

    private SlotManager slotManager;


    public override void Initialize(UIManager mgr)
    {
        base.Initialize(mgr);
        slotManager = uiManager.GameManager.SlotManager;
    }

    private void Awake()
    {
        sellButton.onClick.AddListener(() => OnClickSell());
        fusionButton.onClick.AddListener(()=> OnClickFusion());
    }

    public override void Open()
    {
        UpdateWindowPosition();
        fusionButton.interactable = slotManager.SelectedSlot.TowerGroup.CanFusion;
        base.Open();
    }

    public void UpdateWindowPosition()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(slotManager.SelectedSlot.transform.position);
        transform.position = screenPosition;
    }

    public override void OnOutFocus()
    {
        base.OnOutFocus();
        Close();
    }

    public void OnClickSell()
    {
        slotManager.GameManager.coinGemSystem.AddCoin(slotManager.SelectedSlot.TowerGroup.Data.saleGold);
        slotManager.GameManager.coinGemSystem.AddGem(slotManager.SelectedSlot.TowerGroup.Data.saleGem);

        slotManager.SelectedSlot.RemoveTower();
        Close();
    }

    public void OnClickFusion()
    {
        slotManager.SelectedSlot.FusionTower();
        Close();
    }
}
