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
        if (thirst > 1 || hunger > 1) {
            Destroy(this.gameObject);
        }
    }

    public override void moveTo(Vector3 to) {
        Going();
        agent.SetDestination(to);
        StartCoroutine(WaitTilThereCoroutine(to)); 
    }

    public override void moveTo(GameObject to) {
        Debug.Log(this);
        Going();
        StartCoroutine(FollowObject(to));
    }

    public override void runTo(Vector3 to) {
        Going();
        agent.speed *= 1.5f; 
        moveTo(to); 
        agent.speed /= 1.5f;
    }

    public IEnumerator FollowObject(GameObject to) {
        // Debug.Log(to.transform.position);
        while (Vector3.Distance(transform.position, to.transform.position) > 0.5f && IsSolving()) {
            agent.SetDestination(to.transform.position);
            yield return null; // new WaitForSeconds(0.5f); //TODO: no seria mejor cada intervalos chicos de tiempo? 
        }

        IsThere();
        yield return null;
    }

    public IEnumerator WaitTilThereCoroutine(Vector3 to)
    {
        while (Vector3.Distance(transform.position, to) > 0.5f && IsSolving()) {
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