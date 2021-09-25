using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
    LookForFood, 
    LookForWater, 
    Stealth,
    Idle
}

public static class StateExtensions {
    public static void SolveState(this State state, IAgentController controller) {
        switch (state) {
            case State.LookForFood:
                break; 
            case State.LookForWater:
                break; 
            case State.Stealth:
                break; 
            case State.Idle:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}