using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyButtons
    : MonoBehaviour
{
    public Button startTutorialButton;
    public Button startGameButton;

    private void Awake()
    {
        startGameButton.onClick.AddListener(() => SceneManager.LoadScene("InGame"));
        //startTutorialButton.onClick.AddListener(() => SceneManager.LoadScene("Tutorial"));
    }
}
