using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveTable;

public class GameManager : MonoBehaviour
{

    private readonly GoldGemSystem goldGemSystem = new();
    public GoldGemSystem GoldGemSystem
    {
        get => goldGemSystem;
    }


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

    [SerializeField]
    private EffectManager effectManager;


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

    public EffectManager EffectManager
    {
        get => effectManager;
    }

    #endregion

    private Coroutine coStartThreshold;
    public float startThresholdTime = 10f;

    #region Wave
    [SerializeField]
    private WaveSystem waveSystem;
    public WaveSystem WaveSystem
    {
        get => waveSystem;
    }

    [SerializeField]
    private int maxEnemyCount = 100;

    public Action onGameClear;
    public Action onGameOver;

    #endregion
    private void Awake()
    {
        InitializeManagers();
        EnemyManager.onBossEnemyDie += () => OnBossEnemyDie();
    }
    private void Start()
    {
        var audioData = Resources.Load<AudioClipPackData>("Datas/InGameAudioPackData");
        SoundManager.Instance.SetAudioClipPack(audioData);
        SoundManager.Instance.PlayBGM("Bgm_battle01");

        GoldGemSystem.onGoldPayFail += () => UIManager.Alert("Alert_LessGold");
        GoldGemSystem.onGemPayFail += () => UIManager.Alert("Alert_LessGem");
        GoldGemSystem.AddGold(initialCoinCount);
        GoldGemSystem.AddGem(initialGemCount);

        WaveSystem.onWaveStart += (WaveData data) => GoldGemSystem.AddGold(data.waveNumber * 10);

#if UNITY_EDITOR
        //프레임제한 풀기
        Application.targetFrameRate = -1;
#endif
        coStartThreshold = StartCoroutine(CoStartDelay());
       
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
    }
    private void Update()
    {
        if (EnemyManager.ValidEnemies.Count >= maxEnemyCount)
        {
            OnGameOver();
        }
    }

    private IEnumerator CoStartDelay()
    {
        yield return new WaitForSecondsRealtime(startThresholdTime);
        WaveSystem.StartWave(1);
        //waveSystem.StartWaveAsync(1);
    }

    public void OnGameOver()
    {
        WaveSystem.StopWave();
        onGameOver?.Invoke();
    }

    public void OnGameClear()
    {
        WaveSystem.StopWave();
        onGameClear?.Invoke();
        KALLogger.Log("Game Clear");
    }

    public void OnBossEnemyDie()
    {
        if (WaveSystem.IsLastWave)
        {
            OnGameClear();
        }
        else
        {
            WaveSystem.StartWave(WaveSystem.CurrentWaveNumber + 1);
        }
    }
}
