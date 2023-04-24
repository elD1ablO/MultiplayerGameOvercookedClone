using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingUI : MonoBehaviour
{

    private void Start()
    {
        KitchenGameMultiplayer.Instance.OnTryToJoinGame += KitchenGameMultiplayer_OnTryToJoinGame;
        KitchenGameMultiplayer.Instance.OnFailToJoinGame += KitchenGameMultiplayer_OnFailToJoinGame;
        ShowCanvas(false);
    }

    private void KitchenGameMultiplayer_OnFailToJoinGame(object sender, System.EventArgs e)
    {
        ShowCanvas(false);
    }

    private void KitchenGameMultiplayer_OnTryToJoinGame(object sender, System.EventArgs e)
    {
        ShowCanvas(true);
    }


    private void ShowCanvas(bool show)
    {
        gameObject.SetActive(show);
    }

    private void OnDestroy()
    {
        KitchenGameMultiplayer.Instance.OnTryToJoinGame -= KitchenGameMultiplayer_OnTryToJoinGame;
        KitchenGameMultiplayer.Instance.OnFailToJoinGame -= KitchenGameMultiplayer_OnFailToJoinGame;
    }
}
