using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    protected GameManager gameManager;
    public GameManager GameManager { get { return gameManager; } }

    private static InGameManagers managerId;
    public InGameManagers Id
    {
        get
        {
            return managerId;
        }
    }

    public virtual void Initialize(GameManager gm, InGameManagers id)
    {
        gameManager = gm;
        managerId = id;
    }
}
