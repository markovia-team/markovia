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
    
    // Neural network stuff 
    private int qAtts;
    private int qNeeds;
    private int qStates; 
    private Vector<double> neuralInput;
    private Vector<double> neuralOutput; 
    
    // Factory class para matrices. 
    // Las matrices se crean a partir de métodos de la clase Matrix<T>.Build 
    // Ejemplo:  Matrix<T> foo( a1, a2, ..., an); 
    private MatrixBuilder<double> m = Matrix<double>.Build;
    private VectorBuilder<double> v = Vector<double>.Build; 
    private Matrix<double> weights;
    
    public AgentStats(SortedDictionary<Attribute, double> atts, SortedDictionary<Need, double> needs, SortedSet<State> states, Matrix<double> weights) {
        this.atts = atts;
        this.needs = needs;
        this.states = states;
        this.weights = weights;
        this.qAtts = atts.Count;
        this.qNeeds = needs.Count;
        this.qStates = states.Count; 
        
        neuralInput = v.Dense(qAtts + qNeeds);
        neuralOutput = v.Dense(qStates); 
        
        // Fill values that will never change in the input vector
        var i = 0;
        foreach (var pair in atts) neuralInput[i++] = pair.Value;
    }
/*   
    // TODO: devuelve valores de padre y madre, con mutaciones
    public AgentStats(AgentStats p1, AgentStats p2) {
        
    } 
*/
    public State nextState() {
        
        // Fill values that change in the input vector
        var i = qAtts;
        foreach (var pair in needs) neuralInput[i++] = pair.Value;
        
        // Multiply matrix weights by input vector. "Neural" step.
        weights.Multiply(neuralInput, neuralOutput);
        
        var j = 0;
        double maxValue = 0;
        var maxj = -1; 
        foreach (var value in neuralOutput) {
            if (maxValue < value) {
                maxValue = value;
                maxj = j; 
            }
            j++; 
        }
             
        // Recover state from Set
        // TODO: not efficient, it may be better to use an ArrayList and sort it. Getting the 'nth' element is O(n) in SortedSet 
        return states.ElementAt(maxj);
    }
}
