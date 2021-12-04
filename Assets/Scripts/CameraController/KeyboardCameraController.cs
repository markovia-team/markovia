using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardCameraController : MonoBehaviour
{
    private float maxZoomDistance = 5f;
    private float minZoomDistance = 60f; 
    
    private float maxZAbs = 50f; 
    private float maxXAbs = 40f; 


    // TODO: Cambiar las cosas por LERP 
    [SerializeField] private Camera cam; 
    void Update()
    {
        // Movement
        var onGroundOld = Physics.Raycast(transform.position, Vector3.down, out var hit);
        var oldHeight = hit.point.y;

        if (Input.GetKey(KeyCode.W) && transform.position.z < maxZAbs)
        {
            transform.Translate(Vector3.forward, Space.Self);
        }
        if (Input.GetKey(KeyCode.S) && transform.position.z > -maxZAbs)
        {
            transform.Translate(Vector3.back, Space.World);
        }
        if (Input.GetKey(KeyCode.A) && transform.position.x > -maxXAbs)
        {
            transform.Translate(Vector3.left, Space.World);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < maxXAbs)
        {
            transform.Translate(Vector3.right, Space.World);
        }
        
        var onGroundNew = Physics.Raycast(transform.position, Vector3.down, out hit);
        var newHeight = hit.point.y;

        var deltaHeight = newHeight - oldHeight; 
        transform.Translate(Vector3.up * deltaHeight, Space.World);
        
       
        // Zoom 
        if (Input.GetKey(KeyCode.Z))
        {
            Physics.Raycast(transform.position, cam.transform.forward, out hit);
            if (Vector3.Distance(hit.point, transform.position) > maxZoomDistance)
                transform.Translate(cam.transform.forward,  Space.Self);
        }
        if (Input.GetKey(KeyCode.X))
        {            
            Physics.Raycast(transform.position, cam.transform.forward, out hit);
            if (Vector3.Distance(hit.point, transform.position) < minZoomDistance)
                transform.Translate(-cam.transform.forward,  Space.Self);
        }
        
    }
    
    
    
}
