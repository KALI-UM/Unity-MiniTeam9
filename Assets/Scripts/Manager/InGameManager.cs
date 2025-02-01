using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameManager GameManager { get => gameManager; }

    private static InGameManagers managerId;
    public InGameManagers Id
    {
        get
        {
            return managerId;
        }
    }

    public virtual void InitializeManager(GameManager gm)
    {
        gameManager = gm;
    }

    public virtual void Initialize()
    {
    }

}
