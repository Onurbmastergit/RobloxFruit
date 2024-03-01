using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDetection : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        // Verifica se o objeto colidido é o jogador
        if (other.gameObject.CompareTag("Player"))
        {
            // Define a variável 'cantAttack' do controlador do inimigo como verdadeira
            // Isso indica que o inimigo não pode atacar enquanto estiver detectando o jogador
            transform.parent.GetComponent<EnemyController>().cantAttack = true;
        }
    }
}