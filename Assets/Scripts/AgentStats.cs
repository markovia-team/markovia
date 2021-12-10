using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

public class AgentStats {
    public Matrix<double> weights;
    public SortedDictionary<Attribute, double> Atts => atts;
    public SortedDictionary<Need, double> Needs => needs;
    private SortedDictionary<Attribute, double> atts;
    private SortedDictionary<Need, double> needs;
	private SortedDictionary<Distance, double> distances;
    private SortedSet<State> states;
    private int qAtts;
    private int qNeeds;
	private int qDistances;
    private int qStates;
    private Vector<double> neuralInput;
    private Vector<double> neuralOutput;
    private MatrixBuilder<double> m = Matrix<double>.Build;
    private VectorBuilder<double> v = Vector<double>.Build;

    public override string ToString() {
        return "ATTS:\n" + string.Join(Environment.NewLine, atts) + "\n\nNEEDS:\n" + string.Join(Environment.NewLine, needs) + "\n\nSTATES:\n" + string.Join(Environment.NewLine, states); 
    }

    public double GetAttribute(Attribute att) {
        if (atts.TryGetValue(att, out var d)) return d; 
        return 0.5f;
    }

    public AgentStats(SortedDictionary<Attribute, double> atts, SortedDictionary<Need, double> needs, SortedDictionary<Distance, double> distances, SortedSet<State> states, Matrix<double> weights) {
        this.atts = atts;
        this.needs = needs;
        this.states = states;
        this.weights = weights;
		this.distances = distances;
        qAtts = atts.Count;
        qNeeds = needs.Count;
        qStates = states.Count;
		qDistances = distances.Count;

        neuralInput = v.Dense(qAtts + qDistances + qNeeds);
        neuralOutput = v.Dense(qStates);
        
        var i = 0;
        foreach (var pair in atts) neuralInput[i++] = pair.Value;
    }

    public void UpdateNeed(Need need, float increment) {
        needs.TryGetValue(need, out var a);
        double value = a + increment;
        if (value < 0f)
            value = 0f;
        if (value > 1f)
            value = 1f;
        needs[need] = value;
    }

    public void SetNeed(Need need, float value) {
        if (value < 0f || value > 1f)
            return;
        needs.TryGetValue(need, out var a);
        needs[need] = value;
    }

    public void SetDistance(Distance distance, float value) {
        if (value < 0f)
            return;
        distances.TryGetValue(distance, out var a);
        distances[distance] = value;
    }

    public Matrix<double> GetWeights() {
        return weights;
    }

    public State NextState() {
        var i = qAtts;
		foreach (var pair in distances) neuralInput[i++] = pair.Value;
		foreach (var pair in needs) neuralInput[i++] = pair.Value;
        
        weights.Multiply(neuralInput, neuralOutput);
        
        var j = 0;
        double maxValue = 0;
        var maxJ = -1;
        foreach (var value in neuralOutput) {
            if (maxValue < value) {
                maxValue = value;
                maxJ = j;
            }
            j++;
        }
        
        return maxJ >= 0 ? states.ElementAt(maxJ) : State.Idle;
    }

    public double GetNeed(Need need) {
        needs.TryGetValue(need, out var ans);
        return ans;
    }
}
