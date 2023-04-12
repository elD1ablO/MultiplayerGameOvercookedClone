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

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
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
