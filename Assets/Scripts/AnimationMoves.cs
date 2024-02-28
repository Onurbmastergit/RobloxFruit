using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationMoves : MonoBehaviour
{
    public Animator animator;
    

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Animação de Andar
        if (InputController.InputVertical != 0 || InputController.InputHorizontal != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else if(InputController.InputVertical == 0 || InputController.InputHorizontal == 0)
        {
            animator.SetBool("isWalking", false);
        }
        if(InputController.jumpOn == true) 
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isJump", true);
        }
        if (PlayerJump.jumps == 1) 
        {
            animator.SetBool("isJump", true);
        }
        if (PlayerJump.statesJump == StatesJump.Idle) 
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isIdle", false);
        }
       
        
    }
}
