using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float enemySpwanInterval = 5f;
    public int wave = 0;
    public int waveEnemyCount = 35;
    private Coroutine coSpawnEnemy;

    public  EnemyManager enemyManager;
    public  TowerManager towerManager;
    public  SlotManager slotManager;

    private void Awake()
    {
        enemyManager.gameManager = this;
        towerManager.gameManager = this;
        slotManager.gameManager = this;
    }

    private void Start()
    {
        coSpawnEnemy = StartCoroutine(CoSpawnEnemy(waveEnemyCount));
        //StartCoroutine(CoSpawnEnemy(waveEnemyCount));
    }

    private IEnumerator CoSpawnEnemy(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            enemyManager.SpawnEnemy(Enemy.EnemyType.SoldierA);
            yield return new WaitForSeconds(enemySpwanInterval);
        }
        wave++;
    }
}
