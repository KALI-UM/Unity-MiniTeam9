using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InGameManager[] managers;

    private EnemyManager enemyManager;
    private TowerManager towerManager;
    private SlotManager slotManager;
    private WindowManager windowManager;
    private UIManager uiManager;

    public EnemyManager EnemyManager
    {
        get => enemyManager;
    }

    public TowerManager TowerManager
    {
        get => towerManager;
    }

    public SlotManager SlotManager
    {
        get => slotManager;
    }

    public WindowManager WindowManager
    {
        get => windowManager;
    }

    public UIManager UIManager
    {
        get => uiManager;
    }

    public Action onGameClear;
    public Action onGameOver;

    [Serializable]
    public struct WaveData
    {
        public float nextWaveInterval;
        public float spawnInterval;
        public int enemyCount;
        public Enemy.EnemyType type;
    }

    [SerializeField]
    private List<WaveData> waveDatas;
    public int CurrentWave
    {
        get;
        private set;
    }

    public int lastWave = 10;
    public bool IsLastWave
    {
        get => CurrentWave >= lastWave;
    }

    public Action onWaveStart;

    private Coroutine coSpawnEnemy;
    private void Awake()
    {
        foreach (var manager in managers)
        {
            manager.Initialize(this, Enum.Parse<InGameManagers>(manager.name));
        }

        onWaveStart += () =>
        {
            WindowManager.Open(PopWindows.Wave);
        };
        onGameOver += () => WindowManager.Open(FocusWindows.GameOver);
        onGameClear += () => WindowManager.Open(FocusWindows.GameClear);
    }

    //public T GetManager<T>()
    //{

    //}

    private void Start()
    {
        coSpawnEnemy = StartCoroutine(CoSpawnEnemy());
        //StartCoroutine(CoSpawnEnemy(waveEnemyCount));
    }

    private void Update()
    {
        if (IsLastWave && EnemyManager.ValidEnemies.Count == 0)
        {
            OnGameClear();
        }
    }

    private IEnumerator CoSpawnEnemy()
    {
        while (!IsLastWave)
        {
            for (int i = 0; i < waveDatas[CurrentWave].enemyCount; i++)
            {
                EnemyManager.SpawnEnemy(Enemy.EnemyType.SoldierA);
                yield return new WaitForSeconds(waveDatas[CurrentWave].spawnInterval);
            }
            yield return new WaitForSeconds(waveDatas[CurrentWave].nextWaveInterval);
            OnWaveStart();
        }

    }

    public void OnGameOver()
    {
        StopCoroutine(coSpawnEnemy);
        onGameOver?.Invoke();
    }

    public void OnGameClear()
    {
        onGameClear?.Invoke();
        KALLogger.Log("Game Clear");
    }

    public void OnWaveStart()
    {
        CurrentWave++;
        onWaveStart?.Invoke();
        KALLogger.Log($"ÇöÀç Wave{CurrentWave}");
    }
}
