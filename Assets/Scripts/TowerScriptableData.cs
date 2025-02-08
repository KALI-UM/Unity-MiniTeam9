using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScriptableData : MonoBehaviour
{
    public TowerData currentTowerData;

    [HideInInspector]
    public GameManager gameManager;

    private void Awake()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("GameController"))
        {
            var mgr = item.GetComponent<GameManager>();
            if(mgr!=null)
            {
                gameManager = mgr;
                return;
            }
        }
    }
}
