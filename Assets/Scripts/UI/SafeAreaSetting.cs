using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class SafeAreaSetting : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        var newPos = rectTransform.position;
        if (rectTransform.anchorMax.y == rectTransform.anchorMin.y)
        {
            switch (rectTransform.anchorMax.y)
            {
                case 0f:
                    newPos.y = Screen.safeArea.y;
                    break;
                case 0.5f:
                    newPos.y = (Screen.safeArea.yMax + Screen.safeArea.y) * 0.5f;
                    break;
                case 1f:
                    newPos.y = Screen.safeArea.yMax;
                    break;
            }
        }

        if (rectTransform.anchorMax.x == rectTransform.anchorMin.x)
        {
            switch (rectTransform.anchorMax.x)
            {
                case 0f:
                    newPos.x = Screen.safeArea.x;
                    break;
                case 0.5f:
                    newPos.x = (Screen.safeArea.xMax + Screen.safeArea.x) * 0.5f;
                    break;
                case 1f:
                    newPos.x = Screen.safeArea.xMax;
                    break;
            }
        }

        rectTransform.position = newPos;
    }
}
