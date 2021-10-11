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
    // public AgentSpawner agentSpawner;

    // TODO: reemplazarlo con un acceso al diccionario 
    protected double thirst = 0;
    protected double hunger = 0;
    
    public void Start()
    {
        StartCoroutine(GetNextState());
        StartCoroutine(SolveState(j++));
    }
    
    // No borrar, no compila. Odio Unity (odiamos*)
    public void Update() {
    }
    
    public abstract void moveTo(Vector3 to);
    public abstract void moveTo(GameObject to);
    public abstract void runTo(Vector3 to);
    public abstract void drink();
    public abstract void eat();
    public abstract void sleep();
    public abstract void reproduce(); 
    
    public abstract void seeAround();
    public abstract GameObject getBestWaterPosition();
    public abstract GameObject getBestFoodPosition();

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
        var distance = transform.position - to;
        return Mathf.Abs(distance.x) < 0.5f && Mathf.Abs(distance.z) < 0.5f;
        // return Vector3.Distance(transform.position, to) < 1.5f;
    }

    public IEnumerator GetNextState() {
        do {
            if (thirst > 0.02f) {
                nextState = State.LookForWater;
            }
            else if (hunger > 0.03f) {
                nextState = State.LookForFood;
            }
            else {
                nextState = State.Wander;
            }
            yield return new WaitForSecondsRealtime(1f / WorldController.TickSpeed);
        } while(true);
    }
    
    public IEnumerator SolveState(int i) {
        while (true) {
            if (!nextState.Equals(currentState) || finished) {
                if (!finished) {
                    FinishedSolvingState();
                }
                currentState = nextState;
                StartCoroutine(currentState.SolveState(this));
            }
            yield return null;
        }
    }
}