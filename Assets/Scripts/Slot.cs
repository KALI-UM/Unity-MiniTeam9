using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Slot : MonoBehaviour
{
    private EnemyManager enemyManager;

    public eTower TowerId;
    private List<Tower> towers = new();

    private int columnIndex;
    private int rowIndex;

    public bool IsEmpty
    {
        get;
        private set;
    }

    public int maxSlotTowerCount = 3;
    public bool IsFull
    {
        get;
        private set;
    }

    private Coroutine coTowerAttack;

    private void Awake()
    {
        enemyManager = GameObject.FindGameObjectsWithTag("GameController").First(obj => obj.name.Equals("EnemyManager")).GetComponent<EnemyManager>();

        IsEmpty = true;
        IsFull = false;
    }

    public void SetIndex(int rowIndex, int colIndex)
    {
        this.columnIndex = colIndex;
        this.rowIndex = rowIndex;
    }

    public bool IsPossibleToAdd(eTower towerId)
    {
        if (IsEmpty)
            return true;

        return (TowerId==towerId) && (!IsFull);
    }

    public void AddTower(Tower tower)
    {
        if (IsEmpty)
        {
            TowerId = tower.TowerId;
            IsEmpty = false;

            towers.Add(tower);
            coTowerAttack = StartCoroutine(CoTowerAttack());
        }
        else
        {
            towers.Add(tower);
        }

        for (int i = 0; i < towers.Count; i++)
        {
            towers[i].transform.position = transform.position + SlotManager.GetTowerPosition(towers.Count, i);
        }

        //KALLogger.Log(tower.TowerId + $"»ðÀÔ slot({rowIndex},{columnIndex})", this);
        if (towers.Count >= maxSlotTowerCount)
        {
            IsFull = true;
        }
    }

    public void RemoveTower()
    {

    }

    public void RemoveAllTower()
    {

    }

    private IEnumerator CoTowerAttack()
    {
        while (true)
        {
            foreach (Tower tower in towers)
            {
                if (!tower.IsValidTarget)
                {
                    FindTowerTarget(tower);
                }

                if (tower.IsValidTarget)
                {
                    tower.AttackTarget();
                }

            }
            yield return new WaitForSeconds(TowerManager.SpeedFactor*towers[0].Data.AttackSpeed);
        }
    }

    private bool FindTowerTarget(Tower tower)
    {
        var closestEnemy = enemyManager.ValidEnemies.OrderBy(e => Vector3.Distance(e.transform.position, transform.position)).FirstOrDefault();

        if (closestEnemy != null && Vector3.Distance(closestEnemy.transform.position, transform.position) <= tower.AttackRange)
        {
            tower.SetTarget(closestEnemy);
            return true;
        }
        return false;
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
