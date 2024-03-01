using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Variáveis para armazenar os valores dos inputs do jogador
    public static float InputHorizontal = 0; // Valor do input horizontal (A/D ou Setas esquerda/direita)
    public static float InputVertical = 0; // Valor do input vertical (W/S ou Setas cima/baixo)
    public static bool InputActionPrincipal = false; // Indica se o botão principal de ação foi pressionado (Botão esquerdo do mouse)
    public static bool InputActionSecundary = false; // Indica se o botão secundário de ação está sendo mantido pressionado (Botão direito do mouse)
    public static bool InputActionHold = false; // Indica se o botão de ação está sendo mantido pressionado (Shift esquerdo)
    
    // Variáveis para o input de pulo
    public static bool InputJump = false; // Indica se o botão de pulo foi pressionado (Barra de espaço)
    public static bool jumpOn = false; // Indica se o jogador está pulando atualmente

    // Função chamada a cada frame
    void Update()
    {
        // Obtém os valores dos inputs horizontal e vertical
        InputHorizontal = Input.GetAxis("Horizontal");
        InputVertical = Input.GetAxis("Vertical");
        
        // Verifica se o botão principal de ação foi pressionado
        InputActionPrincipal = Input.GetKeyDown(KeyCode.Mouse0);
        
        // Verifica se o botão secundário de ação está sendo mantido pressionado
        InputActionSecundary = Input.GetKey(KeyCode.Mouse1);
        
        // Verifica se o botão de ação foi pressionado
        InputActionHold = Input.GetKeyDown(KeyCode.LeftShift);

        // Verifica se o botão de pulo foi pressionado
        InputJump = Input.GetKeyDown(KeyCode.Space);
        
        // Se o botão de pulo foi pressionado, atualiza a variável de pulo
        if (InputJump)
        {
            jumpOn = true;
        }
    }
}