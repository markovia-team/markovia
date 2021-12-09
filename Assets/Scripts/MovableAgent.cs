using System.Collections;
using UnityEngine;

public abstract class MovableAgent : Agent {
    public UnityEngine.AI.NavMeshAgent agent;

    public void Update() {
        base.Update();
    }

    public override void moveTo(Vector3 to) {
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
        while (to != null && Vector3.Distance(transform.position, to.transform.position) > 0.5f && IsSolving()) {
            agent.SetDestination(to.transform.position);
            yield return null;
        }

        if (!to.Equals(null))
            IsThere();
        yield return null;
    }

    public IEnumerator WaitTillThereCoroutine(Vector3 to) {
        while (Vector3.Distance(transform.position, to) > 0.5f && IsSolving()) {
            yield return null;
        }

        IsThere();
        yield return null;
    }
}