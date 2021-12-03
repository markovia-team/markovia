using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class SphericCameraController : MonoBehaviour
{
    public GameObject centre;

    private static float minPhi = Mathf.PI / 16;
    private static float maxPhi = Mathf.PI / 2 - 0.1f;
    private static float maxtheta = Mathf.PI * 2 - 0.01f;
    private static float maxRadius = 200f;
    private static float minRadius = 30f; 
    
    private float theta = 0f;
    private float phi = maxPhi / 2;
    private float radius = 100f;

    [SerializeField] private float speed = 1; 

    [SerializeField] private Camera cam; 
        
        
    void Update()
    {
        cam.transform.LookAt(centre.transform.position);
        if (Input.GetKey(KeyCode.W))
        {
            if (phi <= minPhi) return; 
            phi -= speed * 0.01f; 
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (phi >= maxPhi) return;
            phi += speed * 0.01f; 
        }
        if (Input.GetKey(KeyCode.A))
        {
            theta -= speed*0.01f;
            if (theta < 0) theta += Mathf.PI * 2; 
        }
        if (Input.GetKey(KeyCode.D))
        {
            theta += speed*0.01f;
            if (theta > Mathf.PI * 2) theta -= Mathf.PI * 2;         
        }
        
        // Zoom 
        if (Input.GetKey(KeyCode.Z))
        {
            radius -= speed * 0.1f; // Mathf.Min(minRadius, radius - );
        }
        if (Input.GetKey(KeyCode.X))
        {
            radius += speed * 0.1f; // Mathf.Max(maxRadius, radius + speed * 0.01f);
        }
        
        
        transform.position = new Vector3(radius*Mathf.Sin(phi)*Mathf.Cos(theta), radius*Mathf.Cos(phi), radius*Mathf.Sin(phi)*Mathf.Sin(theta));
        
    }
}
