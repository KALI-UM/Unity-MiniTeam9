using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    //Valid = 공격가능한 enemy 목록을 관리
    private List<Enemy> validEnemies = new();
    public List<Enemy> ValidEnemies
    {
        get => validEnemies;
    }

    [SerializeField]
    private WayPointData wayPointData;
    static public WayPointData WayPointData
    {
        get;
        private set;
    }

    [Serializable]
    public struct ColorValue
    {
        public Color color;
        public float value;
    }

    [SerializeField]
    private ColorValue[] hpColors = new ColorValue[3];
    static public ColorValue[] HpColors
    {
        get;
        private set;
    }

    public int CurrEnemyCount
    {
        get
        {
            return enemyPools.Count;
        }
    }

    private void Awake()
    {
        InitializeEnemyInitData();
        InitializeEnemyPool();
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

    private void InitializeEnemyInitData()
    {
        WayPointData = wayPointData;
        HpColors = hpColors.OrderByDescending(c => c.value).ToArray();
    }

    public void SpawnEnemy(EnemyType type)
    {
        Enemy enemy = enemyPools[type].Get();
        enemy.transform.SetParent(gameObject.transform);
        validEnemies.Add(enemy);
    }

    private Enemy CreateEnemy(GameObject gobj)
    {
        Enemy enemy = gobj.GetComponent<Enemy>();
        enemy.onDie += () =>
        {
            enemyPools[enemy.data.type].Release(enemy);
            validEnemies.Remove(enemy);
        };
        return enemy;
    }

    private void OnGetEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.OnReset();
        enemy.Spawn();
    }

    private void OnReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}
