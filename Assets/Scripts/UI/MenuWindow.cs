using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuWindow : UIElement
{
    [SerializeField]
    private Button windowOpen;

    public Button exitButton;
    public Button pauseButton;
    public Button restartButton;
    public Button lobbyButton;

    private void Awake()
    {
        windowOpen.onClick.AddListener(SetOpen);
        exitButton.onClick.AddListener(Reset);
        pauseButton.onClick.AddListener(OnClickPause);
        restartButton.onClick.AddListener(OnClickRestart);
        lobbyButton.onClick.AddListener(OnClickLobby);

        Reset();
    }

    private void Reset()
    {
        windowOpen.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        lobbyButton.gameObject.SetActive(false);
    }

    private void SetOpen()
    {
        windowOpen.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        lobbyButton.gameObject.SetActive(true);
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        restartButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    public void OnClickPause()
    {
        Time.timeScale = 0;
        restartButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
