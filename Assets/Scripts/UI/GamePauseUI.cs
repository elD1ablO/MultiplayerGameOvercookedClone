using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button resumeButton;


    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        
        mainMenuButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
         
        optionsButton.onClick.AddListener(() =>
        {
            
        });
    }

    private void Start()
    {
        GameManager.Instance.OnLocalGamePaused += GameManager_OnLocalGamePaused;
        GameManager.Instance.OnLocalGameUnpaused += GameManager_OnLocalGameUnpaused;

        ShowPauseMenu(false);
    }
        

    private void GameManager_OnLocalGamePaused(object sender, EventArgs e)
    {
        ShowPauseMenu(true);
    }

    private void GameManager_OnLocalGameUnpaused(object sender, EventArgs e)
    {
        ShowPauseMenu(false);
    }

    private void ShowPauseMenu(bool show)
    {
        gameObject.SetActive(show);
    }
}
