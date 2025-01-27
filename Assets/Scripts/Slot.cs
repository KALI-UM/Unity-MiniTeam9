using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
{
    protected SlotManager slotManager;
    public SlotManager SlotManager
    {
        get => slotManager;
    }

    private List<Tower> towers = new();


    public int SlotIndex
    {
        get;
        private set;
    }

    public Action onSelected;


    public TowerGroup TowerGroup
    {
        get;
        private set;
    }

    public void Initialize(SlotManager manager, TowerGroup group, int index)
    {
        slotManager = manager;
        this.TowerGroup = group;
        group.transform.position = transform.position;

        SlotIndex = index;
    }

    public bool IsPossibleToAdd(eTower towerId)
    {
        if (TowerGroup.IsEmpty)
        {
            return true;
        }

        return (!TowerGroup.IsFull) && (TowerGroup.TowerId == towerId);
    }

    public void AddTower(Tower tower)
    {
        TowerGroup.AddTower(tower);
    }

    public void FusionTower()
    {
        GameObject newTower = SlotManager.GameManager.TowerManager.GetRandomTower(TowerGroup.Data.grade + 1);
        RemoveAllTower();
        AddTower(newTower.GetComponent<Tower>());
    }

    public void RemoveTower()
    {
        TowerGroup.RemoveTower();
    }

    public void RemoveAllTower()
    {
        while (!TowerGroup.IsEmpty)
        {
            TowerGroup.RemoveTower();
        }
    }

    //드래그 시작시 호출
    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    //드래그 끝날시 호출
    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onSelected?.Invoke();
        KALLogger.Log("슬롯" + SlotIndex + "선택");

        eventData.Use();
    }
}
