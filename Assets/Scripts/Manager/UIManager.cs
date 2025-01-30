using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static WaveTable;

public class UIManager : InGameManager
{
    public Button screenArea;

    public FocusWindow[] focusWindows;
    public PopWindow[] popWindows;
    public UIElement[] uiElements;


    public FocusWindows currentWindow
    {
        get;
        private set;
    }


    public override void Initialize(GameManager gm)
    {
        base.Initialize(gm);

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

        foreach (var ui in uiElements)
        {
            ui.Initialize(this);
        }

        GameManager.onWaveStart += (WaveData data) =>
        {
            Open(PopWindows.Wave);

            foreach (var ui in focusWindows)
            {
                var currUI = ui;
                currUI.SendMessage("OnWaveStart", data, SendMessageOptions.DontRequireReceiver);
            }

            foreach (var ui in popWindows)
            {
                var currUI = ui;
                currUI.SendMessage("OnWaveStart", data, SendMessageOptions.DontRequireReceiver);
            }

            foreach (var ui in uiElements)
            {
                var currUI = ui;
                currUI.SendMessage("OnWaveStart", data, SendMessageOptions.DontRequireReceiver);
            }
        };

        GameManager.onGameOver += () => Open(FocusWindows.GameOver);
        GameManager.onGameClear += () => Open(FocusWindows.GameClear);
    }

    private void Awake()
    {
        screenArea.onClick.AddListener(()=> OnClickNotUIArea());
    }

    public void OpenFocusWindow(int windowId)
    {
        Open((FocusWindows)windowId);
    }

    public void Open(FocusWindows window)
    {
        focusWindows[(int)currentWindow].OnOutFocus();

        currentWindow = window;
        //[(int)currentWindow].OnFocus();
        focusWindows[(int)currentWindow].Open();
    }

    public void Close(FocusWindows window)
    {
        focusWindows[(int)window].Close();
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
        foreach (var window in popWindows)
        {
            window.Close();
        }
    }

    public void OnClickNotUIArea()
    {
        KALLogger.Log("UI focus ¿“¿Ω");
        //foreach (var window in focusWindows)
        //{
        //    window.OnOutFocus();
        //}
    }
}
