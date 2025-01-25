using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : InGameManager
{
    public FocusWindow[] focusWindows;
    public PopWindow[] popWindows;

    public FocusWindows currentWindow { get; private set; }

    private void Start()
    {
        foreach (var window in focusWindows)
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
        Open((FocusWindows)windowId);
    }

    public void Open(FocusWindows window)
    {
        focusWindows[(int)currentWindow].Close();
        currentWindow = window;
        focusWindows[(int)currentWindow].Open();
    }

    public void OpenPopWindow(int windowId)
    {
        Open((PopWindows)windowId);
    }

    public void Open(PopWindows window)
    {
        popWindows[(int)window].Open();
    }

    public void CloseAllPopWindow()
    {
        foreach(var window in popWindows)
        {
            window.Close();
        }
    }
}
