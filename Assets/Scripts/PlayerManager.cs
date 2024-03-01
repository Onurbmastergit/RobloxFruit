using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int vidaTotal = 5; // Vida total do jogador
    public int vidaAtual; // Vida atual do jogador
    bool die = false; // Indica se o jogador morreu
    Animator animator; // Referência ao componente Animator do jogador

    private void Start()
    {
        vidaAtual = vidaTotal; // Inicializa a vida atual com a vida total no início do jogo
    }

    public void Update()
    {
        // Se o jogador morreu, ativa a animação de morte
        if (die == true) 
        {
            animator.SetBool("die", true);
        }
    }

    // Função chamada para aplicar dano ao jogador
    public void ReceberDano(int valor) 
    {
        vidaAtual -= valor; // Reduz a vida do jogador
        VerificaMorte(); // Verifica se o jogador morreu após receber dano
    }

    // Função para verificar se o jogador morreu
    void VerificaMorte() 
    {
        if (vidaAtual == 0) 
        {
            die = true; // Indica que o jogador morreu
        }
    }
}