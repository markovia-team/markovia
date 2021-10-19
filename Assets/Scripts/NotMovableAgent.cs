using System;
using UnityEngine;

public abstract class NotMovableAgent : Agent
{
    public override void moveTo(Vector3 to) {
        throw new NotImplementedException();
    }

    public override void moveTo(GameObject to)
    {
        throw new NotImplementedException();
    }

    public override void runTo(Vector3 to) {
        throw new NotImplementedException();
    }

}