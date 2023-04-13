using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    [SerializeField] private TextMeshProUGUI recipeCompletedText;

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });        
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        ShowGameOverCanvas(false);
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            ShowGameOverCanvas(true);
            recipeCompletedText.text = DeliveryManager.Instance.GetCompletedOrders().ToString();
        }
        else
        {
            ShowGameOverCanvas(false);
        }
    }
    private void ShowGameOverCanvas(bool show)
    {        
        gameObject.SetActive(show);        
    }

}
