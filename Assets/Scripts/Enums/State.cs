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
    public static IEnumerator SolveState(this State state, IAgentController controller) {
        switch (state) {
            case State.LookForFood:
                
                controller.BeginSolvingState();
                Vector3 foodPostion = controller.getBestFoodPosition();
                controller.moveTo(foodPostion);
                
                while (controller.IsSolving()) {
                    if (controller.IsHere(foodPostion)) { 
                        controller.eat();
                        controller.FinishedSolvingState();
                    }
                    yield return null;
                }

                break; 
            case State.LookForWater:
                // Debug.Log("MBOENAS");
                controller.BeginSolvingState();
                Vector3 waterPostion = controller.getBestWaterPosition();
                //Debug.Log(waterPostion);
                controller.moveTo(waterPostion);
                
                while (controller.IsSolving()) {
                    // Debug.Log("SUSANo: " + controller.IsHere(waterPostion));
                    if (controller.IsHere(waterPostion)) { 
                        controller.drink();
                        controller.FinishedSolvingState();
                    }
                    yield return null;
                }

                break;

            case State.Stealth:
                break; 
            case State.Idle:
                break;
            case State.Wander:
                controller.BeginSolvingState();
                int x = Random.Range(-5, 5);
                int z = Random.Range(-5, 5);
                Vector3 to = new Vector3(x, 0, z);
                controller.moveTo(to);
                // while (controller.IsGoing());

                while (controller.IsSolving()) {
                    if (controller.IsHere(to)) { 
                        controller.FinishedSolvingState();
                        // Debug.Log("hola? hola bb");
                    }
                    yield return null;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}