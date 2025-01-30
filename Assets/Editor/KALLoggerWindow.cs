using UnityEngine;
using UnityEditor;
using System.Linq;

public class KALLoggerWindow : EditorWindow
{
    // �޴� �׸��� ���� �����츦 �� �� �ֵ��� �ϱ�
    [MenuItem("Window/KALLogger Window")]
    public static void ShowWindow()
    {
        KALLoggerWindow window = GetWindow<KALLoggerWindow>("KALLogger Window");
        window.Show();
    }

    private void OnGUI()
    {
        // GUI ����
        GUILayout.Label("This is a custom editor window", EditorStyles.boldLabel);

        foreach (var item in KALLogger.logFilters.ToList())
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(item.Key);
            bool newValue = EditorGUILayout.Toggle(item.Value);
            GUILayout.EndHorizontal();
            KALLogger.logFilters[item.Key] = newValue;
        }
    }

}
