using System.Collections.Generic;
using UnityEngine;

public class TowerEditor : MonoBehaviour
{
    public GameObject defaultTowerPrefab;
    public List<Color> shadowColors = new List<Color>();

#if UNITY_EDITOR
    public SpumToTexture spumToTexture;
#endif



}
