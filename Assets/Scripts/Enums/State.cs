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
    public static void SolveState(this State state, IAgentController controller, GameObject agentGO) {
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
                controller.moveTo(new Vector3(Random.Range(-5, 5), agentGO.transform.position.y, Random.Range(-5, 5)));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}