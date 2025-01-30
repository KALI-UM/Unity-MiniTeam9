using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveTable;

public class GameManager : MonoBehaviour
{
    [ReadOnly]
    public readonly CoinGemSystem coinGemSystem = new();
    public int initialCoinCount = 200;
    public int initialGemCount = 0;

    #region Managers
    //public InGameManager[] managers;
    [SerializeField]
    private SlotManager slotManager;

    [SerializeField]
    private TowerManager towerManager;

    [SerializeField]
    private EnemyManager enemyManager;

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

    public UIManager UIManager
    {
        get => uiManager;
    }

    #endregion

    public Action onGameClear;
    public Action onGameOver;

    #region Wave

    [SerializeField]
    private List<WaveData> waveDatas;

    public WaveData CurrentWaveData
    {
        get => waveDatas[CurrentWaveNumber];
    }

    public int CurrentWaveNumber
    {
        get;
        private set;
    }

    public int lastWaveNumber = 10;
    public bool IsLastWave
    {
        get => CurrentWaveNumber >= lastWaveNumber;
    }

    public Action<WaveData> onWaveStart;

    private Coroutine coWave;
    private Coroutine coWaveSpawnEnemy;

    #endregion


    private void Awake()
    {
        InitializeManagers();

        waveDatas = DataTableManager.WaveTable.GetWaveDatas();
        CurrentWaveNumber = 0;
    }

    private void InitializeManagers()
    {
        SlotManager.Initialize(this);
        TowerManager.Initialize(this);
        EnemyManager.Initialize(this);
        UIManager.Initialize(this);

        coinGemSystem.AddCoin(initialCoinCount);
        coinGemSystem.AddGem(initialGemCount);
    }

    private void Start()
    {
        coWave = StartCoroutine(CoWave());
    }

    private void Update()
    {
        switch (EnemyManager.ValidEnemies.Count)
        {
            case 0:
                {
                    if (IsLastWave)
                    {
                        OnGameClear();
                    }
                    break;
                }
            case 100:
                {
                    OnGameOver();
                    break;
                }
        }
    }

    private IEnumerator CoSpawnEnemy(int waveNumber)
    {
        for (int i = 0; i < waveDatas[waveNumber].enemyCount; i++)
        {
            EnemyManager.SpawnEnemy(waveDatas[waveNumber].enemyId);
            yield return new WaitForSeconds(waveDatas[CurrentWaveNumber].spawnInterval);
        }
    }

    private IEnumerator CoWave()
    {
        while (!IsLastWave)
        {
            OnWaveStart();
            coWaveSpawnEnemy = StartCoroutine(CoSpawnEnemy(CurrentWaveNumber));
            yield return new WaitForSeconds(CurrentWaveData.waveDuration);
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
        coinGemSystem.AddCoin(CurrentWaveNumber * 10);

        CurrentWaveNumber++;
        onWaveStart?.Invoke(CurrentWaveData);
        KALLogger.Log($"ÇöÀç Wave{CurrentWaveNumber}");
    }
}
