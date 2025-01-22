using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GenericWindow[] windows;

    public Windows defaultWindow = 0;
    public Windows currentWindow { get; private set; }

    private void Start()
    {
        foreach (var window in windows)
        {
            window.Init(this);
            //window.Close();
            window.gameObject.SetActive(false);
        }

        currentWindow = defaultWindow;
        windows[(int)currentWindow].Open();
    }

    public void Open(int windowId)
    {
        Open((Windows)windowId);
    }

    public void Open(Windows window)
    {
        windows[(int)currentWindow].Close();
        currentWindow = window;
        windows[(int)currentWindow].Open();
    }
}
