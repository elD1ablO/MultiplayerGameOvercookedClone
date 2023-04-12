using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveyResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    [Header("Image References")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [Header("Colors")]
    [SerializeField] private Color successColor;
    [SerializeField] private Color failColor;
    [Header("Sprites")]
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnOrderCompleted += DeliveryManager_OnOrderCompleted;
        DeliveryManager.Instance.OnOrderFailed += DeliveryManager_OnOrderFailed;
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnOrderFailed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = failColor; 
        iconImage.sprite = failSprite;
        messageText.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_OnOrderCompleted(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
    }
}
