using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    //�ӽ÷� Ÿ���� ���� �д�
    [SerializeField]
    private List<GameObject> towerPrefabs = new List<GameObject>();
    //private Dictionary<string, GameObject> towerPrefabsDictionary = new();


    public GameObject GetTower()
    {
        int index = Random.Range(0, towerPrefabs.Count);

        return Instantiate(towerPrefabs[index]);
    }
}
