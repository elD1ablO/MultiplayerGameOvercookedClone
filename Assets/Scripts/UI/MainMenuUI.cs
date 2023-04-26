using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private Button playSingleplayerButton;
    [SerializeField] private Button playMultiplayerButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playSingleplayerButton.onClick.AddListener(() =>
        {
            KitchenGameMultiplayer.playMultiplayer = false;
            SceneLoader.LoadScene(SceneLoader.Scene.LobbyScene);
        });

        playMultiplayerButton.onClick.AddListener(() =>
        {
            KitchenGameMultiplayer.playMultiplayer = true;
            SceneLoader.LoadScene(SceneLoader.Scene.LobbyScene);
        });
        
        quitButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); 
#endif        
        });
    }
}
