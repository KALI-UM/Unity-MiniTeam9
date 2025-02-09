using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpumToTexture))]
public class SpumToTextureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("RenderTexture To Png"))
        {
            var spumTex = (SpumToTexture)target;
            spumTex.StartCapture();
        }

        base.OnInspectorGUI();
    }
}
