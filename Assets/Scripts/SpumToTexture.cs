using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteAlways]

public class SpumToTexture : MonoBehaviour
{
    public Camera spumCamera;
    public List<GameObject> spumToTextureQueue = new();

    private string spumTexPath = "Assets/Resources/Textures/Tower/{0}";

    private bool isProcessing = false; // ���� ������ üũ

    public void PushToSpumToTextureQueue(GameObject spum)
    {
        spumToTextureQueue.Add(spum);
    }

    public void StartCapture()
    {
        foreach (var spum in spumToTextureQueue)
        {
            spum.transform.position = new Vector3(5, 1, 1);
            spum.SetActive(false);
        }

        if (!isProcessing)
        {
            isProcessing = true;
            EditorApplication.update += ProcessQueue; // �����Ϳ��� ����ǵ��� ����
        }
    }

    private void ProcessQueue()
    {
        if (spumToTextureQueue.Count == 0)
        {
            isProcessing = false;
            EditorApplication.update -= ProcessQueue; // ������Ʈ �������� ����
            AssetDatabase.Refresh();
            return;
        }

        var spum = spumToTextureQueue[0];
        spumToTextureQueue.RemoveAt(0);
        spum.SetActive(true);

        SceneView.RepaintAll(); // ������ ��忡�� �� ����

        //�� ������ ��ٸ� �ʿ� ���� �ٷ� ������
        spumCamera.Render();
        SaveTextureToFileUtility.SaveRenderTextureToFile(spumCamera.targetTexture, string.Format(spumTexPath, spum.name));

        Debug.Log("ĸ�� �Ϸ�: " + spum.name);
        GameObject.DestroyImmediate(spum);
    }


    public void Test()
    {
        SaveTextureToFileUtility.SaveRenderTextureToFile(spumCamera.targetTexture, string.Format(spumTexPath, "temp"));
    }
}

#endif
