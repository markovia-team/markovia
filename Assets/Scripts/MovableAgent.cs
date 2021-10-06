using System;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class MovableAgent : Agent
{
    public UnityEngine.AI.NavMeshAgent agent;
    
    
    // new void Start() {
    //     agent = GetComponent<NavMeshAgent>();
    // }

    public void Update() {
        Debug.Log(thirst);
        if (thirst > 1) Debug.Log("DEAD OF THIRST");
    }

    public override void moveTo(Vector3 to) {
        agent.SetDestination(to);
        StartCoroutine(WaitTilThereCoroutine(to)); 
    }

    public override void runTo(Vector3 to) {
        agent.speed *= 1.5f; 
        moveTo(to); 
        agent.speed /= 1.5f;
    }

    public IEnumerator WaitTilThereCoroutine(Vector3 to)
    {
        while (Vector3.Distance(transform.position, to) > 0.5f)
        {
            thirst += 0.00001; 
            yield return null; // new WaitForSeconds(0.5f); //TODO: no seria mejor cada intervalos chicos de tiempo? 
        }

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