using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using static Enemy;

public class EnemyManager : InGameManager
{
    public GameObject defaultEnemyPrefab;
    private readonly Dictionary<eEnemy, GameObject> enemyPrefabs = new();

    private Dictionary<eEnemy, ObjectPool<Enemy>> enemyPools = new();

    //Valid = 공격가능한 enemy 목록을 관리
    private List<Enemy> validEnemies = new();
    public List<Enemy> ValidEnemies
    {
        get => validEnemies;
    }


    #region EnemyData

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

    #endregion


    #region EnemyCount

    [SerializeField]
    private EnemyCountBar enemyCountBar;

    public int CurrEnemyCount
    {
        get
        {
            return validEnemies.Count;
        }
    }

    #endregion

    private void Awake()
    {
        InitializeEnemyInitData();
        InitializeEnemyPrefabs();
        InitializeEnemyPool();

        enemyCountBar.SetMaxValue(100);
    }

    private void InitializeEnemyPrefabs()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/Enemy");
        foreach (var enmey in prefabs)
        {
            var id = Enum.Parse<eEnemy>(enmey.name);
            enemyPrefabs.Add(id, enmey);
        }
    }

    private void InitializeEnemyPool()
    {
        enemyPools.Clear();
        enemyPrefabs.ToList().ForEach(enemy =>
        {
            ObjectPool<Enemy> pool = new
            (
                createFunc: () => CreateEnemy(Instantiate(enemy.Value)),
                actionOnGet: e => OnGetEnemy(e),
                actionOnRelease: e => OnReleaseEnemy(e),
                //actionOnDestroy: obj => obj.Dispose(),
                //collectionCheck: false,
                defaultCapacity: 35,
                maxSize: 100
            );
            enemyPools.Add(enemy.Key, pool);
        }
        );
    }

    private void InitializeEnemyInitData()
    {
        WayPointData = wayPointData;
        HpColors = hpColors.OrderByDescending(c => c.value).ToArray();
    }

    public void SpawnEnemy(eEnemy type)
    {
        Enemy enemy = enemyPools[type].Get();
        enemy.transform.SetParent(gameObject.transform);
        validEnemies.Add(enemy);
        enemyCountBar.OnCountChanged(CurrEnemyCount);
    }

    private Enemy CreateEnemy(GameObject gobj)
    {
        Enemy enemy = gobj.GetComponent<Enemy>();
        enemy.onDie += () =>
        {
            enemyPools[enemy.EnemyId].Release(enemy);
            validEnemies.Remove(enemy);
            enemyCountBar.OnCountChanged(CurrEnemyCount);
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
