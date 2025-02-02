using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
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
        this.TowerGroup.towerManager = SlotManager.GameManager.TowerManager;
        this.TowerGroup.enemyManager= SlotManager.GameManager.EnemyManager;
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
        slotManager.UpdateTowerSort();
    }

    public void RemoveAllTower()
    {
        while (!TowerGroup.IsEmpty)
        {
            TowerGroup.RemoveTower();
        }
    }

    public void ChangeTowerGroup(TowerGroup towerGroup)
    {
        TowerGroup = towerGroup;
        TowerGroup.MoveTo(transform.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onSelected?.Invoke();
        KALLogger.Log("슬롯" + SlotIndex + "선택");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!TowerGroup.IsEmpty)
        {
            slotManager.OnBeginDragSlot(transform.position);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        KALLogger.Log("슬롯" + SlotIndex + "드래그->" + eventData.pointerEnter.name);
        if (!TowerGroup.IsEmpty)
        {
            var slot = eventData.pointerEnter.GetComponent<Slot>();
            if (slot != null)
            {
                slotManager.OnDragSlot(eventData.pointerEnter.transform.position);
            }
            else
            {
                eventData.dragging = false;
                slotManager.OnEndDragSlot();
            }
        }
    }

    //드래그 끝날시 호출
    public void OnEndDrag(PointerEventData eventData)
    {
        KALLogger.Log("슬롯" + SlotIndex + "드래그끝");

        var slot = eventData.pointerEnter.GetComponent<Slot>();
        if (slot != null)
        {
            TowerGroup temp = TowerGroup;

            ChangeTowerGroup(slot.TowerGroup);
            slot.ChangeTowerGroup(temp);
        }

        slotManager.OnEndDragSlot();
    }

}
