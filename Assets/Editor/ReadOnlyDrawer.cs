using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // ��Ȱ��ȭ
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true; // �ٽ� Ȱ��ȭ
    }
}
