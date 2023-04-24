using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterSelectReady : NetworkBehaviour
{
    public event EventHandler OnReadyChanged;
    public static CharacterSelectReady Instance { get; private set; }

    private Dictionary<ulong, bool> playerReadyDictionary;

    private void Awake()
    {
        Instance = this;

        playerReadyDictionary = new Dictionary<ulong, bool>();
    }

    public void SetPlayerReady()
    {
        SetPlayerReadyServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        SetPlayerDeadyClientRpc(serverRpcParams.Receive.SenderClientId);

        playerReadyDictionary[serverRpcParams.Receive.SenderClientId] = true;

        bool allClientsReady = true;
        foreach (ulong clientID in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!playerReadyDictionary.ContainsKey(clientID) || !playerReadyDictionary[clientID])
            {
                allClientsReady = false;
                break;
            }
        }

        if (allClientsReady)
        {
            SceneLoader.LoadNetwork(SceneLoader.Scene.GameScene);
        }
    }

    [ClientRpc]
    private void SetPlayerDeadyClientRpc(ulong clientID)
    {
        playerReadyDictionary[clientID] = true;

        OnReadyChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsPlayerReady(ulong clientID)
    {
        return playerReadyDictionary.ContainsKey(clientID) && playerReadyDictionary[clientID];
    }

}
