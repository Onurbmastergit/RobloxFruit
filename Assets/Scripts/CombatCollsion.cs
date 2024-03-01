using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCollsion : MonoBehaviour
{
    public static Collider colliderPunch; // Referência para o Collider usado para ataques

    private void Start()
    {
        colliderPunch = GetComponent<Collider>(); // Obtém o Collider deste objeto
    }

    private void OnTriggerEnter(Collider collider)
    {
        // Verifica se o objeto atingido é um inimigo
        if (collider.GetComponent<EnemyController>() != null) 
        {
            Debug.Log("Tomou dano"); // Exibe uma mensagem de debug indicando que o inimigo foi atingido
            collider.GetComponent<EnemyController>().Damage(1); // Chama o método de causar dano no inimigo
        }
        // Verifica se o objeto atingido é o jogador
        if (collider.GetComponent<PlayerManager>() !=null) 
        {
            collider.GetComponent<PlayerManager>().ReceberDano(1); // Chama o método de causar dano no jogador
        }
    }
}