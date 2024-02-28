using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //Cria uma variavel do tipo float que guardara valor do InputGetAxis na Horizontal e Vertical 
    public static float InputHorizontal = 0;
    public static float InputVertical = 0;
    public static bool InputActionPrincipal = false;
    public static bool InputActionSecundary = false;
    public static bool InputActionHold = false;

    //Cria uma Variavel do tipo bolleana para guarda se o player esta apertando o input de pulo
    public static bool InputJump = false;


    public static bool jumpOn = false;

    //second Jump
    

    //Funçao que é chamada a cada frame
    void Update()
    {
        //Guarda o valor Horizontal do inputGetaxis que mapeia e retorna as teclas mapeadas como valor 
        InputHorizontal = Input.GetAxis("Horizontal");
        InputVertical = Input.GetAxis("Vertical");
        InputActionPrincipal = Input.GetKeyDown(KeyCode.Mouse0);
        InputActionSecundary = Input.GetKey(KeyCode.Mouse1);
        InputActionHold = Input.GetKeyDown(KeyCode.LeftShift);

        //Altera o valor da variavel bool , verificando se o player esta apertando espaço se sim a varivel recebe true se não false
        InputJump = Input.GetKeyDown(KeyCode.Space);
        if (InputJump)
        {
            jumpOn = true;
        }
    }
}
