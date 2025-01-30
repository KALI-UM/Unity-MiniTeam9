using UnityEngine;
using UnityEditor;
using System.Linq;

public class KALLoggerWindow : EditorWindow
{
    // 메뉴 항목을 통해 윈도우를 열 수 있도록 하기
    [MenuItem("Window/KALLogger Window")]
    public static void ShowWindow()
    {
        KALLoggerWindow window = GetWindow<KALLoggerWindow>("KALLogger Window");
        window.Show();
    }

    private void OnGUI()
    {
        // GUI 구성
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
