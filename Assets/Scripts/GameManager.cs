using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;

    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private bool isGamePaused = false;

    [SerializeField] private float gamePlayingTimerMax = 120f;

    
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInputHandler.Instance.OnPauseAction += GameInputHandler_OnPauseAction;
    }    

    private void Update()
    {
        switch (state)
        {

            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
        Debug.Log(state);
    }

    private void GameInputHandler_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCoundownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {            
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {            
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }        
    }
}
