using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Variável que define em que estado do combo de ataque o jogador está
    int combo = 0;

    // Variável que define a quantidade total de ataques no combo
    int comboTotal = 3;

    // Variável que controla se o personagem pode atacar
    bool cantAttack = true;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtém o componente Animator do jogador
    }

    
    void Update()
    {
        animator.SetInteger("comboPunch", combo); // Define o parâmetro "comboPunch" do Animator com o valor do combo atual

        // Verifica se o botão de ataque principal foi pressionado e se o jogador pode atacar
        if (InputController.InputActionPrincipal && cantAttack == true) 
            PunchsCombo(); // Executa o combo de ataques
        
        // Se o combo estiver em seu estado inicial (0), o jogador pode atacar novamente
        if (combo == 0) 
            cantAttack = true;
    }

    // Função para executar o combo de ataques
    void PunchsCombo() 
    {
        combo++; // Incrementa o combo atual

        animator.SetInteger("comboPunch", combo); // Atualiza o parâmetro "comboPunch" do Animator

        cantAttack = false; // Impede o jogador de atacar novamente até o final do combo

        // Se o combo atingiu o limite total, reinicia-o
        if (combo >= comboTotal) 
            combo = 0;
    }

    // Função chamada no final do combo para permitir que o jogador ataque novamente
    void EndCombo() 
    {
        cantAttack = true; // Permite que o jogador ataque novamente

        animator.SetInteger("comboPunch", 0); // Reseta o parâmetro "comboPunch" do Animator
    }

    // Função para habilitar o colisor do ataque
    void EnableCollider()
    {
        CombatCollsion.colliderPunch.enabled = true; // Habilita o colisor do ataque
    }

    // Função para desabilitar o colisor do ataque
    void DisableCollider()
    {
        CombatCollsion.colliderPunch.enabled = false; // Desabilita o colisor do ataque
    }
}