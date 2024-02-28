using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // variavel que define em que estado do ataque o jogador está
    int combo = 0;

    //varivel que define a quantidade total de ataque 
    int comboTotal = 3;

    // varivel que controla se o personagem pode atacar
    bool cantAttack = true;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        animator.SetInteger("comboPunch", combo);

        if (InputController.InputActionPrincipal && cantAttack ==  true) 
            PunchsCombo();
        if (combo == 0) 
            cantAttack = true;
    }
    void PunchsCombo() 
    {
        combo++;

        animator.SetInteger("comboPunch", combo);

        cantAttack = false;

        if (combo >= comboTotal) 
        combo = 0;
        
    }
    void EndCombo() 
    {
        cantAttack = true;

        animator.SetInteger("comboPunch", 0);

    }
    void EnableCollider()
    {
        CombatCollsion.colliderPunch.enabled = true;
    }
    void DisableCollider()
    {
        CombatCollsion.colliderPunch.enabled = false;
    }
}
