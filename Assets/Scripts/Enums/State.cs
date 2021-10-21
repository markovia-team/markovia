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
    Wander,
    Sleep
}

public static class StateExtensions {
    public static IEnumerator SolveState(this State state, Agent agent) {
        switch (state) {
            case State.LookForFood:
                
                agent.BeginSolvingState();
                GameObject food = agent.getBestFoodPosition();
                agent.moveTo(food);
                
                while (agent.IsSolving()) {
                    agent.stats.UpdateNeed(Need.Thirst, 0.2f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Sleep, 0.2f * Time.deltaTime * WorldController.TickSpeed);
                    if (agent.IsHere(food.transform.position)) {
                        // UnityEngine.Object.Destroy(foodPosition);
                        agent.eat();
                        agent.stats.UpdateNeed(Need.Hunger, -0.4f * Time.deltaTime * WorldController.TickSpeed);
                    }
                    else {
                        agent.stats.UpdateNeed(Need.Hunger, 0.2f * Time.deltaTime * WorldController.TickSpeed);
                    }
                    yield return null;
                }

                break; 
            case State.LookForWater:
                agent.BeginSolvingState();
                GameObject water = agent.getBestWaterPosition();
                agent.moveTo(water);
                
                while (agent.IsSolving()) {
                    agent.stats.UpdateNeed(Need.Hunger, 0.2f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Sleep, 0.2f * Time.deltaTime * WorldController.TickSpeed);
                    if (agent.IsHere(water.transform.position)) {
                        agent.drink();
                        agent.stats.UpdateNeed(Need.Thirst, -0.4f * Time.deltaTime * WorldController.TickSpeed);
                    }
                    else {
                        agent.stats.UpdateNeed(Need.Thirst, 0.2f * Time.deltaTime * WorldController.TickSpeed);
                    }
                    yield return null;
                }

                break;

            case State.Stealth:
                break; 
            case State.Idle:
                agent.BeginSolvingState();
                while (agent.IsSolving()) {
                    agent.stats.UpdateNeed(Need.Hunger, 0.1f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Thirst, 0.1f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Sleep, 0.1f * Time.deltaTime * WorldController.TickSpeed);
                }
                break;
            case State.Wander:
                agent.BeginSolvingState();
                int x = Random.Range(-5, 5);
                int z = Random.Range(-5, 5);
                Vector3 to = new Vector3(x, 0, z);
                agent.moveTo(to);

                while (agent.IsSolving()) {
                    agent.stats.UpdateNeed(Need.Hunger, 0.2f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Thirst, 0.2f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Sleep, 0.2f * Time.deltaTime * WorldController.TickSpeed);

                    if (agent.IsHere(to)) { 
                        agent.ResetCoroutines();
                    }
                    yield return null;
                }
                break;
            case State.Sleep:
                agent.BeginSolvingState();
                while(agent.IsSolving()) {
                    agent.stats.UpdateNeed(Need.Hunger, 0.3f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Thirst, 0.3f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Sleep, -0.2f * Time.deltaTime * WorldController.TickSpeed);
                    yield return null;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}