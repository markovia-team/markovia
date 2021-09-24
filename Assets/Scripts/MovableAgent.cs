using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class MovableAgent : Agent
{
    public UnityEngine.AI.NavMeshAgent agent;

    // new void Start() {
    //     agent = GetComponent<NavMeshAgent>();
    // }

    public void Update() {
        if (agent.remainingDistance < agent.stoppingDistance)
            moveTo(new Vector3(Random.Range(-5, 5), transform.position.y, Random.Range(-5, 5))); 
    }

    public override void moveTo(Vector3 to) {
        agent.SetDestination(to); 
    }

    public override void runTo(Vector3 to) {
        agent.speed *= 2; 
        moveTo(to); 
        agent.speed /= 2;
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