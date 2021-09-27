using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum State {
    LookForFood, 
    LookForWater, 
    Stealth,
    Idle,
    Wander
}

public static class StateExtensions {
    // public static void SolveState(this State state, IAgentController controller, GameObject agentGO) {
    public static void SolveState(this State state, IAgentController controller, Agent agent) {
        switch (state) {
            case State.LookForFood:
                break; 
            case State.LookForWater:
                break; 
            case State.Stealth:
                break; 
            case State.Idle:
                break;
            case State.Wander:
                int x = Random.Range(-5, 5);
                int z = Random.Range(-5, 5);
                Vector3 to = new Vector3(x, agent.gameObject.transform.position.y, z);
                // controller.moveTo(new Vector3(Random.Range(-5, 5), agentGO.transform.position.y, Random.Range(-5, 5)));
                controller.moveTo(to);
                // Debug.Log(x + ", " + z);
                agent.StartCoroutine(GetThereCoroutine(to, agent));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public static IEnumerator GetThereCoroutine(Vector3 to, Agent agent)
    {
        while (Vector3.Distance(agent.transform.position, to) > 0.1f)
        {
            yield return null; //new WaitForSeconds(0.5f);
        }
        agent.finished = true;
        yield return null;
    }
}