using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class AgentSpawner : MonoBehaviour
{
    // Source for the serializable dictionary: 
    // https://wiki.unity3d.com/index.php/File:SerializableDictionary.zip
    public SerializableDictionary<Species, GameObject> speciesPrefabs = new SerializableDictionary<Species, GameObject>();
    private static Dictionary<Species, GameObject> speciesPrefabsStatic = new Dictionary<Species, GameObject>();

    // TODO: en realidad estas posiciones pueden calcularse a partir de OnGameAgents pero mepa que es mejor tenerlas aparte
    private Dictionary<Species, List<Vector3>> NonMovableAgentsPositions = new Dictionary<Species, List<Vector3>>();
    private Dictionary<Species, HashSet<Agent>> InGameAgents = new Dictionary<Species, HashSet<Agent>>();

    public Dictionary<Species, HashSet<Agent>> gameAgents {
        get => InGameAgents;
    }

    void Start() {
        foreach (var keyValuePair in speciesPrefabsStatic)
            speciesPrefabs.Add(keyValuePair.Key, keyValuePair.Value);
        
        foreach (var s in speciesPrefabs)
            if (s.Value.GetComponent<NotMovableAgent>() != null )
                NonMovableAgentsPositions.Add(s.Key, new List<Vector3>());
        
        foreach (var s in speciesPrefabs.Keys)
            InGameAgents.Add(s, new HashSet<Agent>());
        
        for (int i = 0; i < 15; i++) {
            speciesPrefabs.TryGetValue(Species.Grass, out var selectedPrefab);
            if (selectedPrefab != null)
            {
                Vector3 vect = new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f));
                GameObject reference = Instantiate(selectedPrefab, vect, new Quaternion(0,0,0,0));
                reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Grass);
                reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
                InGameAgents.TryGetValue(Species.Grass, out var x);
                x.Add(reference.GetComponent<Agent>());
            }
        }

        for (int i = 0; i < 10; i++) {
            speciesPrefabs.TryGetValue(Species.Chicken, out var selectedPrefab);
            if (selectedPrefab != null) {
                GameObject reference = Instantiate(selectedPrefab, this.transform);
                reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Chicken);
                reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
                InGameAgents.TryGetValue(Species.Chicken, out var x);
                x.Add(reference.GetComponent<Agent>());
            }
        }

        for (int i = 0; i < 2; i++) {
            speciesPrefabs.TryGetValue(Species.Fox, out var selectedPrefab);
            if (selectedPrefab != null) {
                GameObject reference = Instantiate(selectedPrefab, this.transform);
                reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Fox);
                reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
                InGameAgents.TryGetValue(Species.Fox, out var x);
                x.Add(reference.GetComponent<Agent>());
            }
        }

        //StartCoroutine(Populate()); 
    }

    void Update() {}

    public static void AddSpecies(Species species, GameObject gameObject) {
        speciesPrefabsStatic.Add(species, gameObject);
    }

    public HashSet<Agent> GetSpecies(Species species)
    {
        InGameAgents.TryGetValue(species, out var ansSet);
        return ansSet;
    }
    
    public HashSet<Agent> GetChickens()
    {
        InGameAgents.TryGetValue(Species.Chicken, out var ansSet);
        return ansSet;
    }

    public void Died(Agent agent, Species species)
    {
        InGameAgents.TryGetValue(species, out var set);
        set.Remove(agent);
        // InGameAgents.Add(species, set);
    }

    IEnumerator Populate()
    {
        while (true)
        {
            InGameAgents.TryGetValue(Species.Chicken, out var chickenSet);
            Agent chicken1 = chickenSet.ElementAt(Random.Range(0, chickenSet.Count));
            Agent chicken2 = chickenSet.ElementAt(Random.Range(0, chickenSet.Count));
            
            AgentStats ags = SpeciesFactory.NewAgentStats(chicken1.stats, chicken2.stats, Species.Chicken);

            
            speciesPrefabs.TryGetValue(Species.Chicken, out var selectedPrefab); 
            GameObject reference = Instantiate(selectedPrefab, this.transform);
            reference.GetComponent<Agent>().stats = ags; 
            InGameAgents.TryGetValue(Species.Chicken, out var x);
            x.Add(reference.GetComponent<Agent>());
            yield return new WaitForSeconds(5f/WorldController.TickSpeed); 
        }
    }

    public void Reproduce(Agent ag1, Agent ag2, Species species)
    {
        // Debug.Log("-- REPRODUCE --");
        AgentStats ags = SpeciesFactory.NewAgentStats(ag1.stats, ag2.stats, species);
        ags.SetNeed(Need.ReproductiveUrge, 0f);

        speciesPrefabs.TryGetValue(species, out var selectedPrefab); 
        GameObject reference = Instantiate(selectedPrefab, ag1.transform.position, ag1.transform.rotation, transform);
        reference.GetComponent<Agent>().stats = ags;
        reference.GetComponent<Agent>().worldController = ag1.worldController;
        InGameAgents.TryGetValue(species, out var x);
        x.Add(reference.GetComponent<Agent>());
    }
    
    public void AsexualReproduce(Agent ag1, Species species)
    {
        AgentStats ags = SpeciesFactory.NewAgentStats(ag1.stats, ag1.stats, species);
        speciesPrefabs.TryGetValue(species, out var selectedPrefab);
        Vector3 pos = ag1.transform.position;
        Vector3 randomVector = new Vector3(pos.x + Random.Range(-3f, 3f), pos.y, pos.z + Random.Range(-3f, 3f)); 
        GameObject reference = Instantiate(selectedPrefab, randomVector, ag1.transform.rotation);
        reference.GetComponent<Agent>().stats = ags;
        reference.GetComponent<Agent>().worldController = ag1.worldController;
        InGameAgents.TryGetValue(species, out var x);
        x.Add(reference.GetComponent<Agent>());
    }
}
