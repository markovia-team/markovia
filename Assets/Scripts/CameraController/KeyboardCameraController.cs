using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardCameraController : MonoBehaviour
{

    // TODO: Cambiar las cosas por LERP 
    [SerializeField] private Camera cam; 
    void Update()
    {
        // Movement
        var onGroundOld = Physics.Raycast(transform.position, Vector3.down, out var hit);
        var oldHeight = hit.point.y;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward, Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right, Space.World);
        }
        
        var onGroundNew = Physics.Raycast(transform.position, Vector3.down, out hit);
        var newHeight = hit.point.y;

        transform.Translate(Vector3.up * (newHeight - oldHeight), Space.World);
        
        // Rotation 
        // TODO, quizas girar con respecto a un pivote? 
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, -1f, 0f, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {            
            transform.Rotate(0f, 1f, 0f, Space.World);
        }
        
        // Zoom 
        // TODO, quizas girar con respecto a un pivote? 
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Translate(cam.transform.forward,  Space.Self);
        }
        if (Input.GetKey(KeyCode.X))
        {            
            transform.Translate(-cam.transform.forward,  Space.Self);
        }
        
    }
    
    
    
}
