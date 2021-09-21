using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour, IAgentController
{
    public AgentStats stats;
    private List<State> states; 
    
    // Physics. 
    public float speed = 5f; // TODO: Hay que pasarlo como atributo, es para tenerlo andando
    protected Vector3 velocity = new Vector3 (); 
    public float gravity = -9.8f*3; 
    
    //CharacterController 
    protected CharacterController characterController;
    
    //Character stats
    public float jumpHeight = 5f;  // TODO: Hay que pasarlo como atributo, es para tenerlo andando

    //GroundCheck. Para saber si el agente está en el piso o no 
    protected bool isGrounded; 
    private float groundDistance = 0.2f; // Offset, a veces poner 0 no sirve 
    public Transform groundCheck;  
    public LayerMask groundMask; 
    
    // Start is called before the first frame update
    protected void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    // Las acciones son llamadas desde afuera
    // En Update() van las cosas que deben ocurrir independientemente del control externo 
    protected void Update()
    {
        GroundCheck();
        GenerateGravity();
    }
    
    private void GenerateGravity()
    {
        //If it is on the ground, no vertical motion 
        if (isGrounded && velocity.y < 0)
            velocity.y = 0;
        
        //Gravity control 
        velocity.y += gravity*Time.deltaTime; 
        characterController.Move(velocity*Time.deltaTime); //t^2 in physics 
    }
    
    //Ground Check
    //Is the sphere touches the ground, we are grounded. The sphere is really small.
    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
    
    
    
    
    public abstract void moveTo(Vector3 to);
    public abstract void runTo(Vector3 to);
    public abstract void drink();
    public abstract void eat();
    public abstract void sleep();
    public abstract void seeAround();
}
