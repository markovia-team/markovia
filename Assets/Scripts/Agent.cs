using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour, IAgentController
{
    public AgentStats stats;
    private State currentState = State.Wander;
    private State nextState = State.Wander;
    private bool finished = true;
    private bool going = false;
    int j = 0;
    public WorldController worldController; 

    // TODO: reemplazarlo con un acceso al diccionario 
    protected double thirst = 0;
    protected double hunger = 0;
    
    
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(GetNextState());
        StartCoroutine(SolveState(j++));
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
    public abstract Vector3 getBestFoodPosition();

    public void FinishedSolvingState() {
        finished = true; 
        StopAllCoroutines();
        StartCoroutine(GetNextState());
        StartCoroutine(SolveState(j++));
    }
    public void BeginSolvingState() {
        finished = false; 
    }
    public bool IsSolving() {
        return !finished;
    }
    public bool IsGoing() {
        return going;
    }
    public void IsThere() {
        going = false;
    }
    public void Going() {
        going = true;
    }
    public bool IsHere(Vector3 to) {
        // Debug.Log("chan: " + to + " from: " + transform.position);
        // return Vector3.Distance(transform.position.x, to.x) < 0.5f && Vector3.Distance(transform.position.z, to.z) < 0.5f;
        var distance = transform.position - to;
        return Mathf.Abs(distance.x) < 0.5f && Mathf.Abs(distance.z) < 0.5f;
        // return Vector3.Distance(transform.position, to) < 1.5f;
    }

    public IEnumerator GetNextState() {
        do
        {
            if (thirst > 0.02f)
            {
                nextState = State.LookForWater;

            }
            else if (hunger > 0.03f)
            {
                nextState = State.LookForFood;
            }
            else {
                nextState = State.Wander;
            }
            yield return new WaitForSecondsRealtime(1f / WorldController.TickSpeed);
            // yield return null;
        } while(true);
    }
    
    public IEnumerator SolveState(int i) {
        while (true) {
            // Debug.Log("next: " + nextState);
            // Debug.Log("current: " + currentState);
            // Debug.Log(finished);
            if (!nextState.Equals(currentState) || finished) {
                if (!finished) {
                    // StopCoroutine("WaitTillThereCoroutine");
                    // going = false;
                    FinishedSolvingState();
                }
                // Debug.Log(i + ": " + nextState);
                // BeginSolvingState();
                currentState = nextState;
                StartCoroutine(currentState.SolveState(this));
            }
            yield return null;
        }
    }
}