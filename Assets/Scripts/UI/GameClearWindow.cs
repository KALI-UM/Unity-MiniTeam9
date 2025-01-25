using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearWindow : FocusWindow
{
    public Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(OnClickRestart);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
