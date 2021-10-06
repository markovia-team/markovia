using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour, IAgentController
{
    public AgentStats stats;
    private State currentState = State.Wander;
    private State nextState = State.Wander;
    private bool finished = true;
    public WorldController worldController; 

    // TODO: reemplazarlo con un acceso al diccionario 
    protected double thirst = 0; 
    
    
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(GetNextState());
        StartCoroutine(SolveState());
    }
    
    // No borrar, no compila. Odio Unity
    public void Update() {
    }
    
    public abstract void moveTo(Vector3 to);
    public abstract void runTo(Vector3 to);
    public abstract void drink();
    public abstract void eat();
    public abstract void sleep();
    public abstract void reproduce(); 
    
    public abstract void seeAround();
    public abstract Vector3 getBestWaterPosition();

    protected void FinishedSolvingState() {
        finished = true; 
    }
    protected void BeginSolvingState() {
        finished = false; 
    }

    public IEnumerator GetNextState() {
        do
        {
            if (thirst > 0.02)
            {
                Debug.Log("LOOKING FOR WATER");
                nextState = State.LookForWater;
            } 
            else nextState = State.Wander;/*stats.NextState();*/ 
            yield return new WaitForSecondsRealtime(1f / WorldController.TickSpeed);
            // yield return null;
        } while(true);
    }
    
    public IEnumerator SolveState() {
        while(true) {
            if (!nextState.Equals(currentState) || finished) {
                BeginSolvingState();
                currentState.SolveState(this);
            }
            yield return null;
        }
    }
}