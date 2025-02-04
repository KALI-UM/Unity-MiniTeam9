using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuWindow : FocusWindow
{
    [SerializeField]
    private Button windowOpen;

    public Button restartButton;
    public Button lobbyButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(OnClickRestart);
        lobbyButton.onClick.AddListener(OnClickLobby);
    }

    public override void Initialize(UIManager mgr)
    {
        base.Initialize(mgr);
        windowOpen.onClick.AddListener(() => UIManager.Open(FocusWindows.Menu));
    }

    public override void Open()
    {
        base.Open();
        Time.timeScale = 0;
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 1f;
    }

    public void OnClickRestart()
    {
        Close();
    }

    public void OnClickLobby()
    {
        Close();
        SceneManager.LoadScene("Lobby");
    }
}
