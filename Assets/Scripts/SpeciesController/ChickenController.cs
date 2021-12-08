using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using System.Runtime.Serialization;

public class ChickenController : MovableAgent
{
    private static float chickenMaxSize = 1.25f;
    private static float chickenMinSize = 0.75f;
    private static float chickenMaxSpeed = 2.75f;
    private static float chickenMinSpeed = 1.5f;
    private Vector3 transformInicial;
    
    private float getEffectiveSize(double normalized) {
        return (float) ((chickenMaxSize - chickenMinSize) * normalized + chickenMinSize); 
    }

    private float getEffectiveSpeed(double normalized) {
        return (float) ((chickenMaxSpeed - chickenMinSpeed) * normalized + chickenMinSpeed); 
    }

    new void Start() {
        // Debug.Log("Size: " + stats.GetAttribute(Attribute.Size));
        // Debug.Log("Size: " + getEffectiveSize(stats.GetAttribute(Attribute.Size)));
        // Debug.Log("localScale: " + transform.localScale);
        base.Start();
        // Debug.Log("localScale: " + transform.localScale);
        agent.speed = getEffectiveSpeed(stats.GetAttribute(Attribute.Speed)) * WorldController.TickSpeed;
        transformInicial = transform.localScale;
    }

    new void Update() {
        base.Update();
        transform.localScale = getEffectiveSize(stats.GetAttribute(Attribute.Size) * SizeWithAge()) * transformInicial;// * transform.localScale;
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

    public void Die()
    {
        base.Die();
    }
    
    public override void sleep() {
        throw new NotImplementedException();
    }

    public override void seeAround() {
        throw new NotImplementedException();
    }

    public override Species GetSpecies()
    {
        return Species.Chicken;
    }

    public override GameObject getBestWaterPosition() {
        List<GameObject> waters = worldController.GetWaterReferences();
        // Vector3 bestWater = new Vector3(Single.PositiveInfinity, Single.PositiveInfinity, Single.PositiveInfinity);
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
        // HashSet<GameObject> waters = worldController.agentSpawner.GetChickens();
        
        HashSet<Agent> grassSet = worldController.GetComponent<AgentSpawner>().GetSpecies(Species.Grass);
        float bestDistance = float.MaxValue;
        Agent result = null;
        foreach (Agent g in grassSet) {
            if (g == null || g.gameObject == null)
                continue;
            float dist = Vector3.Distance(this.transform.position, g.transform.position); 
            if (dist < bestDistance) {
                bestDistance = dist;
                result = g;
            }
        }
        return result;
    }

    public override Agent findMate()
    {
        HashSet<Agent> chickenSet = worldController.GetComponent<AgentSpawner>().GetSpecies(GetSpecies());
        float bestDistance = float.MaxValue;
        Agent result = null;
        foreach (Agent c in chickenSet) {
            if (c.Equals(this) || c.Equals(null) || c.gameObject.Equals(null))
                continue;
            if (c.GetAge() > 10f) {
                float dist = Vector3.Distance(this.transform.position, c.transform.position); 
                if (dist < bestDistance) {
                    bestDistance = dist;
                    result = c;
                }
            }
        }
        return result;
    }

    public override void reproduce() {
        throw new NotImplementedException();
    }
    
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }

    public override string ToString(){
        return "Chicken".ToString();
    }

}