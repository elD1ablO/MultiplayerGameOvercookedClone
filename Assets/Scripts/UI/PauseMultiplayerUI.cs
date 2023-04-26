using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMultiplayerUI : MonoBehaviour
{


    private void Start()
    {
        GameManager.Instance.OnMultiplayerGamePaused += GameManager_OnMultiplayerGamePaused;
        GameManager.Instance.OnMultiplayerGameUnpaused += GameManager_OnMultiplayerGameUnpaused;

        ShowPauseCanvas(false);
    }

    private void GameManager_OnMultiplayerGamePaused(object sender, EventArgs e)
    {
        ShowPauseCanvas(true);
    }
        

    private void GameManager_OnMultiplayerGameUnpaused(object sender, EventArgs e)
    {
        ShowPauseCanvas(false);
    }

    private void ShowPauseCanvas(bool show)
    {
        gameObject.SetActive(show);
    }
}
