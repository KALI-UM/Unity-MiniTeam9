using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static WaveTable;

[Serializable]
public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

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

    [SerializeField]
    private int lastWaveNumber = 10;
    public bool IsLastWave
    {
        get => CurrentWaveNumber >= lastWaveNumber;
    }

    public Action<WaveData> onWaveStart;
    public Action onWaveEnd;
    public Action onBossWaveTimeOver;

    private Coroutine coWave;
    private Coroutine coWaveSpawnEnemy;
    private void Start()
    {
        waveDatas = DataTableManager.Get<WaveTable>(DataTableIds.Wave).GetWaveDatas();
    }
    public void StartWave(int waveNumber)
    {
        //���� Wave�� �ִٸ� ����
        StopWave();

        CurrentWaveNumber = waveNumber;
        onWaveStart?.Invoke(CurrentWaveData);
        coWave = StartCoroutine(CoWave(CurrentWaveData));
    }
    public void StopWave()
    {
        if (coWave != null)
        {
            StopCoroutine(coWave);
            coWave=null;
        }

        if (coWaveSpawnEnemy != null)
        {
            StopCoroutine(coWaveSpawnEnemy);
            coWaveSpawnEnemy=null; 
        }
    }
    private IEnumerator CoWave(WaveData data)
    {
        coWaveSpawnEnemy = StartCoroutine(CoSpawnEnemy(data));
        yield return new WaitForSeconds(data.waveDuration);

        if (data.isBossWave)
        {
            onBossWaveTimeOver?.Invoke();
            yield break;
        }

        if (data.waveNumber < lastWaveNumber)
        {
            StartWave(data.waveNumber + 1);
        }
    }
    private IEnumerator CoSpawnEnemy(WaveData data)
    {
        for (int i = 0; i < data.enemyCount; i++)
        {
            gameManager.EnemyManager.SpawnEnemy(data.enemyId);
            yield return new WaitForSeconds(data.spawnInterval);
        }
    }
}
