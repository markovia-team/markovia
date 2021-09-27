using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class MovableAgent : Agent
{
    public UnityEngine.AI.NavMeshAgent agent;

    // new void Start() {
    //     agent = GetComponent<NavMeshAgent>();
    // }

    // public void Update() {
    //     if (agent.remainingDistance < agent.stoppingDistance); 
    // }

    public override void moveTo(Vector3 to) {
        agent.SetDestination(to);
        StartCoroutine(WaitTilThereCoroutine(to)); 
    }

    public override void runTo(Vector3 to) {
        agent.speed *= 2; 
        moveTo(to); 
        agent.speed /= 2;
    }

    public IEnumerator WaitTilThereCoroutine(Vector3 to)
    {
        while (Vector3.Distance(transform.position, to) > 0.5f)
            yield return null; // new WaitForSeconds(0.5f); //TODO: no seria mejor cada intervalos chicos de tiempo? 
        FinishedSolvingState();
        yield return null;
    }
    
    
    // public override void moveToClick() {
    //     if (Input.GetMouseButtonDown(0)) {
    //         Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //         RaycastHit hit;
        
    //         if (Physics.Raycast(ray, out hit))
    //             agent.SetDestination(hit.point); 
    //     }
    // }
}