using System;
using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

//https://numerics.mathdotnet.com/Matrix.html
public class AgentStats
{
    // Stats 
    private Dictionary<Attribute, float> atts = new Dictionary<Attribute, float>();
    private Dictionary<Need, float> needs = new Dictionary<Need, float>();
    
    // Factory class para matrices. 
    // Las matrices se crean a partir de métodos de la clase Matrix<T>.Build 
    // Ejemplo:  Matrix<T> foo( a1, a2, ..., an); 
    MatrixBuilder<double> M = Matrix<double>.Build;
    private Matrix<double> weights;


    // TODO: devuelve valores aleatorios 
    public AgentStats()
    {
        
    }
    
    // TODO: devuelve valores de padre y madre, con mutaciones
    public AgentStats(AgentStats p1, AgentStats p2)
    {
        
    } 

    // TODO: implement neural network 
    State nextState()
    {
        return State.LookForWater;
    }

}
