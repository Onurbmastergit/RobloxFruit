using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//Definindo os estados de pulo 
public enum StatesJump
{
    Jumping,
    SecondJumping,
    Falling,
    Idle
}
public class PlayerJump : MonoBehaviour
{
    //Criando uma varivel publica dos estados para acessar no codigo
    public static StatesJump statesJump;

    //cria uma variavel publica para defini��o de dura��o do pulo e tempo no ar 
    public static float timeOfJump = 0.60f;
    
    public static float timeJumping = 0f;

    public static int jumps = 0;



    //Tamanho da decte��o de colis�o via raycast que cria um raio que retorna uma colis�o
    public float raycastHead = 5.65f;

    //Cria uma variavel do tipo CharacterController 
    public static CharacterController cc;
    
    //metodo que se chama ao inicio do game 
    void Start()
    {
        //define o estado de pulo para idle que quer dizer que o player se encontra no ch�o
        statesJump = StatesJump.Idle;

        //Pega o componente characterController do proprio player caso esse script se encontrar nele
        cc = GetComponent<CharacterController>();
    }

   
    //E chamado a cada frame 
    void Update()
    {
        if (InputController.InputJump ) 
        {
            jumps++;
            if (jumps == 2)
            {
                timeJumping = -0.5f;
                statesJump = StatesJump.Jumping;
            }
        }
        //uma verifica��o , se o player esta apertando a barra de espa�o e o estado � idle (solo)
        if (InputController.InputJump && statesJump == StatesJump.Idle)
        {
            
            //define o estado para pulando 
            statesJump = StatesJump.Jumping;
        }
        //verifica se estado � pulando 
        if (statesJump == StatesJump.Jumping)
        {
           
                //acresenta valor na variavel de timeJumping para cronometra o tempo do pulo 
                timeJumping += Time.deltaTime;
   
            //verifica se o tempo cronometrado (tempoDecorrido do pulo � maior ou igual ao tempo do pulo)
            if (timeJumping >= timeOfJump )
            {
               //define o estado de pulando para caindo  
               statesJump = StatesJump.Falling;
               //zera o tempo decorrido (Cronometrado)
               timeJumping = 0f;
            }
        }
       

        //Verifica se o Playerr esta caindo
        if (statesJump == StatesJump.Falling) 
        {
            //Acresenta um valor de acordo com a varivel gravidade e multiplica por time.deltatime para manter um valor fixo 
            timeJumping += PlayerMoviment.gravity * Time.deltaTime;
        }
        
        //verifica se o estado � caindo e se o Player tocou no ch�o ou esta em uma superficie (isGrounded � uma proprieda que so pode ser acessada se o player ou outro objeto em cena ter um CharacterController)
        if (statesJump == StatesJump.Falling && cc.isGrounded)
        {
            //zera o tempoDecorrido (Cronomentado)
            timeJumping = 0;
            //define o estado para idle(solo)
            statesJump = StatesJump.Idle;
            jumps = 0;

        }
    }
    //Fun��o que verifica se a coliss�o (especifica para o uso do CharacterController)
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Cria um raio que dectecta coliss�es  // verifica se o raio dectectou algo e se o estado � pulando
        if (Physics.Raycast(transform.position, Vector3.up, raycastHead) && statesJump == StatesJump.Jumping)
        {
            //define o estado para caindo 
            statesJump = StatesJump.Falling;
            //zera o tempo cronomentado 
            timeJumping = 0;
        }
    }
}

