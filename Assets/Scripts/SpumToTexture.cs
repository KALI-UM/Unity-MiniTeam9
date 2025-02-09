using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class SpumToTexture : MonoBehaviour
{
    public Camera spumCamera;
    public List<GameObject> spumToTextureQueue = new();

    private string spumTexPath = "Assets/Resources/Textures/Tower/{0}";

    private bool isProcessing = false; // 진행 중인지 체크

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
            EditorApplication.update += ProcessQueue; // 에디터에서 실행되도록 변경
        }
    }

    private void ProcessQueue()
    {
        if (spumToTextureQueue.Count == 0)
        {
            isProcessing = false;
            EditorApplication.update -= ProcessQueue; // 업데이트 루프에서 제거
            AssetDatabase.Refresh();
            return;
        }

        var spum = spumToTextureQueue[0];
        spumToTextureQueue.RemoveAt(0);
        spum.SetActive(true);

        SceneView.RepaintAll(); // 에디터 모드에서 뷰 갱신

        //한 프레임 기다릴 필요 없이 바로 렌더링
        spumCamera.Render();
        SaveTextureToFileUtility.SaveRenderTextureToFile(spumCamera.targetTexture, string.Format(spumTexPath, spum.name));

        Debug.Log("캡쳐 완료: " + spum.name);
        GameObject.DestroyImmediate(spum);
    }


    public void Test()
    {
        SaveTextureToFileUtility.SaveRenderTextureToFile(spumCamera.targetTexture, string.Format(spumTexPath, "temp"));
    }
}

