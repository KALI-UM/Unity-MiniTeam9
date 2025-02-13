using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using static Enemy;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyManager : InGameManager
{
    [SerializeField]
    private GameObject defaultEnemyPrefab;

    [SerializeField]
    private GameObject bossEnemyPrefab;

    private readonly Dictionary<eEnemy, GameObject> enemyPrefabs = new();
    private Dictionary<eEnemy, ObjectPool<Enemy>> enemyPools = new();


    [ReadOnly, SerializeField]
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

    public Action<int> onEnemyCountChange;

    public int CurrEnemyCount
    {
        get
        {
            return validEnemies.Count;
        }
    }

    public Action onBossEnemyDie;

    #endregion

    private void Awake()
    {
        InitializeEnemyInitData();
        InitializeEnemyPrefabs();
        InitializeEnemyPools();
    }

    private void InitializeEnemyPrefabs()
    {
#if UNITY_EDITOR
        //EnemyData[] datas = Resources.LoadAll<EnemyData>("Datas/Enemy");
        //foreach (var data in datas)
        //{
        //    GameObject enemyPrefab = (data.grade == 2 ? Instantiate(bossEnemyPrefab) : Instantiate(defaultEnemyPrefab));
        //    var enemy = enemyPrefab.GetComponent<Enemy>();

        //    //해당 데이터 ScriptableObject
        //    enemy.InitializeData(data);

        //    if (data.enemySpum != null)
        //    {
        //        GameObject spumprefab = Instantiate(data.enemySpum);
        //        Vector3 scale = enemy.character.transform.GetChild(0).transform.localScale;
        //        GameObject.DestroyImmediate(enemy.character.transform.GetChild(0).gameObject);

        //        spumprefab.transform.localScale = scale;
        //        spumprefab.transform.position = Vector3.zero;
        //        spumprefab.transform.SetParent(enemy.character.transform);
        //        var handler = spumprefab.AddComponent<SpumAnimationHandler>();
        //        enemy.animationHandler = handler;
        //    }
        //    enemyPrefabs.Add(data.Id, enemyPrefab);
        //    enemyPrefab.name = data.key;
        //    enemyPrefab.transform.position = new Vector3(100, 100, 100);
        //}
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/Enemy");
        foreach (var enmey in prefabs)
        {
            var id = Enum.Parse<eEnemy>(enmey.name);
            enemyPrefabs.Add(id, enmey);
        }
#else
    GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/Enemy");
        foreach (var enmey in prefabs)
        {
            var id = Enum.Parse<eEnemy>(enmey.name);
            enemyPrefabs.Add(id, enmey);
        }
#endif
    }

    private void InitializeEnemyPools()
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
                defaultCapacity: 50,
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
        onEnemyCountChange?.Invoke(CurrEnemyCount);
    }

    private Enemy CreateEnemy(GameObject gobj)
    {
        Enemy enemy = gobj.GetComponent<Enemy>();

        if (enemy.Data.grade == 2)
        {
            enemy.onDie += () =>
            {
                validEnemies.Remove(enemy);
                onEnemyCountChange?.Invoke(CurrEnemyCount);

                GameManager.goldGemSystem.AddGold(enemy.Data.dropGold);
                GameManager.goldGemSystem.AddGem(enemy.Data.dropGem);

                onBossEnemyDie?.Invoke();
            };

            enemy.onSpawn +=()=> SoundManager.Instance.PlayBGM("Bgm_Boss01");
        }
        else
        {
            enemy.onDie += () =>
            {
                validEnemies.Remove(enemy);
                onEnemyCountChange?.Invoke(CurrEnemyCount);

                GameManager.goldGemSystem.AddGold(enemy.Data.dropGold);
                GameManager.goldGemSystem.AddGem(enemy.Data.dropGem);
            };
        }

        enemy.onDamaged += (int damage) =>
        {
            temp(enemy.transform, damage);
        };
        enemy.animationHandler.onDeathExit += () => enemyPools[enemy.EnemyId].Release(enemy);
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


    public void temp(Transform position, int damage)
    {
        var effect = EnemyManager.EffectManager.Get(eEffects.DamageText) as DamageText;

        effect.Play(position.position);
        effect.SetDamageText(damage);
    }
}
