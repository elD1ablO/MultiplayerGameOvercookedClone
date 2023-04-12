using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene
    }
    private static Scene targetScene;

    public static void LoadScene(Scene targetScene)
    {
        SceneLoader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());        
    }

    public static void LoadGameScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }

}

