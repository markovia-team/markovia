using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public enum State {
    LookForFood, 
    LookForWater, 
    Stealth,
    Sleep,
    Wander,
    Reproduce,
    AsexualReproduce,
    Idle
}

public static class StateExtensions {
    public static IEnumerator SolveState(this State state, Agent agent) {
        switch (state) {
            case State.LookForFood:
                agent.BeginSolvingState();
                Agent food = agent.getBestFoodPosition();

                if (food == null) {
                    agent.stats.UpdateNeed(Need.Thirst, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Sleep, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    if (agent.GetAge() > 10f)
                        agent.stats.UpdateNeed(Need.ReproductiveUrge, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Hunger, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    agent.FinishSolvingState();
                    break;
                }

                agent.moveTo(food.gameObject);

                do {
                    agent.stats.UpdateNeed(Need.Thirst, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Sleep, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    if (agent.GetAge() > 10f)
                        agent.stats.UpdateNeed(Need.ReproductiveUrge, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    
                    if (food == null) {
                        break;
                    }
                    
                    if (agent.IsHere(food.transform.position)) {
                        agent.stats.UpdateNeed(Need.Hunger, -0.8f);
                        food.Die();
                        break;
                    } else {
                        agent.stats.UpdateNeed(Need.Hunger, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    }
                    yield return null;
                } while (agent.IsSolving() && agent.stats.GetNeed(Need.Hunger) > 0 && food != null);
                agent.ResetCoroutines();
                
                break; 
            case State.LookForWater:
                agent.BeginSolvingState();
                GameObject water = agent.getBestWaterPosition();
                agent.moveTo(water);

                do {
                    agent.stats.UpdateNeed(Need.Hunger, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Sleep, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    if (agent.GetAge() > 10f)
                        agent.stats.UpdateNeed(Need.ReproductiveUrge, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    
                    if (agent.IsHere(water.transform.position)) {
                        agent.stats.UpdateNeed(Need.Thirst, -0.2f * Time.deltaTime * WorldController.TickSpeed);
                    }
                    else {
                        agent.stats.UpdateNeed(Need.Thirst, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    }
                    yield return null;
                } while (agent.IsSolving() && agent.stats.GetNeed(Need.Thirst) > 0);
                agent.ResetCoroutines();

                break;
            case State.Stealth:
                break; 
            case State.Idle:
                agent.BeginSolvingState();
                do {
                    agent.stats.UpdateNeed(Need.ReproductiveUrge, 0.03f * Time.deltaTime * WorldController.TickSpeed * 3f);
                    yield return null;
                } while (agent.IsSolving());
                agent.ResetCoroutines();
                
                break;
            case State.Wander:
                agent.BeginSolvingState();
                int x = Random.Range(-5, 5);
                int z = Random.Range(-5, 5);
                Vector3 to = new Vector3(x, 0, z);
                agent.moveTo(to);
                
                do {
                    agent.stats.UpdateNeed(Need.Hunger, 0.005f * Time.deltaTime * WorldController.TickSpeed * 2f);
                    agent.stats.UpdateNeed(Need.Thirst, 0.005f * Time.deltaTime * WorldController.TickSpeed * 2f);
                    agent.stats.UpdateNeed(Need.Sleep, 0.005f * Time.deltaTime * WorldController.TickSpeed * 2f);
                    if (agent.GetAge() > 10f)
                        agent.stats.UpdateNeed(Need.ReproductiveUrge, 0.005f * Time.deltaTime * WorldController.TickSpeed * 2f);
                
                    if (agent.IsHere(to)) { 
                        break;
                    }
                    yield return null;
                } while (agent.IsSolving());
                agent.ResetCoroutines();
                
                break;
            case State.Sleep:
                agent.BeginSolvingState();

                do {
                    agent.stats.UpdateNeed(Need.Hunger, 0.01f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Thirst, 0.01f * Time.deltaTime * WorldController.TickSpeed);
                    if (agent.GetAge() > 10f)
                        agent.stats.UpdateNeed(Need.ReproductiveUrge, 0.01f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Sleep, -0.1f * Time.deltaTime * WorldController.TickSpeed);
                    yield return null;
                } while (agent.IsSolving() && agent.stats.GetNeed(Need.Sleep) > 0f);
                agent.ResetCoroutines();
                
                break;
            case State.Reproduce:
                agent.BeginSolvingState();
                
                // Debug.Log("-- REPRODUCE --" + agent.name);

                Agent mate = agent.findMate();
                if (mate == null || mate.gameObject == null) {
                    agent.stats.UpdateNeed(Need.Hunger, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Thirst, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Sleep, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    agent.FinishSolvingState();
                    break;
                }

                agent.moveTo(mate.gameObject);
                do {
                    agent.stats.UpdateNeed(Need.Hunger, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Thirst, 0.005f * Time.deltaTime * WorldController.TickSpeed);
                    agent.stats.UpdateNeed(Need.Sleep, 0.005f * Time.deltaTime * WorldController.TickSpeed);

                    if (mate == null || mate.gameObject == null) {
                        break;
                    }
                    
                    if (agent.IsHere(mate.transform.position)) {
                        //Debug.Log("-- ME VOY --" + agent.name);
                        agent.FinishSolvingState();
                        mate.FinishSolvingState();
                        mate.StopAllCoroutines();
                        agent.stats.SetNeed(Need.ReproductiveUrge, 0f);
                        mate.stats.SetNeed(Need.ReproductiveUrge, 0f);
                        agent.worldController.GetComponent<AgentSpawner>().gameAgents.TryGetValue(agent.GetSpecies(), out var set);
                        if (set.Count > 30) {
                            //Debug.Log("-- LÍMITE --" + agent.name);
                            break;
                        }
                        agent.worldController.GetComponent<AgentSpawner>().Reproduce(agent, mate, agent.GetSpecies());
                        mate.ResetCoroutines();
                        //Debug.Log("-- SE REPRODUJO --" + agent.name);
                        break;
                    }
                    yield return null;
                } while (agent.IsSolving() && agent.stats.GetNeed(Need.ReproductiveUrge) > 0);
                agent.ResetCoroutines();
                break;

            case State.AsexualReproduce:
                agent.BeginSolvingState();
                agent.worldController.GetComponent<AgentSpawner>().AsexualReproduce(agent, agent.GetSpecies());
                agent.stats.SetNeed(Need.ReproductiveUrge, 0f);
                agent.ResetCoroutines();
                yield return null;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}