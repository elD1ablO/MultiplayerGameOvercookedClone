using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
        
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        ShowTimer(false);
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(GameManager.Instance.GetCoundownToStartTimer()).ToString();
        if (GameManager.Instance.GetCoundownToStartTimer() > 2f)
        {
            countdownText.color = Color.red;
        }
        else if (GameManager.Instance.GetCoundownToStartTimer() <= 2f && GameManager.Instance.GetCoundownToStartTimer() > 1f)
        {
            countdownText.color = Color.yellow;
        }
        else
        {
            countdownText.color = Color.green;
        }
    }
    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            ShowTimer(true);
        }
        else
        {
            ShowTimer(false);
        }
    }
    private void ShowTimer(bool show)
    {        
        gameObject.SetActive(show);        
    }
}
