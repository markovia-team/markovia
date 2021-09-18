using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAgent : Agent
{
    public override void moveTo(Vector3 to)
    {
        transform.Translate(to);
    }

    public override void runTo(Vector3 to)
    {
        transform.Translate(to);
    }

    public override void drink()
    {
        throw new System.NotImplementedException();
    }

    public override void eat()
    {
        throw new System.NotImplementedException();
    }

    public override void sleep()
    {
        throw new System.NotImplementedException();
    }

    public override void seeAround()
    {
        throw new System.NotImplementedException();
    }
}