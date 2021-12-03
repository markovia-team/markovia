using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.Distributions;
using Random = UnityEngine.Random;

public class GrassController : NotMovableAgent
{
    public override void drink() {
        throw new System.NotImplementedException();
    }

    public override void eat() {
        throw new System.NotImplementedException();
    }

    public override void sleep() {
        throw new System.NotImplementedException();
    }

    public override void seeAround() {
        throw new System.NotImplementedException();
    }

    public override GameObject getBestWaterPosition()
    {
        throw new NotImplementedException();
    }

    public override GameObject getBestFoodPosition()
    {
        throw new NotImplementedException();
    }

    private double getLambdaRate() {
        return 1; 
    }
    
    public override Species GetSpecies()
    {
        return Species.Grass;
    }

    public override Agent findMate()
    {
        return this;
    }

    public override void reproduce()
    {
        float distSon = (float)(new Exponential(getLambdaRate()).Sample());
        float angSon = Random.Range(0f, (float)(2*Math.PI));

        Vector3 sonPos = new Vector3(transform.position.x+distSon*((float)Math.Cos(angSon)), transform.position.y, transform.position.z+distSon*((float)Math.Sin(angSon)));
        // Instantiate(this.gameObject, sonPos, this.transform.rotation); 
    }
}