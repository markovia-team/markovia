using System;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ChickenController : Agent
{
    private Vector3 destination = new Vector3();

    new void Update()
    {
        // base if equivalent to Java's super() call 
        // Need to do a generic Agent Update() before making a Chicken Update()
        base.Update();
        moveTo(destination);
        if (destination == Vector3.zero)
        {
            destination.x = Random.Range(-5.0f, 5.0f); 
            destination.z = Random.Range(-5.0f, 5.0f);
        }
    }

    public override void moveTo(Vector3 to)
    {
        // You are a terrestrial animal. You cannot move while on air
        if (!isGrounded) return; 
        
        const float offsetRadius = 0.5f; 
        transform.LookAt(to);
        
        // Ask for the distance to a point Vector3 'to'. However, project 'to' height's into the chicken's height
        if ( Vector3.Distance( transform.position, new Vector3(to.x, transform.position.y, to.z)) > offsetRadius)
        {
            characterController.Move(speed * Time.deltaTime *  transform.TransformDirection(Vector3.forward));
        }
        else
        {
            destination = Vector3.zero; 
        }
    }

    public override void runTo(Vector3 to)
    {
        throw new NotImplementedException();
    }

    public override void drink()
    {
        throw new NotImplementedException();
    }

    public override void eat()
    {
        throw new NotImplementedException();
    }

    public override void sleep()
    {
        throw new NotImplementedException();
    }

    public override void seeAround()
    {
        throw new NotImplementedException();
    }

}