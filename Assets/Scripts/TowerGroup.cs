using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerGroup : MonoBehaviour
{
    public eTower TowerId
    {
        get;
        private set;
    }

    [ReadOnly, SerializeField]
    private List<Tower> towers = new();

    public EnemyManager enemyManager
    {
        get;
        private set;
    }

    public readonly int maxSlotTowerCount = 3;
    public bool IsEmpty
    {
        get => towers.Count == 0;
    }

    public bool IsFull
    {
        get => towers.Count >= maxSlotTowerCount;
    }

    private void Awake()
    {
        enemyManager = GameObject.FindObjectOfType<GameManager>().EnemyManager;
    }


    public void AddTower(Tower tower)
    {
        if (IsEmpty)
        {
            TowerId = tower.TowerId;
        }

        tower.OnSpawn(this);
        towers.Add(tower);
        UpdateTowerPosition();
    }

    public void RemoveTower()
    {
        var tower = towers[towers.Count - 1];
        towers.RemoveAt(towers.Count-1);
        GameObject.Destroy(tower.gameObject);

        UpdateTowerPosition();
    }

    public void UpdateTowerPosition()
    {
        for (int i = 0; i < towers.Count; i++)
        {
            towers[i].transform.localPosition = SlotManager.GetTowerPosition(towers.Count, i);
        }
    }

    private void OnDrawGizmos()
    {
        if (!IsEmpty)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, towers[0].AttackRange);
        }
    }
}
