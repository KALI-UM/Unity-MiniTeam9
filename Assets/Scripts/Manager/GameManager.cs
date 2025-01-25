using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveTable;

public class GameManager : MonoBehaviour
{
    #region Managers
    //public InGameManager[] managers;
    [SerializeField]
    private SlotManager slotManager;

    [SerializeField]
    private TowerManager towerManager;

    [SerializeField]
    private EnemyManager enemyManager;

    [SerializeField]
    private WindowManager windowManager;

    [SerializeField]
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

    #endregion

    public Action onGameClear;
    public Action onGameOver;

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

    private Coroutine coWave;
    private Coroutine coWaveSpawnEnemy;

    private void Awake()
    {
        //foreach (var manager in managers)
        //{
        //    manager.Initialize(this, Enum.Parse<InGameManagers>(manager.name));
        //}

        InitializeManagers();

        waveDatas = DataTableManager.WaveTable.GetWaveDatas();

        CurrentWave = 0;
    }

    private void InitializeManagers()
    {
        SlotManager.Initialize(this);
        TowerManager.Initialize(this);
        EnemyManager.Initialize(this);
        WindowManager.Initialize(this);
        UIManager.Initialize(this);
    }

    private void Start()
    {
        coWave = StartCoroutine(CoWave());
    }

    private void Update()
    {
        if (IsLastWave && EnemyManager.ValidEnemies.Count == 0)
        {
            OnGameClear();
        }
    }

    private IEnumerator CoSpawnEnemy(int waveNumber)
    {
        for (int i = 0; i < waveDatas[waveNumber].enemyCount; i++)
        {
            EnemyManager.SpawnEnemy(waveDatas[waveNumber].enemyId);
            yield return new WaitForSeconds(waveDatas[CurrentWave].spawnInterval);
        }
    }

    private IEnumerator CoWave()
    {
        while (!IsLastWave)
        {
            OnWaveStart();
            coWaveSpawnEnemy = StartCoroutine(CoSpawnEnemy(CurrentWave));
            yield return new WaitForSeconds(waveDatas[CurrentWave].spawnDuration);
            StopCoroutine(coWaveSpawnEnemy);
        }
    }

    public void OnGameOver()
    {
        StopCoroutine(coWave);
        StopCoroutine(coWaveSpawnEnemy);

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
