using System;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Runtime.Serialization;

public class FoxController : MovableAgent {
    private static float foxMaxSize = 2f;
    private static float foxMinSize = 0.8f;
    private static float foxMaxSpeed = 5;
    private static float foxMinSpeed = 2.5f;
    private Vector3 transformInicial;

    private float getEffectiveSize(double normalized) {
        return (float) ((foxMaxSize - foxMinSize) * normalized + foxMinSize);
    }

    private float getEffectiveSpeed(double normalized) {
        return (float) ((foxMaxSpeed - foxMinSpeed) * normalized + foxMinSpeed);
    }

    new void Start() {
        base.Start();
        agent.speed = getEffectiveSpeed(stats.GetAttribute(Attribute.Speed)) * WorldController.TickSpeed;
        transformInicial = transform.localScale;
    }

    new void Update() {
        base.Update();
        transform.localScale = getEffectiveSize(stats.GetAttribute(Attribute.Size) * SizeWithAge()) * transformInicial;
        agent.speed = getEffectiveSpeed(stats.GetAttribute(Attribute.Speed)) * WorldController.TickSpeed;
    }

    public override void runTo(Vector3 to) {
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

    public override Species GetSpecies() {
        return Species.Fox;
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
    }

    public override Agent getBestFoodPosition() {
        HashSet<Agent> chickenSet = worldController.GetComponent<AgentSpawner>().GetSpecies(Species.Chicken);
        if (chickenSet == null)
            return null;
        float bestDistance = float.MaxValue;
        Agent result = null;
        foreach (Agent c in chickenSet) {
            if (c == null || c.gameObject == null)
                continue;
            float dist = Vector3.Distance(this.transform.position, c.transform.position);
            if (dist < bestDistance) {
                bestDistance = dist;
                result = c;
            }
        }

        return result;
    }

    public override Agent findMate() {
        HashSet<Agent> foxSet = worldController.GetComponent<AgentSpawner>().GetSpecies(GetSpecies());
        float bestDistance = float.MaxValue;
        Agent result = null;
        foreach (Agent f in foxSet) {
            if (f.Equals(this))
                continue;
            if (f.GetAge() > 10f) {
                float dist = Vector3.Distance(this.transform.position, f.transform.position);
                if (dist < bestDistance) {
                    bestDistance = dist;
                    result = f;
                }
            }
        }

        return result;
    }

    public override void reproduce() {
        throw new NotImplementedException();
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
        throw new NotImplementedException();
    }

    public override string ToString() {
        return "Fox";
    }
}