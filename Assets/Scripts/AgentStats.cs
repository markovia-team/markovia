using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

//https://numerics.mathdotnet.com/Matrix.html
public class AgentStats {

    // Stats 
    private SortedDictionary<Attribute, double> atts;
    private SortedDictionary<Need, double> needs;
    private SortedSet<State> states;
    
    // Factory class para matrices. 
    // Las matrices se crean a partir de métodos de la clase Matrix<T>.Build 
    // Ejemplo:  Matrix<T> foo( a1, a2, ..., an); 
    private MatrixBuilder<double> m = Matrix<double>.Build;
    private VectorBuilder<double> v = Vector<double>.Build; 
    private Matrix<double> weights;


    // TODO: devuelve valores aleatorios
    public AgentStats(SortedDictionary<Attribute, double> atts, SortedDictionary<Need, double> needs, SortedSet<State> states, Matrix<double> weights) {
        this.atts = atts;
        this.needs = needs;
        this.states = states;
        this.weights = weights;
    }

/*
    public AgentStats() {

    }
    
    // TODO: devuelve valores de padre y madre, con mutaciones
    public AgentStats(AgentStats p1, AgentStats p2) {
        
    } 
*/

    // TODO: implement neural network 
    public State nextState() {
        v.DenseOfEnumerable(atts.Values.Concat(needs.Values));
        return State.Stealth; 
    }
}
