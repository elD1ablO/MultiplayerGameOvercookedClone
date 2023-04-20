using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public static event EventHandler OnStartPressed;

    [SerializeField] private Button startButton;
    [SerializeField] private GameObject waitingGameObject;

    public static void ResetStaticData()
    {
        OnStartPressed = null;
    }
    private void Awake()
    {
        ShowTutorial(true);
    }
    private void Start()
    {        
        GameManager.Instance.OnLocalPlayerReadyChanged += GameManager_OnLocalPlayerReadyChanged;

        startButton.onClick.AddListener(() =>
        {
            
            OnStartPressed?.Invoke(this, EventArgs.Empty);
            
        });
    }

    private void GameManager_OnLocalPlayerReadyChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsLocalPlayerReady())
        {
            startButton.gameObject.SetActive(false);
            waitingGameObject.SetActive(true);
        }
    }

    private void ShowTutorial(bool showTutorial)
    {
        gameObject.SetActive(showTutorial);
    }
}
