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
    public static void SolveState(this State state, IAgentController controller) {
        switch (state) {
            case State.LookForFood:
                break; 
            case State.LookForWater:
                Vector3 waterPostion = controller.getBestWaterPosition();
                controller.moveTo(waterPostion);
                controller.drink();
                break; 
            case State.Stealth:
                break; 
            case State.Idle:
                break;
            case State.Wander:
                int x = Random.Range(-5, 5);
                int z = Random.Range(-5, 5);
                Vector3 to = new Vector3(x, 0, z);
                controller.moveTo(to); 
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}