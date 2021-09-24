using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

public static class SpeciesFactory {

    private static readonly Dictionary<Species, SortedSet<Attribute>> spec_att = new Dictionary<Species, SortedSet<Attribute>>() {    
        { Species.Chicken, new SortedSet<Attribute>() { Attribute.Speed, Attribute.Size } },
        { Species.Grass, new SortedSet<Attribute>() { Attribute.Size } },
        { Species.Fox, new SortedSet<Attribute>() { Attribute.Speed, Attribute.Size } }
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
    private static readonly Dictionary<Species, SortedDictionary<Attribute, double>> default_attributes;

    public static AgentStats NewAgentStats(Species species) {
        Matrix<double> aux_mat = null;
        SortedDictionary<Attribute, double> aux_att = null;
        SortedSet<Need> aux_needs = null; 
        SortedSet<State> aux_state = null;

        spec_needs.TryGetValue(species, out aux_needs);
        default_attributes.TryGetValue(species, out aux_att);
        spec_states.TryGetValue(species, out aux_state);
        default_weights.TryGetValue(species, out aux_mat);

        SortedDictionary<Need, double> needs_aux = new SortedDictionary<Need, double>();
        foreach (Need need in aux_needs) {
            needs_aux.Add(need, 0);
        }

        SortedDictionary<Attribute, double> atts_aux = new SortedDictionary<Attribute, double>(); 
        foreach (KeyValuePair<Attribute, double> kvp in aux_att)
        {
            atts_aux.Add(kvp.Key, kvp.Value);
        }

        return new AgentStats(atts_aux, needs_aux, aux_state, aux_mat);
    }

    public static AgentStats NewAgentStats(AgentStats p1, AgentStats p2) {
        return null;
    }
}