using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour, IAgentController
{
    public AgentStats stats;
    private State currentState = State.Idle;
    private State nextState = State.Idle;
    private bool finished = true;
    private bool going = false;
    public WorldController worldController;
    private bool dead = false;
    // public AgentSpawner agentSpawner;
    
    public void Start()
    {
        ResetCoroutines();
    }
    
    // No borrar, no compila. Odio Unity (odiamos*) :D
    public void Update()
    {
        int i = 0;
        // Debug.Log("Sleep: " + stats.GetNeed(Need.Sleep));
        if (!dead) {
            foreach (double value in stats.Needs.Values)
                if (value == 1f) {
                    // Debug.Log(i++);
                    Die();
                }
        }
    }
    
    public abstract void moveTo(Vector3 to);
    public abstract void moveTo(GameObject to);
    public abstract void runTo(Vector3 to);
    public abstract void drink();
    public abstract void eat();
    public abstract void sleep();
    public abstract void reproduce();
    
    public abstract void seeAround();
    public abstract Species GetSpecies();
    public abstract GameObject getBestWaterPosition();
    public abstract GameObject getBestFoodPosition();
    public abstract Agent findMate();

    public void ResetCoroutines() {
        finished = true; 
        StopAllCoroutines();
        StartCoroutine(GetNextState());
        StartCoroutine(SolveState());
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
        stats.SetDistance(Distance.ToFood, Vector3.Distance(transform.position, getBestFoodPosition().transform.position));
        stats.SetDistance(Distance.ToWater, Vector3.Distance(transform.position, getBestWaterPosition().transform.position));
        going = false;
    }
    public void Going() {
        going = true;
    }
    public bool IsHere(Vector3 to) {
        var distance = transform.position - to;
        return Mathf.Abs(distance.x) < 0.8f && Mathf.Abs(distance.z) < 0.8f;
        // return Vector3.Distance(transform.position, to) < 1.5f;
    }

    public IEnumerator GetNextState() {
        do {
            nextState = stats.NextState();
            /*
            if (thirst > 0.02f) {
                nextState = State.LookForWater;
            }
            else if (hunger > 0.03f) {
                nextState = State.LookForFood;
            }
            else {
                nextState = State.Wander;
            }
            */
            yield return new WaitForSecondsRealtime(1f / WorldController.TickSpeed);
        } while(true);
    }

    public void Die() {
        // this.transform.Rotate(0f, 0f, 90f);
        worldController.GetComponent<AgentSpawner>().Died(this, GetSpecies());
        Destroy(this.gameObject);
        dead = true;
        StopAllCoroutines();
        // Destroy(this.gameObject);
    }
    
    public IEnumerator SolveState() {
        while (true) {
            if (!nextState.Equals(currentState) || finished) {
                if (!finished) {
                    ResetCoroutines();
                }
                currentState = nextState;
                StartCoroutine(currentState.SolveState(this));
            }
            yield return null;
        }
    }
}