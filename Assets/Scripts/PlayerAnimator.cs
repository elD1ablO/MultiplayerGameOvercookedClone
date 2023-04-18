using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimator : NetworkBehaviour
{
    [SerializeField] private Player player;

    private const string IS_WALKING = "isWalking";
    private const string IS_CARRYING = "isCarrying";
    

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.SetBool(IS_CARRYING, player.IsCarrying());        
    }
}
