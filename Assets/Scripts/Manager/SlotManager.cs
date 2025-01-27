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

    [SerializeField]
    private int maxTowerCount = 50;
    private int currTowerCount;

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
                    gameManager.UIManager.Open(FocusWindows.TowerInteraction);
                }
                else
                {
                    gameManager.UIManager.Close(FocusWindows.TowerInteraction);
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

    public bool IsPossibleToAdd()
    {
        return (currTowerCount <= maxTowerCount && IsEmptySlotExist());
    }

    public void AddTower(Tower tower)
    {
        foreach (var slot in slots)
        {
            if (slot.IsPossibleToAdd(tower.TowerId))
            {
                slot.AddTower(tower);
                return;
            }
        }
    }

    static public Vector3 GetTowerPosition(int towerCount, int towerIndex)
    {
        return TowerPositions[towerCount].positions[towerIndex].localPosition;
    }
}
