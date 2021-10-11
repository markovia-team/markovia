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
        thirst += Time.deltaTime * 0.001f * WorldController.TickSpeed;
        hunger += Time.deltaTime * 0.001f * WorldController.TickSpeed;
        // Debug.Log("thirst: " + thirst);
        if (thirst > 1 || hunger > 1) {
            // Debug.Log("DEAD OF THIRST Puan");
            Destroy(this.gameObject);
        }
    }

    public override void moveTo(Vector3 to) {
        // Debug.Log(":$ " + to);
        Going();
        agent.SetDestination(to);
        StartCoroutine(WaitTilThereCoroutine(to)); 
    }

    public override void runTo(Vector3 to) {
        Going();
        agent.speed *= 1.5f; 
        moveTo(to); 
        agent.speed /= 1.5f;
    }

    public IEnumerator WaitTilThereCoroutine(Vector3 to)
    {
        while (Vector3.Distance(transform.position, to) > 0.5f && !IsSolving())
        {
            // Debug.Log("from: " + transform.position);
            // Debug.Log("to " + to);
            // Debug.Log("tiempo " + Time.deltaTime);
            
            yield return null; // new WaitForSeconds(0.5f); //TODO: no seria mejor cada intervalos chicos de tiempo? 
        }

        IsThere();
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