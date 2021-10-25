using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class TrainingSpawner : MonoBehaviour
{
    public SerializableDictionary<Species, Agent> speciesPrefabs = new SerializableDictionary<Species, Agent>();
    private static Dictionary<Species, Agent> speciesPrefabsStatic = new Dictionary<Species, Agent>();

    private Dictionary<Species, List<Vector3>> NonMovableAgentsPositions = new Dictionary<Species, List<Vector3>>();
    private Dictionary<Species, HashSet<Agent>> InGameAgents = new Dictionary<Species, HashSet<Agent>>();

    void Start()
    {
        foreach (var keyValuePair in speciesPrefabsStatic)
            speciesPrefabs.Add(keyValuePair.Key, keyValuePair.Value);

        foreach (var s in speciesPrefabs)
            if (s.Value.GetComponent<NotMovableAgent>() != null)
                NonMovableAgentsPositions.Add(s.Key, new List<Vector3>());

        foreach (var s in speciesPrefabs.Keys)
            InGameAgents.Add(s, new HashSet<Agent>());

        // for (int i = 0; i < 1; i++)
        // {
        //     speciesPrefabs.TryGetValue(Species.Grass, out var selectedPrefab);
        //     if (selectedPrefab != null)
        //     {
        //         Agent reference = Instantiate(selectedPrefab, this.transform);
        //         reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Grass);
        //         reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
        //         InGameAgents.TryGetValue(Species.Grass, out var x);
        //         x.Add(reference);
        //     }
        // }

        // for (int i = 0; i < 1; i++)
        // {
        //     speciesPrefabs.TryGetValue(Species.Chicken, out var selectedPrefab);
        //     if (selectedPrefab != null)
        //     {
        //         Agent reference = Instantiate(selectedPrefab, this.transform);
        //         reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Chicken);
        //         reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
        //         InGameAgents.TryGetValue(Species.Chicken, out var x);
        //         x.Add(reference);
        //     }
        // }

        // for (int i = 0; i < 1; i++)
        // {
        //     speciesPrefabs.TryGetValue(Species.Fox, out var selectedPrefab);
        //     if (selectedPrefab != null)
        //     {
        //         Agent reference = Instantiate(selectedPrefab, this.transform);
        //         reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Fox);
        //         reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
        //         InGameAgents.TryGetValue(Species.Fox, out var x);
        //         x.Add(reference);
        //     }
        // }

        StartCoroutine(Train(Species.Chicken)); 
    }

    void Update() {}

    public static void AddSpecies(Species species, Agent agent) {
        speciesPrefabsStatic.Add(species, agent);
    }

    IEnumerator Populate()
    {
        while (true)
        {
            InGameAgents.TryGetValue(Species.Chicken, out var chickenSet);
            Agent chicken1 = chickenSet.ElementAt(Random.Range(0, chickenSet.Count));
            Agent chicken2 = chickenSet.ElementAt(Random.Range(0, chickenSet.Count));

            AgentStats ags = SpeciesFactory.NewAgentStats(chicken1.GetComponent<Agent>().stats, chicken2.GetComponent<Agent>().stats, Species.Chicken);


            speciesPrefabs.TryGetValue(Species.Chicken, out var selectedPrefab);
            Agent reference = Instantiate(selectedPrefab, this.transform);
            reference.GetComponent<Agent>().stats = ags;
            InGameAgents.TryGetValue(Species.Chicken, out var x);
            x.Add(reference);
            yield return new WaitForSeconds(5f / WorldController.TickSpeed);
        }
    }

    public HashSet<Agent> GetChickens() {
        InGameAgents.TryGetValue(Species.Chicken, out var chickenSet);
        return chickenSet;
    }

    public IEnumerator Train(Species species)
    {
        int iter = 0;
        while (true)
        {
            speciesPrefabs.TryGetValue(species, out var selectedPrefab);
            HashSet<AgentStats> spawn = new HashSet<AgentStats>();
            if (iter == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (selectedPrefab != null)
                    {
                        spawn.Add(TrainingSpeciesFactory.NewAgentStats(species));
                    }
                }
            }
            else {
                InGameAgents.TryGetValue(species, out var speciesNullSet);
                HashSet<Agent> speciesSet = new HashSet<Agent>();
                for (int k = 0; k < 10; k++)
                {
                    if (speciesNullSet.ElementAt(k) != null)
                        speciesSet.Add(speciesNullSet.ElementAt(k));
                }
                if (speciesSet.Count == 0)
                {
                    iter = 0;
                    continue;
                }
                for (int i = 0; i < 10; i++)
                {
                    int j = 0, k = 0;
                    Agent agent1 = null, agent2 = null;
                    while (agent1 == null)
                    {
                        k = Random.Range(0, speciesSet.Count);
                        agent1 = speciesSet.ElementAt(j);
                    }
                    while (agent2 == null)
                    {
                        k = Random.Range(0, speciesSet.Count);
                        agent2 = speciesSet.ElementAt(k);
                    }
                    if (selectedPrefab != null)
                    {   
                        // reference.GetComponent<Agent>().stats = 
                        spawn.Add(TrainingSpeciesFactory.NewAgentStats(agent1.stats, agent2.stats, species));
                    }
                }
                for (int i = 0; i < speciesSet.Count; i++)
                    if (i != null)
                        speciesSet.ElementAt(i).Die();
                InGameAgents[species] = new HashSet<Agent>();
            }
            for (int i = 0; i < spawn.Count; i++)
            {
                int x = Random.Range(-5, 5);
                int z = Random.Range(-5, 5);
                Vector3 where = new Vector3(x, 0, z);
                Agent reference = Instantiate(selectedPrefab, where, new Quaternion(), this.transform);
                reference.GetComponent<Agent>().stats = spawn.ElementAt(i);
                reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
                InGameAgents.TryGetValue(species, out var inGame);
                inGame.Add(reference);
            }
            iter++;
            yield return new WaitForSecondsRealtime((30f + 0.1f * iter)/ WorldController.TickSpeed);
//            for (int i = 0; i < speciesSet.Count; i++)
//                if (i != null)
//                    speciesSet.ElementAt(i).die();
//            InGameAgents[species] = new HashSet<Agent>();
        }
    }
}
