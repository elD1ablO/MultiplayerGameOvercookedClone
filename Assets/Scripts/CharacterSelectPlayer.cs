using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPlayer : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject readyGameText;
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private Button kickButton;

    private void Awake()
    {
        kickButton.onClick.AddListener(() =>
        {
            PlayerData playerData = KitchenGameMultiplayer.Instance.GetPlayerDataFromIndex(playerIndex);
            KitchenGameMultiplayer.Instance.KickPlayer(playerData.clientID);
        });
    }

    private void Start()
    {
        KitchenGameMultiplayer.Instance.OnPlayerDataNetworkListChanged += KitchenGameMultiplayer_OnPlayerDataNetworkListChanged;
        CharacterSelectReady.Instance.OnReadyChanged += CharacterSelectReady_OnReadyChanged;

        kickButton.gameObject.SetActive(NetworkManager.Singleton.IsServer);

        UpdatePlayer();
    }

    private void CharacterSelectReady_OnReadyChanged(object sender, EventArgs e)
    {
        UpdatePlayer();
    }

    private void KitchenGameMultiplayer_OnPlayerDataNetworkListChanged(object sender, EventArgs e)
    {
        UpdatePlayer();
    }

    private void UpdatePlayer()
    {
        if (KitchenGameMultiplayer.Instance.IsPlayerIndexConnected(playerIndex))
        {
            ShowPlayerPrefab(true);

            PlayerData playerData = KitchenGameMultiplayer.Instance.GetPlayerDataFromIndex(playerIndex);

            readyGameText.SetActive(CharacterSelectReady.Instance.IsPlayerReady(playerData.clientID));

            playerVisual.SetPlayerMaterial(KitchenGameMultiplayer.Instance.GetPlayerMaterial(playerData.materialID));
        }
        else
        {
            ShowPlayerPrefab(false);
        }
    }

    private void ShowPlayerPrefab(bool show)
    {
        gameObject.SetActive(show);
    }

    private void OnDestroy()
    {
        KitchenGameMultiplayer.Instance.OnPlayerDataNetworkListChanged -= KitchenGameMultiplayer_OnPlayerDataNetworkListChanged;
    }
}
