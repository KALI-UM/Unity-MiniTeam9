using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemyManager enemyManager;

    public float enemySpwanInterval = 5f;
    public int wave = 0;
    public int waveEnemyCount = 35;
    private Coroutine coSpawnEnemy;


    private void Start()
    {
        coSpawnEnemy = StartCoroutine(CoSpawnEnemy(35));
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
