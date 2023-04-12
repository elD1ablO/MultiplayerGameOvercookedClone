using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;


    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        
        mainMenuButton.onClick.AddListener(() =>
        {            
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        ShowPauseMenu(false);
    }
        

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        ShowPauseMenu(true);
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        ShowPauseMenu(false);
    }

    private void ShowPauseMenu(bool show)
    {
        gameObject.SetActive(show);
    }
}
