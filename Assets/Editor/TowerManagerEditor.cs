using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;

[CustomEditor(typeof(TowerManager))]
public class TowerManagerEditor : Editor
{
   static private float radiusCoefficient = 1f;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        float prevValue = TowerManager.RadiusCoefficient;
        TowerManager.RadiusCoefficient = EditorGUILayout.Slider("RadiusCoefficient", TowerManager.RadiusCoefficient, 1, 10);

        if (prevValue != TowerManager.RadiusCoefficient)
        {
            //KALLogger.Log("������ ��� ����");
            EditorUtility.SetDirty(this);
        }
    }
}
