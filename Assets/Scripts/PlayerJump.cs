using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Definindo os estados de pulo
public enum StatesJump
{
    Jumping,
    SecondJumping,
    Falling,
    Idle
}
public class PlayerJump : MonoBehaviour
{
    // Criando uma variável pública dos estados para acessar no código
    public static StatesJump statesJump;

    // Cria uma variável pública para definição de duração do pulo e tempo no ar 
    public static float timeOfJump = 0.60f;
    public static float timeJumping = 0f;

    // Variável para contar o número de pulos realizados
    public static int jumps = 0;

    // Tamanho da detecção de colisão via raycast que cria um raio que retorna uma colisão
    public float raycastHead = 5.65f;

    // Cria uma variável do tipo CharacterController 
    public static CharacterController cc;

    // Método que é chamado ao início do jogo 
    void Start()
    {
        // Define o estado de pulo para idle que quer dizer que o player se encontra no chão
        statesJump = StatesJump.Idle;

        // Pega o componente CharacterController do próprio player caso esse script se encontrar nele
        cc = GetComponent<CharacterController>();
    }

    // É chamado a cada frame 
    void Update()
    {
        // Verifica se o jogador está pressionando o botão de pulo
        if (InputController.InputJump ) 
        {
            // Incrementa o contador de pulos
            jumps++;
            // Se já foi feito um segundo pulo, inicia o pulo
            if (jumps == 2)
            {
                timeJumping = -0.5f;
                statesJump = StatesJump.Jumping;
            }
        }
        // Verifica se o jogador está pressionando o botão de pulo e se o estado é idle (solo)
        if (InputController.InputJump && statesJump == StatesJump.Idle)
        {
            // Define o estado para pulando 
            statesJump = StatesJump.Jumping;
        }
        // Verifica se o estado é pulando 
        if (statesJump == StatesJump.Jumping)
        {
            // Acrescenta valor na variável de timeJumping para cronometrar o tempo do pulo 
            timeJumping += Time.deltaTime;

            // Verifica se o tempo cronometrado (tempoDecorrido do pulo é maior ou igual ao tempo do pulo)
            if (timeJumping >= timeOfJump )
            {
                // Define o estado de pulando para caindo  
                statesJump = StatesJump.Falling;
                // Zera o tempo decorrido (Cronometrado)
                timeJumping = 0f;
            }
        }

        // Verifica se o jogador está caindo
        if (statesJump == StatesJump.Falling) 
        {
            // Acrescenta um valor de acordo com a variável gravidade e multiplica por Time.deltaTime para manter um valor fixo 
            timeJumping += PlayerMoviment.gravity * Time.deltaTime;
        }
        
        // Verifica se o estado é caindo e se o Player tocou no chão ou está em uma superfície (isGrounded é uma propriedade que só pode ser acessada se o player ou outro objeto em cena tiver um CharacterController)
        if (statesJump == StatesJump.Falling && cc.isGrounded)
        {
            // Zera o tempoDecorrido (Cronometrado)
            timeJumping = 0;
            // Define o estado para idle (solo)
            statesJump = StatesJump.Idle;
            jumps = 0;
        }
    }

    // Função que verifica se a colisão (específica para o uso do CharacterController)
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Cria um raio que detecta colisões  // Verifica se o raio detectou algo e se o estado é pulando
        if (Physics.Raycast(transform.position, Vector3.up, raycastHead) && statesJump == StatesJump.Jumping)
        {
            // Define o estado para caindo 
            statesJump = StatesJump.Falling;
            // Zera o tempo cronometrado 
            timeJumping = 0;
        }
    }
}