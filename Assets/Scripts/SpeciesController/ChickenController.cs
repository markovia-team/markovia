using System;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ChickenController : MovableAgent
{
    private static float chickenMaxSize = 1.75f;
    private static float chickenMinSize = 0.5f;

    private static float chickenMaxSpeed = 1.75f;
    private static float chickenMinSpeed = 0.5f;

    private float getEffectiveSize(double normalized) {
        return (float) ((chickenMaxSize - chickenMinSize) * normalized + chickenMinSize); 
    }

    private float getEffectiveSpeed(double normalized) {
        return (float) ((chickenMaxSpeed - chickenMinSpeed) * normalized + chickenMinSpeed); 
    }

    new void Start()
    {
        base.Start();
        transform.localScale = getEffectiveSize(stats.GetAttribute(Attribute.Size))*transform.localScale;
        agent.speed = getEffectiveSpeed(stats.GetAttribute(Attribute.Speed))*WorldController.TickSpeed;
    }
    // private static maxSpeed;
    new void Update()
    {
        // base is equivalent to Java's super() call 
        // Need to do a generic Agent Update() before making a Chicken Update()
        base.Update();
        agent.speed = getEffectiveSpeed(stats.GetAttribute(Attribute.Speed))*WorldController.TickSpeed;
    }

    // public override void moveTo(Vector3 to)
    // {
    //     // base.moveTo(to); 
    // }

    public override void runTo(Vector3 to) {
        throw new NotImplementedException();
    }

    public override void drink() {
        throw new NotImplementedException();
    }

    public override void eat() {
        throw new NotImplementedException();
    }

    public override void sleep() {
        throw new NotImplementedException();
    }

    public override void seeAround() {
        throw new NotImplementedException();
    }
    
    public override void reproduce() {
        throw new NotImplementedException();
    }
}