using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button startGameButton;

    private void Awake()
    {
        startGameButton.onClick.AddListener(() => SceneManager.LoadScene("InGame"));
    }
}
