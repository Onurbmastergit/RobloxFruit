using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    float mouseX;
    float mouseY;
    public float sensbilty = 2.22f;
    public static bool shiftLock = false;


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

   
    void  Update()
    {
        if (InputController.InputActionHold) 
        {
            shiftLock = !shiftLock;
        }

        transform.position = player.position - new Vector3(0, -1, 0);

        if (shiftLock == true) 
        {
            TravarCursor();
            Cursor.visible = false;
            mouseX += Input.GetAxis("Mouse X") * sensbilty;
            mouseY += Input.GetAxis("Mouse Y") * sensbilty;

            mouseY = Mathf.Clamp(mouseY, -90f, 90f);
        }
        else if (shiftLock == false) 
        {
            DestravarCursor();
            Cursor.visible = true;
        }
        if (InputController.InputActionSecundary)
        {
            TravarCursor();
            mouseX += Input.GetAxis("Mouse X") * sensbilty;
            mouseY += Input.GetAxis("Mouse Y") * sensbilty;

            mouseY = Mathf.Clamp(mouseY, -90f, 90f);
            
        } 
        else
        {
            DestravarCursor();
        }

 
        transform.rotation = Quaternion.Euler( -mouseY , mouseX , 0);

    }
    void TravarCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void DestravarCursor() 
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
