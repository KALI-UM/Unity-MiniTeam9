using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    //임시로 타워를 여기 둔다
    [SerializeField]
    private List<GameObject> towerPrefabs = new List<GameObject>();
    //private Dictionary<string, GameObject> towerPrefabsDictionary = new();


    public GameObject GetTower()
    {
        int index = Random.Range(0, towerPrefabs.Count);

        return Instantiate(towerPrefabs[index]);
    }
}
