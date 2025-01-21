using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using static Enemy;

public class EnemyManager : MonoBehaviour
{
    public GameManager gameManager;

    [Serializable]
    public struct EnemyPrefab
    {
        public EnemyType type;
        public GameObject prefab;
    }
    public List<EnemyPrefab> enemyPrefabs = new();
    private Dictionary<EnemyType, ObjectPool<Enemy>> enemyPools = new();

    [SerializeField]
    private WayPointData wayPointData;


    public int CurrEnemyCount
    {
        get;
        private set;
    }

    private void Awake()
    {
        InitializeEnemyPool();
        CurrEnemyCount = 0;
    }

    private void InitializeEnemyPool()
    {
        enemyPools.Clear();
        enemyPrefabs.ForEach(enemy =>
        {
            ObjectPool<Enemy> pool = new
            (
                createFunc: () => CreateEnemy(Instantiate(enemy.prefab)),
                actionOnGet: e => OnGetEnemy(e),
                actionOnRelease: e => OnReleaseEnemy(e),
                //actionOnDestroy: obj => obj.Dispose(),
                //collectionCheck: false,
                defaultCapacity: 35,
                maxSize: 100
            );
            enemyPools.Add(enemy.type, pool);
        }
        );
    }

    public void SpawnEnemy(EnemyType type)
    {
        Enemy enemy = enemyPools[type].Get();
        enemy.transform.SetParent(gameObject.transform);
    }

    private Enemy CreateEnemy(GameObject gobj)
    {
        Enemy enemy = gobj.GetComponent<Enemy>();
        enemy.movement.wayPointData = wayPointData;
        enemy.onDie += () => { 
            enemyPools[enemy.data.type].Release(enemy);
            CurrEnemyCount--;
        };
        return enemy;
    }

    private void OnGetEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.OnReset();
        enemy.Spawn();
        CurrEnemyCount++;
    }

    private void OnReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}
