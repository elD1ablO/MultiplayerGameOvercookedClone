using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer playerMeshRenderer;
        
    public void SetPlayerMaterial(Material selectedMaterial)
    {
        playerMeshRenderer.material = selectedMaterial;
    }
}
