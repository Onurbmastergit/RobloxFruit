using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int vidaTotal = 5;
    public int vidaAtual;
    bool die = false;
    Animator animator;

    private void Start()
    {
        vidaAtual = vidaTotal;
    }
    public void Update()
    {
        if (die == true) 
        {
            animator.SetBool("die", true);
        }
    }
    public void ReceberDano(int valor) 
    {
        vidaAtual -= valor;
        VerificaMorte();
    }
    void VerificaMorte() 
    {
        if (vidaAtual == 0) 
        {
           die = true;
        }
    }
}
