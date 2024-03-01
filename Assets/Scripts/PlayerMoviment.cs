using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    // Cria uma variável de velocidade tipo float
    public static float speed = 10f;

    // Cria uma variável de gravidade tipo float 
    public static float gravity = 10f;

    // Cria uma variável do tipo CharacterController 
    CharacterController cc;

    // Método que é chamado ao iniciar o jogo 
    private void Start()
    {
        // Pega o componente CharacterController do próprio player caso esse script se encontrar nele
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Cria uma variável tipo float que recebe valor do Input.GetAxis que é uma função da Unity que mapeia as teclas e retorna um valor delas caso clicadas 
        float direcao_x = InputController.InputHorizontal * speed * Time.deltaTime;
        float direcao_z = InputController.InputVertical * speed * Time.deltaTime;

        // Cria uma variável do tipo float que define uma gravidade simulada para dar uma física ao jogo usando a variável que criamos acima e Time.deltaTime para definir que esse valor seja fixo independente da potência da máquina
        float direcao_y = -gravity * Time.deltaTime;

        // Verifica se o estado está em pulando 
        if (PlayerJump.statesJump == StatesJump.Jumping)
        {
            // Agrega um valor de forma interpolada de acordo com o valor da gravidade começando maior e ficando menor para simular a desaceleração que ocorre no mundo real
            direcao_y = Mathf.SmoothStep(gravity, gravity * 0.40f, PlayerJump.timeJumping / PlayerJump.timeOfJump) * Time.deltaTime;
        }
        // Verifica se o estado é caindo
        if (PlayerJump.statesJump == StatesJump.Falling)
        {
            // Agrega um valor de uma maneira interpolada para simular a queda começando menor e terminando maior para simular a inércia e a desaceleração 
            direcao_y = Mathf.SmoothStep(-gravity * 0.60f, -gravity * 1.5f, PlayerJump.timeJumping / PlayerJump.timeOfJump) * Time.deltaTime;
        }

        // Rotação do Personagem

        // Cria um Vector3
        Vector3 front = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Zera os y do Vector3, no caso o y
        front.y = 0;
        right.y = 0;

        // Normaliza e mantém o movimento constante sem que haja uma multiplicação da velocidade
        front.Normalize();
        right.Normalize();

        // Adiciona os valores correspondentes dos inputs que seguem as direções
        front = front * direcao_z;
        right = right * direcao_x;

        // Verifica se o player enviou input 
        if (direcao_x != 0 || direcao_z != 0) 
        {
            // Rotaciona o personagem de forma interpolada, para dar a sensação de suavidade no movimento
            float angle = Mathf.Atan2(front.x + direcao_x, front.z + direcao_z) * Mathf.Rad2Deg;
            Quaternion rotationCam = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationCam, 0.15f);
        }

        Vector3 direction_vertical = Vector3.up * direcao_y;
        Vector3 direction_horizontal = front + right;

        // Cria um vetor que guarda 3 valores, no caso estamos usando ele para atualizar a posição do player sem precisar alterar 3 variáveis 
        Vector3 moviment = direction_vertical + direction_horizontal;

        // Chama a função Move responsável pela movimentação, agregando o movimento para atualizar a movimentação
        cc.Move(moviment);
    }
}
