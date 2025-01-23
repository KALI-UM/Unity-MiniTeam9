using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GameManager gameManager;

    public GenericWindow[] genericWindows;
    public PopWindow[] popWindows;

    public GenericWindows currentWindow { get; private set; }

    private void Start()
    {
        foreach (var window in genericWindows)
        {
            window.Initialize(this);
            window.gameObject.SetActive(false);
        }

        foreach (var window in popWindows)
        {
            window.Initialize(this);
            window.gameObject.SetActive(false);
        }
    }

    public void OpenGenericWindow(int windowId)
    {
        Open((GenericWindows)windowId);
    }

    public void Open(GenericWindows window)
    {
        genericWindows[(int)currentWindow].Close();
        currentWindow = window;
        genericWindows[(int)currentWindow].Open();
    }

    public void OpenPopWindow(int windowId)
    {
        Open((PopWindows)windowId);
    }

    public void Open(PopWindows window)
    {
        popWindows[(int)currentWindow].Open();
    }
}
