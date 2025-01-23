using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyManager enemyManager;

    [SerializeField]
    private TowerManager towerManager;

    [SerializeField]
    private SlotManager slotManager;

    [SerializeField]
    private WindowManager windowManager;


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

    public bool IsWaveEnded
    {
        get => CurrentWave >= waveDatas.Count;
    }

    public Action onWaveStart;

    private Coroutine coSpawnEnemy;
    private void Awake()
    {
        EnemyManager.gameManager = this;
        TowerManager.gameManager = this;
        SlotManager.gameManager = this;
        WindowManager.gameManager = this;

        onWaveStart += () => WindowManager.Open(PopWindows.WavePop);
        onGameOver += () => WindowManager.Open(GenericWindows.GameOver);
        onGameClear += () => WindowManager.Open(GenericWindows.GameClear);
    }

    private void Start()
    {
        coSpawnEnemy = StartCoroutine(CoSpawnEnemy());
        //StartCoroutine(CoSpawnEnemy(waveEnemyCount));
    }

    private void Update()
    {
        if (IsWaveEnded && EnemyManager.ValidEnemies.Count == 0)
        {
        }
    }

    private IEnumerator CoSpawnEnemy()
    {
        while (!IsWaveEnded)
        {
            for (int i = 0; i < waveDatas[CurrentWave].enemyCount; i++)
            {
                EnemyManager.SpawnEnemy(Enemy.EnemyType.SoldierA);
                yield return new WaitForSeconds(waveDatas[CurrentWave].spawnInterval);
            }
            yield return new WaitForSeconds(waveDatas[CurrentWave].nextWaveInterval);
            CurrentWave++;
            onWaveStart?.Invoke();
            KALLogger.Log($"ÇöÀç Wave{CurrentWave}");
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
}
