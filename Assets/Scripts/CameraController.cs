using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player; // Referência para o transform do jogador
    float mouseX; // Armazena a posição do mouse no eixo X
    float mouseY; // Armazena a posição do mouse no eixo Y
    public float sensbilty = 2.22f; // Sensibilidade do mouse
    public static bool shiftLock = false; // Indica se o cursor está travado ou não

    void Start()
    {
        // Encontrar o GameObject com a tag "Player" e obter seu transform
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        // Verificar se a tecla de ação está sendo pressionada
        if (InputController.InputActionHold) 
        {
            // Inverte o estado de shiftLock quando a tecla de ação é pressionada
            shiftLock = !shiftLock;
        }

        // Atualizar a posição da câmera para seguir o jogador, mantendo-se à mesma altura
        transform.position = player.position - new Vector3(0, -1, 0);

        // Verificar se o cursor está travado
        if (shiftLock == true) 
        {
            // Travar o cursor e esconder
            TravarCursor();
            Cursor.visible = false;

            // Atualizar as rotações da câmera com base nos movimentos do mouse
            mouseX += Input.GetAxis("Mouse X") * sensbilty;
            mouseY += Input.GetAxis("Mouse Y") * sensbilty;

            // Limitar a rotação do mouse para evitar que a câmera faça loop
            mouseY = Mathf.Clamp(mouseY, -90f, 90f);
        }
        else if (shiftLock == false) 
        {
            // Destravar o cursor e torná-lo visível
            DestravarCursor();
            Cursor.visible = true;
        }

        // Verificar se a tecla de ação secundária está sendo pressionada
        if (InputController.InputActionSecundary)
        {
            // Travar o cursor enquanto a tecla de ação secundária é pressionada
            TravarCursor();

            // Atualizar as rotações da câmera com base nos movimentos do mouse
            mouseX += Input.GetAxis("Mouse X") * sensbilty;
            mouseY += Input.GetAxis("Mouse Y") * sensbilty;

            // Limitar a rotação do mouse para evitar que a câmera faça loop
            mouseY = Mathf.Clamp(mouseY, -90f, 90f);
        } 
        else
        {
            // Destravar o cursor quando a tecla de ação secundária não estiver sendo pressionada
            DestravarCursor();
        }

        // Aplicar as rotações da câmera
        transform.rotation = Quaternion.Euler( -mouseY , mouseX , 0);

    }

    // Método para travar o cursor
    void TravarCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Método para destravar o cursor
    void DestravarCursor() 
    {
        Cursor.lockState = CursorLockMode.None;
    }
}