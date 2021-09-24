using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour, IAgentController
{
    public AgentStats stats;
    private List<State> states; 
    
    // Start is called before the first frame update
    protected void Start()
    {
        
    }
    
    // Las acciones son llamadas desde afuera
    // En Update() van las cosas que deben ocurrir independientemente del control externo 
    protected void Update()
    {
        
    }

    public abstract void moveTo(Vector3 to);
    public abstract void runTo(Vector3 to);
    public abstract void drink();
    public abstract void eat();
    public abstract void sleep();
    public abstract void seeAround();
}
