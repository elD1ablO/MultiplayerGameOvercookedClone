using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionResponseMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(()=>
        {
            ShowCanvas(false);
        });
    }

    private void Start()
    {
        KitchenGameMultiplayer.Instance.OnFailToJoinGame += KitchenGameMultiplayer_OnFailToJoinGame;

        ShowCanvas(false);
    }

    private void KitchenGameMultiplayer_OnFailToJoinGame(object sender, System.EventArgs e)
    {
        ShowCanvas(true);
        messageText.text = NetworkManager.Singleton.DisconnectReason.ToString();

        if (messageText.text == "")
        {
            messageText.text = "Failed to connect";
        }
    }

    private void ShowCanvas(bool show)
    {
        gameObject.SetActive(show);
    }

    private void OnDestroy()
    {
        KitchenGameMultiplayer.Instance.OnFailToJoinGame -= KitchenGameMultiplayer_OnFailToJoinGame;
    }
}
