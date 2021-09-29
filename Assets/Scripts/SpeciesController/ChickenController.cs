using System;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ChickenController : MovableAgent
{

    new void Start()
    {
        base.Start();
        transform.localScale = (float)stats.GetAttribute(Attribute.Size)*transform.localScale ; 
    }
    // private static maxSpeed;
    new void Update()
    {
        // base is equivalent to Java's super() call 
        // Need to do a generic Agent Update() before making a Chicken Update()
        base.Update();
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