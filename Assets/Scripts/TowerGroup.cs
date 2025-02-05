using DG.Tweening;
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

    public TowerData Data
    {
        get => towers[0].Data;
    }

    [ReadOnly, SerializeField]
    private List<Tower> towers = new();

    public TowerManager towerManager;
    public EnemyManager enemyManager;

    public readonly int maxSlotTowerCount = 3;
    public bool IsEmpty
    {
        get => towers.Count == 0;
    }

    public bool IsFull
    {
        get
        {
            if (Data.grade == towerManager.MaxGrade)
            {
                return towers.Count >= 1;
            }
            else
            {
                return towers.Count >= maxSlotTowerCount;
            }
        }
    }

    public bool CanFusion
    {
        get => IsFull && Data.grade < towerManager.MaxGrade-1;
    }

    #region Move
    private Coroutine coMoveTo;

    #endregion

    public void AddTower(Tower tower)
    {
        if (IsEmpty)
        {
            TowerId = tower.TowerId;
        }

        tower.OnAddTowerGroup(this);
        towers.Add(tower);
        towerManager.TowerCountChange(1);

        UpdateTowerPosition();
    }

    public void RemoveTower()
    {
        var tower = towers[towers.Count - 1];
        towers.RemoveAt(towers.Count - 1);
        towerManager.DestoryTower(tower);
        UpdateTowerPosition();
    }

    public Tower SendToNewTowerGroup()
    {
        var tower = towers[towers.Count - 1];
        towers.RemoveAt(towers.Count - 1);

        UpdateTowerPosition();
        return tower;
    }

    public void ReceiveTower(Tower tower)
    {
        tower.OnAddTowerGroup(this);
        towers.Add(tower);

        UpdateTowerPosition();
    }

    public void UpdateTowerPosition()
    {
        for (int i = 0; i < towers.Count; i++)
        {
            towers[i].transform.localPosition = SlotManager.GetTowerPosition(towers.Count, i);
        }
    }

    public void MoveTo(Vector3 destination)
    {
        StartCoroutine(CoMoveTo(destination));
    }

    private IEnumerator CoMoveTo(Vector3 destination)
    {
        foreach (var tower in towers)
        {
            tower.towerAttack.enabled = false;
        }

        Vector3 dir = (destination - transform.position).normalized;
        float distance = Vector3.Distance(destination, transform.position);
        while (true)
        {
            Vector3 move = dir * Time.deltaTime * TowerManager.GlobalTowerMoveSpeed;
            transform.position += move;
            distance -= move.magnitude;
            if (distance < 0)
            {
                transform.position = destination;
                break;
            }
            yield return new WaitForFixedUpdate();
        }

        foreach (var tower in towers)
        {
            tower.towerAttack.enabled = true;
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
