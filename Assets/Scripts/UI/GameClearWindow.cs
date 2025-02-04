using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearWindow : FocusWindow
{
    public Button restartButton;
    public Button lobbyButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(OnClickRestart);
        lobbyButton.onClick.AddListener(OnClickLobby);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
