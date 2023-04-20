using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HostDisconnectedUI : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
        ShowCanvas(false);

        mainMenuButton.onClick.AddListener(() =>
        {

            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientID)
    {
        if (clientID == NetworkManager.ServerClientId)
        {
            ShowCanvas(true);
        }
    }

    private void ShowCanvas(bool show)
    {
        gameObject.SetActive(show);
    }
}
