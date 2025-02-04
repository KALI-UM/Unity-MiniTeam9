using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveTable;

public class GameManager : MonoBehaviour
{
    [ReadOnly]
    public readonly GoldGemSystem goldGemSystem = new();
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

    private Coroutine coStartThreshold;
    public float startThresholdTime = 3f;

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

        EnemyManager.onBossEnemyDie += () => OnBossEnemyDie();

        waveDatas = DataTableManager.Get<WaveTable>(DataTableIds.Wave).GetWaveDatas();
        CurrentWaveNumber = 0;
    }


    private void InitializeManagers()
    {
        SlotManager.InitializeManager(this);
        TowerManager.InitializeManager(this);
        EnemyManager.InitializeManager(this);
        UIManager.InitializeManager(this);

        SlotManager.Initialize();
        TowerManager.Initialize();
        EnemyManager.Initialize();
        UIManager.Initialize();

        goldGemSystem.AddGold(initialCoinCount);
        goldGemSystem.AddGem(initialGemCount);
    }

    private void Start()
    {
#if UNITY_EDITOR
        //프레임제한 풀기
        Application.targetFrameRate = -1;
#endif
        coStartThreshold = StartCoroutine(CoStartDelay());
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

            if(CurrentWaveNumber!=0&&CurrentWaveNumber % 10==0)
            {
                OnGameOver();
            }
        }
    }

    private IEnumerator CoStartDelay()
    {
        yield return new WaitForSecondsRealtime(startThresholdTime);
        coWave = StartCoroutine(CoWave());
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
        goldGemSystem.AddGold(CurrentWaveNumber * 10);

        CurrentWaveNumber++;
        onWaveStart?.Invoke(CurrentWaveData);
    }

    public void OnBossEnemyDie()
    {
        StopCoroutine(coWave);
        coWave = StartCoroutine(CoWave());
    }
}
