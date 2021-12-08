using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.Distributions;
using Random = UnityEngine.Random;
using System.Runtime.Serialization;

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

    public override void reproduce()
    {
        throw new NotImplementedException();
    }

    public override void seeAround() {
        throw new System.NotImplementedException();
    }

    public override GameObject getBestWaterPosition()
    {
        throw new NotImplementedException();
    }

    public override Agent getBestFoodPosition()
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

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }

    public override string ToString(){
        return "Grass".ToString();
    }
}