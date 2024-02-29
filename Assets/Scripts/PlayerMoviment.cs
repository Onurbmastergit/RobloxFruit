using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    //Cria uma variavel de velocidade tipo float
    public static float speed = 10f;

    //Cria uma variavel de gravidade tipo float 
    public static float gravity = 10f;


    //Cria uma variavel do tipo CharacterController 
    CharacterController cc;

    //metodo que se chama ao inicio do game 
    private void Start()
    {
        //Pega o componente characterController do proprio player caso esse script se encontrar nele
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        //Cria uma variavel tipo float que recebe falor do Input.GetAxis que é uma função da unity que mapeia as teclas e retorna um valor delas caso clicada 
         float direcao_x = InputController.InputHorizontal * speed * Time.deltaTime;
         float direcao_z = InputController.InputVertical * speed * Time.deltaTime;

        //cria uma variavel do tipo float que define uma gravidade simulada para dar uma fisica ao jogo usando a variavel que criamos a cima e TimeDeltaTime para definir que esse valor seja fixo indpendete da pontecia da maquina
         float direcao_y = -gravity * Time.deltaTime;

        //Verifica se o estado esta em pulando 
        if (PlayerJump.statesJump == StatesJump.Jumping)
        {
            //agrega um valor de forma interpolada de acordo com o valor da gravidade começando maior e ficando menor para simular a desaceleração que ocorre no mundo real
            direcao_y = Mathf.SmoothStep(gravity, gravity * 0.40f, PlayerJump.timeJumping / PlayerJump.timeOfJump) * Time.deltaTime;
    
        }
        //verifca se o estado é caindo
        if (PlayerJump.statesJump == StatesJump.Falling)
        {
            //agrega um valor de uma maneira interpolada para simular a queda começando menor e terminando maior para simular a inercia e a desaceleração 
            direcao_y = Mathf.SmoothStep(-gravity * 0.60f, -gravity * 1.5f, PlayerJump.timeJumping / PlayerJump.timeOfJump) * Time.deltaTime;
        }

        //Rotação do Personagem

        //Cria um vector3
        Vector3 front = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        //zera os y do vector 3 no caso o y
        front.y = 0;
        right.y = 0;

        //normaliza e matem o movimento costante sem que haja uma multiplicação da velocidade
        front.Normalize();
        right.Normalize();

        //adiciona os valores correspondente dos inputs que seguem as direções
        front = front * direcao_z;
        right = right * direcao_x;

        //verifica se o player  enviou input 
        if (direcao_x != 0 || direcao_z != 0) 
        {
            //Rotaciona o personagem de forma interpolada , para dar a sensação de suavidade no movimento
            float angle = Mathf.Atan2(front.x + direcao_x, front.z + direcao_z) * Mathf.Rad2Deg;
            Quaternion rotationCam = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationCam, 0.15f);
        }
      

        Vector3 direction_vertical = Vector3.up * direcao_y;
        Vector3 direction_horizontal = front + right;

        //Cria um vetor que guarda 3 valores , no caso estamos usando ele para atualizar a posição do player sem precisar alterar 3 variaves 
        Vector3 moviment = direction_vertical + direction_horizontal;

      

        //Chama a função move responsavel pela movimentação , agregando o moviment para atualizar a movimentação
        cc.Move(moviment);
    }
}
