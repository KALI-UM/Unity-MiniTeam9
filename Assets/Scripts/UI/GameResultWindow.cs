using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultWindow : FocusWindow
{
    public Button restartButton;
    public Button lobbyButton;
    public Image title;

    //[SerializeField]
    //private Sprite clearSprite;
    //[SerializeField]
    //private Sprite overSprite;

    private Sequence resultWindowAnimation;

    private void Awake()
    {
        restartButton.onClick.AddListener(OnClickRestart);
        lobbyButton.onClick.AddListener(OnClickLobby);
    }

    private void Start()
    {
        resultWindowAnimation = DOTween.Sequence().SetAutoKill(false);
        float initialSize = title.rectTransform.sizeDelta.x;
        resultWindowAnimation.Append(title.rectTransform.DOSizeDelta(new Vector2(initialSize + 500f, title.rectTransform.sizeDelta.y), 2f));
    }

    public override void Open()
    {
        base.Open();
        resultWindowAnimation.Restart();
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
