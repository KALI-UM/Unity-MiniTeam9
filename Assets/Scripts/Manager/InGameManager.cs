using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameManager GameManager { get => gameManager; }

    public EnemyManager EnemyManager
    {
        get => GameManager.EnemyManager;
    }

    public TowerManager TowerManager
    {
        get => GameManager.TowerManager;
    }

    public SlotManager SlotManager
    {
        get => GameManager.SlotManager;
    }

    public UIManager UIManager
    {
        get => GameManager.UIManager;
    }

    public EffectManager EffectManager
    {
        get => GameManager.EffectManager;
    }

    public virtual void InitializeManager(GameManager gm)
    {
        gameManager = gm;
    }

    public virtual void Initialize()
    {
    }

}
