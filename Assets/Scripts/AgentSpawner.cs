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
    private Dictionary<Species, HashSet<GameObject>> InGameAgents = new Dictionary<Species, HashSet<GameObject>>(); 
    
    void Start() {
        foreach (var keyValuePair in speciesPrefabsStatic)
            speciesPrefabs.Add(keyValuePair.Key, keyValuePair.Value);
        
        foreach (var s in speciesPrefabs)
            if (s.Value.GetComponent<NotMovableAgent>() != null )
                NonMovableAgentsPositions.Add(s.Key, new List<Vector3>());
        
        foreach (var s in speciesPrefabs.Keys)
            InGameAgents.Add(s, new HashSet<GameObject>());
        
        // for (int i = 0; i < 1; i++) {
        //     speciesPrefabs.TryGetValue(Species.Grass, out var selectedPrefab);
        //     if (selectedPrefab != null) {
        //         GameObject reference = Instantiate(selectedPrefab, this.transform);
        //         reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Grass);
        //         reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
        //         InGameAgents.TryGetValue(Species.Chicken, out var x);
        //         x.Add(reference);
        //     }
        // }

        for (int i = 0; i < 1; i++) {
            speciesPrefabs.TryGetValue(Species.Chicken, out var selectedPrefab);
            if (selectedPrefab != null) {
                GameObject reference = Instantiate(selectedPrefab, this.transform);
                reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Chicken);
                reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
                InGameAgents.TryGetValue(Species.Chicken, out var x);
                x.Add(reference);
            }
        }

        for (int i = 0; i < 1; i++) {
            speciesPrefabs.TryGetValue(Species.Fox, out var selectedPrefab);
            if (selectedPrefab != null) {
                GameObject reference = Instantiate(selectedPrefab, this.transform);
                reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Fox);
                reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
                InGameAgents.TryGetValue(Species.Fox, out var x);
                x.Add(reference);
            }
        }

        //StartCoroutine(Populate()); 
    }

    void Update() {}

    public static void AddSpecies(Species species, GameObject gameObject) {
        speciesPrefabsStatic.Add(species, gameObject);
    }

    public HashSet<GameObject> GetChickens() {
        InGameAgents.TryGetValue(Species.Chicken, out var chickenSet);
        return chickenSet;
    }

    IEnumerator Populate()
    {
        while (true)
        {
            InGameAgents.TryGetValue(Species.Chicken, out var chickenSet);
            GameObject chicken1 = chickenSet.ElementAt(Random.Range(0, chickenSet.Count));
            GameObject chicken2 = chickenSet.ElementAt(Random.Range(0, chickenSet.Count));
            
            AgentStats ags = SpeciesFactory.NewAgentStats(chicken1.GetComponent<Agent>().stats, chicken2.GetComponent<Agent>().stats, Species.Chicken);

            
            speciesPrefabs.TryGetValue(Species.Chicken, out var selectedPrefab); 
            GameObject reference = Instantiate(selectedPrefab, this.transform);
            reference.GetComponent<Agent>().stats = ags; 
            InGameAgents.TryGetValue(Species.Chicken, out var x);
            x.Add(reference);
            yield return new WaitForSeconds(5f/WorldController.TickSpeed); 
        }
    }
}
