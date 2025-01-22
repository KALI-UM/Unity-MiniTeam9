using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public EnemyManager enemyManager;
    public TowerManager towerManager;
    public SlotManager slotManager;

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
    public int CurrWave
    {
        get;
        private set;
    }

    public bool IsWaveEnded
    {
        get => CurrWave >= waveDatas.Count;
    }

    private Coroutine coSpawnEnemy;
    private void Awake()
    {
        enemyManager.gameManager = this;
        towerManager.gameManager = this;
        slotManager.gameManager = this;
    }

    private void Start()
    {
        coSpawnEnemy = StartCoroutine(CoSpawnEnemy());
        //StartCoroutine(CoSpawnEnemy(waveEnemyCount));
    }

    private void Update()
    {
        if (IsWaveEnded&& enemyManager.ValidEnemies.Count==0)
        {
            KALLogger.Log("Game Clear");
        }
    }

    private IEnumerator CoSpawnEnemy()
    {
        while (!IsWaveEnded)
        {
            for (int i = 0; i < waveDatas[CurrWave].enemyCount; i++)
            {
                enemyManager.SpawnEnemy(Enemy.EnemyType.SoldierA);
                yield return new WaitForSeconds(waveDatas[CurrWave].spawnInterval);
            }
            yield return new WaitForSeconds(waveDatas[CurrWave].nextWaveInterval);
            CurrWave++;
            KALLogger.Log($"ÇöÀç Wave{CurrWave}");
        }

    }

    public void OnGameOver()
    {
        StopCoroutine(coSpawnEnemy);
    }
}
