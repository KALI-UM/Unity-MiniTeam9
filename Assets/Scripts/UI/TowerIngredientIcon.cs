using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerIngredientIcon : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private GameObject existIcon;

    public void SetIcon(bool isExist)
    {
        existIcon.SetActive(isExist);
    }

}
