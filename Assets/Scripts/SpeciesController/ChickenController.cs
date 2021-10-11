using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ChickenController : MovableAgent
{
    private static float chickenMaxSize = 1.75f;
    private static float chickenMinSize = 0.5f;

    private static float chickenMaxSpeed = 2.75f;
    private static float chickenMinSpeed = 1.5f;
    
    private float getEffectiveSize(double normalized) {
        return (float) ((chickenMaxSize - chickenMinSize) * normalized + chickenMinSize); 
    }

    private float getEffectiveSpeed(double normalized) {
        return (float) ((chickenMaxSpeed - chickenMinSpeed) * normalized + chickenMinSpeed); 
    }

    new void Start() {
        base.Start();
        transform.localScale = getEffectiveSize(stats.GetAttribute(Attribute.Size)) * transform.localScale;
        agent.speed = getEffectiveSpeed(stats.GetAttribute(Attribute.Speed)) * WorldController.TickSpeed;
    }

    new void Update() {
        base.Update();
        agent.speed = getEffectiveSpeed(stats.GetAttribute(Attribute.Speed))*WorldController.TickSpeed;
    }

    public override void runTo(Vector3 to) {
        // base.moveTo(to); 
        throw new NotImplementedException();
    }

    public override void drink() {
        Debug.Log("CHICKEN DRINK");
        thirst = 0; 
    }

    public override void eat() {
        Debug.Log("CHICKEN EAT");
        hunger = 0;
    }

    public override void sleep() {
        throw new NotImplementedException();
    }

    public override void seeAround() {
        throw new NotImplementedException();
    }

    public override GameObject getBestWaterPosition()
    {
        List<GameObject> waters = worldController.GetWaterReferences();
        // Vector3 bestWater = new Vector3(Single.PositiveInfinity, Single.PositiveInfinity, Single.PositiveInfinity);
        float bestDistance = float.MaxValue;
        GameObject result = null;
        foreach (var w in waters)
        {
            var dist = Vector3.Distance(this.transform.position, w.transform.position); 
            if (dist < bestDistance)
            {
                bestDistance = dist;
                result = w; 
            }
        }
        return result;
    }

    public override GameObject getBestFoodPosition() {
        List<GameObject> waters = worldController.GetFoodReferences();
        Vector3 bestFood = new Vector3(Single.PositiveInfinity, Single.PositiveInfinity, Single.PositiveInfinity);
        float bestDistance = float.MaxValue;
        GameObject result = null;
        foreach (var w in waters)
        {
            var dist = Vector3.Distance(this.transform.position, w.transform.position); 
            if (dist < bestDistance)
            {
                bestDistance = dist;
                bestFood = w.transform.position;
                result = w;
            }
        }
        return result;
    }

    public override void reproduce() {
        throw new NotImplementedException();
    }
}