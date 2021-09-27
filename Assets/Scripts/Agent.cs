using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour, IAgentController
{
    private AgentStats stats;
    private State currentState = State.Wander;
    private State nextState = State.Wander;
    public bool finished = true;
    
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(GetNextState());
        StartCoroutine(SolveState());
    }
    
    // Las acciones son llamadas desde afuera
    // En Update() van las cosas que deben ocurrir independientemente del control externo
    public void Update()
    {
        /*
        if (!nextState.Equals(currentState) || finished) {
            if (!finished)
                StopCoroutine(SolveState());
            currentState = nextState;
            StartCoroutine(SolveState());
        }
        */
    }

    // State nowState = stats.nextState();
    // nowState.SolveState(this);

    public abstract void moveTo(Vector3 to);
    public abstract void runTo(Vector3 to);
    public abstract void drink();
    public abstract void eat();
    public abstract void sleep();
    public abstract void reproduce(); 
    
    public abstract void seeAround();

    public IEnumerator GetNextState() {
        do {
            nextState = /*stats.NextState();*/ State.Wander;
            yield return new WaitForSecondsRealtime(1f);
            // yield return null;
        } while(true);
    }

    /*
    public IEnumerator SolveState() {
        finished = false;
        currentState.SolveState(this, this.gameObject);
        finished = true;
        Debug.Log("finished");
        yield return null;
    }
    */
    
    public IEnumerator SolveState()
    {
        while(true)
        {
            if (!nextState.Equals(currentState) || finished)
            {
                finished = false;
                currentState.SolveState(this, this);
                Debug.Log(finished);
            }
            yield return null;
        }
    }
}