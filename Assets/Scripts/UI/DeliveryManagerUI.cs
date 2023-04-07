using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform orderTemplate;

    private void Awake()
    {
        orderTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnOrderSpawned += DeliveryManager_OnOrderSpawned;
        DeliveryManager.Instance.OnOrderCompleted += DeliveryManager_OnOrderCompleted;

        UpdateVisual();
    }

    

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == orderTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(orderTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetOrderSO(recipeSO);
        }
    }

    private void DeliveryManager_OnOrderCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnOrderSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }
}
