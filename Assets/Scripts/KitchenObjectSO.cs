using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public string objectName;
    public Sprite iconSprite;    
    public Transform prefab;
}
