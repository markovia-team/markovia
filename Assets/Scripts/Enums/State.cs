using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    LookForFood, 
    LookForWater, 
    Stealth,
}

// Jamas vi algo mas feo en mi vida (salvo quizas NOOOOO NO PODES DECIR ESO) 
// Aun asi, no queda otra
public static class StateExtensions
{
    public static void SolveState(this State state, IAgentController controller)
    {
        switch (state)
        {
            case State.LookForFood:
                break; 
            case State.LookForWater:
                break; 
            case State.Stealth:
                break; 
        }
    }
}