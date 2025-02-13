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

    private void Start()
    {
        var audioData = Resources.Load<AudioClipPackData>("Datas/LobbyAudioPackData");
        SoundManager.Instance.SetAudioClipPack(audioData);
        SoundManager.Instance.PlayBGM("Bgm_01_Lobby01");
    }
}
