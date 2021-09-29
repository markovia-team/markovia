using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

public static class SpeciesFactory {
    
    private static readonly Dictionary<Species, SortedDictionary<Attribute, double>> spec_atts = new Dictionary<Species, SortedDictionary<Attribute, double>>() {
        { Species.Chicken, new SortedDictionary<Attribute, double>() {
            {Attribute.Speed, 0.5f},
            {Attribute.Size, 0.5f},
            {Attribute.Color, 0.5f},
        } },
        { Species.Grass, new SortedDictionary<Attribute, double>() {
            {Attribute.Size, 0.5f}
        } },
        { Species.Fox, new SortedDictionary<Attribute, double>() {
            {Attribute.Speed, 0.5f},
            {Attribute.Size, 0.5f}
        } }
    };
    
    private static readonly Dictionary<Species, SortedSet<Need>> spec_needs = new Dictionary<Species, SortedSet<Need>>() {
        { Species.Chicken, new SortedSet<Need>() { Need.Sleep, Need.Hunger, Need.ReproductiveUrge } },
        { Species.Grass, new SortedSet<Need>() { Need.ReproductiveUrge } },
        { Species.Fox, new SortedSet<Need>() { Need.Sleep, Need.Hunger, Need.ReproductiveUrge } }
    };

    private static readonly Dictionary<Species, SortedSet<State>> spec_states = new Dictionary<Species, SortedSet<State>>() {    
        { Species.Chicken, new SortedSet<State>() { State.LookForFood, State.LookForWater, State.Stealth, State.Idle } },
        { Species.Grass, new SortedSet<State>() { State.Idle } },
        { Species.Fox, new SortedSet<State>() { State.LookForFood, State.LookForWater, State.Stealth, State.Idle } }
    };
    
    private static readonly Dictionary<Species, Matrix<double>> default_weights;
    
    public static AgentStats NewAgentStats(Species species) {
        spec_needs.TryGetValue(species, out var baseNeeds);
        spec_atts.TryGetValue(species, out var baseAtts);
        spec_states.TryGetValue(species, out var baseStates);
        //`default_weights.TryGetValue(species, out var aux_mat);
        
        SortedDictionary<Need, double> needsAux = new SortedDictionary<Need, double>();
        if (baseNeeds != null)
            foreach (Need need in baseNeeds)
                needsAux.Add(need, 0);

        SortedDictionary<Attribute, double> attsAux = new SortedDictionary<Attribute, double>();
        if (baseAtts != null)
            foreach (KeyValuePair<Attribute, double> kvp in baseAtts)
                attsAux.Add(kvp.Key, kvp.Value);
        
        return new AgentStats(attsAux, needsAux, baseStates, null);//aux_mat);
    }

    public static AgentStats NewAgentStats(AgentStats p1, AgentStats p2, Species species) {
        
        // Get a reference to species' States
        spec_states.TryGetValue(species, out var baseStates);

        // Fill up needs in zero 
        spec_needs.TryGetValue(species, out var baseNeeds);
        SortedDictionary<Need, double> needsAux = new SortedDictionary<Need, double>();
        if (baseNeeds != null)
            foreach (Need need in baseNeeds)
                needsAux.Add(need, 0);
        
        // Fill attributes from parents. TODO: ameliorate random distribution. Species should contain a MUTABILITY constant value in a separate dictionary 
        SortedDictionary<Attribute, double> attsAux = new SortedDictionary<Attribute, double>();
        foreach (KeyValuePair<Attribute, double> kvp in p1.Atts)
            if ( Random.Range(0f, 1f) < 0.4f ) // Reemplazar por mutabilidad
                attsAux.Add(kvp.Key, Random.Range(0f, 1f));
            else if (Random.Range(0f, 1f) < 0.5)
                attsAux.Add(kvp.Key, kvp.Value);
            else { 
                p2.Atts.TryGetValue(kvp.Key, out var p2att);
                attsAux.Add(kvp.Key, p2att);
            }

        // Mix-and-match reactance matrix
        return new AgentStats(attsAux, needsAux, baseStates, null);//aux_mat);
        
    }
}