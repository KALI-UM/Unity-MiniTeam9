using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField]
    private GameObject slotPrefab;

    public List<Slot[]> slots = new();
    [SerializeField]
    private int columnCount = 6;
    [SerializeField]
    private int rowCount = 4;

    [SerializeField]
    private int maxTowerCount = 50;
    private int currTowerCount;

    public float colDistance = 1f;
    public float rowDistance = 0.9f;

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

    public bool IsPossibleToAdd
    {
        get
        {
            return (currTowerCount <= maxTowerCount && IsEmptySlotExist());
        }
    }

    private void Awake()
    {
        TowerPositions = towerPositions;

        for (int i = 0; i < columnCount; i++)
        {
            Slot[] slotRow = new Slot[rowCount];
            for (int j = 0; j < rowCount; j++)
            {
                slotRow[j] = Instantiate(slotPrefab).GetComponent<Slot>();
                slotRow[j].transform.SetParent(gameObject.transform);
                slotRow[j].gameObject.name = slotPrefab.name + i + j;
                slotRow[j].SetIndex(j, i);
                slotRow[j].transform.localPosition = new Vector3(i * colDistance, -j * rowDistance, 0);
            }
            slots.Add(slotRow);
        }
    }

    public bool IsEmptySlotExist()
    {
        foreach (var slotRow in slots)
        {
            foreach (var slot in slotRow)
            {
                if (slot.IsEmpty)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void AddTower(Tower tower)
    {
        foreach (var slotRow in slots)
        {
            foreach (var slot in slotRow)
            {
                if (slot.IsPossibleToAdd(tower.TowerId))
                {
                    slot.AddTower(tower);
                    return;
                }
            }
        }
    }

    static public Vector3 GetTowerPosition(int towerCount, int towerIndex)
    {
        return TowerPositions[towerCount].positions[towerIndex].localPosition;
    }
}
