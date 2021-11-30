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
        base.Update();
    }

    public override void moveTo(Vector3 to)
    {
        Going();
        agent.SetDestination(to);
        StartCoroutine(WaitTillThereCoroutine(to)); 
    }

    public override void moveTo(GameObject to) {
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
        while (Vector3.Distance(transform.position, to.transform.position) > 0.5f && IsSolving()) {
            agent.SetDestination(to.transform.position);
            yield return null; // new WaitForSeconds(0.5f); //TODO: no seria mejor cada intervalos chicos de tiempo? 
        }

        IsThere();
        yield return null;
    }

    public IEnumerator WaitTillThereCoroutine(Vector3 to)
    {
        while (Vector3.Distance(transform.position, to) > 0.5f && IsSolving()) {
            stats.SetDistance(Distance.ToFood, Vector3.Distance(transform.position, getBestFoodPosition().transform.position));
            stats.SetDistance(Distance.ToWater, Vector3.Distance(transform.position, getBestWaterPosition().transform.position));
            yield return null; // new WaitForSeconds(0.5f); //TODO: no seria mejor cada intervalos chicos de tiempo? 
        }

        stats.SetDistance(Distance.ToFood, Vector3.Distance(transform.position, getBestFoodPosition().transform.position));
        stats.SetDistance(Distance.ToWater, Vector3.Distance(transform.position, getBestWaterPosition().transform.position));
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