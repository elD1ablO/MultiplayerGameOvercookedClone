using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
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
        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.SetBool(IS_CARRYING, player.IsCarrying());        
    }
}
