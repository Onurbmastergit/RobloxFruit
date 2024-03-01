using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationMoves : MonoBehaviour
{
    public Animator animator; // Vamos precisar do componente Animator para controlar as animações.

    void Start()
    {
        animator = GetComponent<Animator>(); // Pegamos o componente Animator associado a este GameObject.
    }

    void Update()
    {
        // Vamos ver se o jogador está se movendo para frente ou para os lados.
        if (InputController.InputVertical != 0 || InputController.InputHorizontal != 0)
        {
            // Se ele estiver se movendo, vamos dizer ao Animator que ele está andando.
            animator.SetBool("isWalking", true);
        }
        else if(InputController.InputVertical == 0 || InputController.InputHorizontal == 0)
        {
            // Se não houver movimento, dizemos ao Animator que ele não está andando.
            animator.SetBool("isWalking", false);
        }
        
        // Vamos verificar se o jogador pressionou o botão de pulo.
        if(InputController.jumpOn == true) 
        {
            // Se ele pulou, vamos dizer ao Animator que ele está parado e pulando.
            animator.SetBool("isIdle", true);
            animator.SetBool("isJump", true);
        }
        
        // Vamos ver se o jogador já deu um pulo.
        if (PlayerJump.jumps == 1) 
        {
            // Se ele pulou uma vez, vamos dizer ao Animator que ele está no ar.
            animator.SetBool("isJump", true);
        }
        
        // Vamos verificar se o jogador está no estado de "Idle" (parado).
        if (PlayerJump.statesJump == StatesJump.Idle) 
        {
            // Se ele estiver parado, vamos dizer ao Animator que ele não está pulando nem parado.
            animator.SetBool("isJump", false);
            animator.SetBool("isIdle", false);
        }
    }
}