using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SlotManager : InGameManager
{
    [SerializeField]
    private GameObject towerGroupPrefab;

    [SerializeField]
    private List<Slot> slots = new();


    [Serializable]
    public struct TowerPosition
    {
        public int count;
        public Transform[] positions;
    }

    [SerializeField]
    private List<TowerPosition> towerPositions = new();
    static public List<TowerPosition> TowerPositions
    {
        get;
        private set;
    }

    [ReadOnly, SerializeField]
    private int selectedSlotIndex;
    public Slot SelectedSlot
    {
        get => slots[selectedSlotIndex];
    }

    [SerializeField]
    private LineRenderer lineRenderer;

    public bool IsSlotDragging
    {
        get;
        private set;
    }

    private void Awake()
    {
        TowerPositions = towerPositions;

        //slots = slots.OrderBy(s => transform.position.x).ThenByDescending(s => s.transform.position.y).ToList();
        for (int i = 0; i < slots.Count; i++)
        {
            TowerGroup group = Instantiate(towerGroupPrefab).GetComponent<TowerGroup>();
            var currSlot = slots[i];
            currSlot.Initialize(this, group, i);
            currSlot.onSelected += () =>
            {
                selectedSlotIndex = currSlot.SlotIndex;
                if (!currSlot.TowerGroup.IsEmpty)
                {
                    GameManager.UIManager.Open(FocusWindows.SlotInteraction);
                }
                else
                {
                    GameManager.UIManager.Close(FocusWindows.SlotInteraction);
                }
            };
            group.transform.SetParent(gameObject.transform);
        }
    }

    public bool IsEmptySlotExist()
    {
        foreach (var slot in slots)
        {
            if (slot.TowerGroup.IsEmpty)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsPossibleToSpawnTower()
    {
        return (GameManager.TowerManager.TowerCount <= GameManager.TowerManager.MaxTowerCount && IsEmptySlotExist());
    }


    public void AddTower(Tower tower, int index)
    {
        //ÁßÃ¸ÇÒ ¼ö ÀÖ´Â ½½·ÔÀÌ ÀÖ´Ù¸é ÁßÃ¸
        foreach (var slot in slots)
        {
            if (slot.IsNotEmptyAndPossibleToAdd(tower.TowerId))
            {
                slot.AddTower(tower);
                return;
            }
        }

        //¾ø´Ù¸é index ½½·Ô¿¡ »ðÀÔÇÑ´Ù
        slots[index].AddTower(tower);
    }

    public void AddTower(Tower tower)
    {
        //ÁßÃ¸ÇÒ ¼ö ÀÖ´Â ½½·ÔÀÌ ÀÖ´Ù¸é ÁßÃ¸
        foreach (var slot in slots)
        {
            if (slot.IsNotEmptyAndPossibleToAdd(tower.TowerId))
            {
                slot.AddTower(tower);
                return;
            }
        }

        //¾ø´Ù¸é ºñ¾îÀÖ´Â ½½·Ô¿¡ »ðÀÔÇÑ´Ù
        foreach (var slot in slots)
        {
            if (slot.TowerGroup.IsEmpty)
            {
                slot.AddTower(tower);
                return;
            }
        }
    }

    public void UpdateTowerSort()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];

            if (slot.TowerGroup.IsEmpty || slot.TowerGroup.IsFull)
                continue;

            for (int j = i + 1; j < slots.Count; j++)
            {
                var otherSlot = slots[j];
                if (otherSlot.TowerGroup.IsEmpty || otherSlot.TowerGroup.IsFull)
                    continue;

                if (slot.TowerGroup.TowerId == otherSlot.TowerGroup.TowerId)
                {
                    while (!otherSlot.TowerGroup.IsEmpty)
                    {
                        var t = otherSlot.TowerGroup.SendToNewTowerGroup();
                        slot.TowerGroup.ReceiveTower(t);

                        if (slot.TowerGroup.IsFull)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

    public Slot FindSlot(eTower id)
    {
        foreach (Slot slot in slots)
        {
            if (!slot.TowerGroup.IsEmpty&&slot.TowerGroup.TowerId==id)
            {
                return slot;
            }
        }

        return null;
    }

    public void OnBeginDragSlot(Vector3 dragStart)
    {
        IsSlotDragging = true;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, dragStart);
        lineRenderer.SetPosition(1, dragStart);

    }

    public void OnDragSlot(Vector3 enterSlot)
    {
        lineRenderer.SetPosition(1, enterSlot);
    }

    public void OnEndDragSlot()
    {
        IsSlotDragging = false;
        lineRenderer.enabled = false;
    }

    static public Vector3 GetTowerPosition(int towerCount, int towerIndex)
    {
        return TowerPositions[towerCount].positions[towerIndex].localPosition;
    }
}
