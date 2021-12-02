using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class FoxController : MovableAgent {
    private static float foxMaxSize = 1.5f;
    private static float foxMinSize = 0.5f;
    private static float foxMaxSpeed = 5;
    private static float foxMinSpeed = 2.75f;
    
    private float getEffectiveSize(double normalized) {
        return (float) ((foxMaxSize - foxMinSize) * normalized + foxMinSize); 
    }

    private float getEffectiveSpeed(double normalized) {
        return (float) ((foxMaxSpeed - foxMinSpeed) * normalized + foxMinSpeed); 
    }

    new void Start()
    {
        base.Start();
        transform.localScale = getEffectiveSize(stats.GetAttribute(Attribute.Size)) * transform.localScale;
        agent.speed = getEffectiveSpeed(stats.GetAttribute(Attribute.Speed)) * WorldController.TickSpeed;
    }

    new void Update()
    {
        // Need to do a generic Agent Update() before making a fox Update()
        base.Update();
        agent.speed = getEffectiveSpeed(stats.GetAttribute(Attribute.Speed))*WorldController.TickSpeed;
    }

    public override void runTo(Vector3 to) {
        // base.moveTo(to); 
        throw new NotImplementedException();
    }

    public override void drink() {
        
    }

    public override void eat() {
        
    }

    public override void sleep() {
        throw new NotImplementedException();
    }

    public override void seeAround() {
        throw new NotImplementedException();
    }

    public override GameObject getBestWaterPosition() {
        
        List<GameObject> waters = worldController.GetWaterReferences();
        float bestDistance = float.MaxValue;
        GameObject result = null;
        foreach (var w in waters) {
            var dist = Vector3.Distance(this.transform.position, w.transform.position); 
            if (dist < bestDistance) {
                bestDistance = dist;
                result = w; 
            }
        }
        return result;
        
        //return this.gameObject;
    }

    public override GameObject getBestFoodPosition() {
        // HashSet<GameObject> waters = worldController.agentSpawner.GetChickens();
        
        List<GameObject> chicken = worldController.GetFoodReferences();
        float bestDistance = float.MaxValue;
        GameObject result = null;
        foreach (var c in chicken) {
            var dist = Vector3.Distance(this.transform.position, c.transform.position); 
            if (dist < bestDistance) {
                bestDistance = dist;
                result = c;
            }
        }
        return result;
        
        //return this.gameObject;
    }

    public override Agent findMate()
    {
        throw new NotImplementedException();
    }

    public override void reproduce() {
        throw new NotImplementedException();
    }
}