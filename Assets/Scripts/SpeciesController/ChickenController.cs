using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ChickenController : Agent
{
    // Time to next rotation. Recontra Dummy, es para ir jugando 
    public float wanderTime = 5f; 
    
    new void Update()
    {
        // Equivalente to Java's super() call 
        // Need to do a generic Agent Update() before making a Chicken Update()
        base.Update();
        
        
        if (isGrounded && wanderTime > 0)
        {
            characterController.Move(speed * Time.deltaTime *  transform.TransformDirection(Vector3.forward));
            wanderTime -= Time.deltaTime; 
        }
        else
        {
            wanderTime = Random.Range(2.0f, 5.0f);
            transform.LookAt(new Vector3(Random.Range(-3f, 3f), transform.position.y, Random.Range(-5f, 5f)));
        }
    }

    public override void moveTo(Vector3 to)
    {
        throw new NotImplementedException();
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