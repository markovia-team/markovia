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
    public static IEnumerator SolveState(this State state, IAgentController controller) {
        switch (state) {
            case State.LookForFood:
                
                controller.BeginSolvingState();
                GameObject food = controller.getBestFoodPosition();
                controller.moveTo(food);
                
                while (controller.IsSolving()) {
                    if (controller.IsHere(food.transform.position)) {
                        // UnityEngine.Object.Destroy(foodPosition);
                        controller.eat();

                        controller.FinishedSolvingState();
                    }
                    yield return null;
                }

                break; 
            case State.LookForWater:
                controller.BeginSolvingState();
                GameObject water = controller.getBestWaterPosition();
                controller.moveTo(water);
                
                while (controller.IsSolving()) {
                    if (controller.IsHere(water.transform.position)) { 
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

                while (controller.IsSolving()) {
                    if (controller.IsHere(to)) { 
                        controller.FinishedSolvingState();
                    }
                    yield return null;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}