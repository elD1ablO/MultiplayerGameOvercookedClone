using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingForPlayersUI : MonoBehaviour
{
    [SerializeField] private GameObject tutorialUI;
    private void Start()
    {
        GameManager.Instance.OnLocalPlayerReadyChanged += GameManager_OnLocalPlayerReadyChanged;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        ShowWaiting(false);
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            tutorialUI.SetActive(false);
        }
    }

    private void GameManager_OnLocalPlayerReadyChanged(object sender, System.EventArgs e)
    {
       if (GameManager.Instance.IsLocalPlayerReady())
        {
            ShowWaiting(true);
        }
    }

    private void ShowWaiting(bool show)
    {
        gameObject.SetActive(show);
    }
}
