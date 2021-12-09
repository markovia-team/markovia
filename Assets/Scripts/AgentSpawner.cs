using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Runtime.Serialization;
using System.IO;
using SFB;
using UnityEngine.AI;

public class AgentSpawner : MonoBehaviour, ISerializable {
    private static Dictionary<Species, GameObject> speciesPrefabsStatic = new Dictionary<Species, GameObject>();
    private Dictionary<Species, List<Vector3>> NonMovableAgentsPositions = new Dictionary<Species, List<Vector3>>();
    private Dictionary<Species, HashSet<Agent>> InGameAgents = new Dictionary<Species, HashSet<Agent>>();
    public SerializableDictionary<Species, GameObject> speciesPrefabs = new SerializableDictionary<Species, GameObject>();
    public Dictionary<Species, HashSet<Agent>> gameAgents => InGameAgents;
    public GameObject popup;
    
    private Dictionary<Species, List<int>> valueLists = new Dictionary<Species, List<int>>()
    {
        {Species.Chicken, new List<int>()}, 
        {Species.Fox, new List<int>()}, 
        {Species.Grass, new List<int>()}
    };

    private List<int> averageChickenSpeed = new List<int>();

    void Awake() {}
    
    void Start() {
        foreach (var keyValuePair in speciesPrefabsStatic)
            speciesPrefabs.Add(keyValuePair.Key, keyValuePair.Value);

        foreach (var s in speciesPrefabs)
            if (s.Value.GetComponent<NotMovableAgent>() != null)
                NonMovableAgentsPositions.Add(s.Key, new List<Vector3>());

        foreach (var s in speciesPrefabs.Keys)
            InGameAgents.Add(s, new HashSet<Agent>());

        // for (int i = 0; i < 15; i++) {
        //     speciesPrefabs.TryGetValue(Species.Grass, out var selectedPrefab);
        //     if (selectedPrefab != null) {
        //         Vector3 vect = new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f));
        //         GameObject reference = Instantiate(selectedPrefab, vect, new Quaternion(0, 0, 0, 0), transform);
        //         reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Grass);
        //         reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
        //         InGameAgents.TryGetValue(Species.Grass, out var x);
        //         x.Add(reference.GetComponent<Agent>());
        //     }
        // }
        //
        // for (int i = 0; i < 10; i++) {
        //     speciesPrefabs.TryGetValue(Species.Chicken, out var selectedPrefab);
        //     if (selectedPrefab != null) {
        //         GameObject reference = Instantiate(selectedPrefab, this.transform);
        //         reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Chicken);
        //         reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
        //         InGameAgents.TryGetValue(Species.Chicken, out var x);
        //         x.Add(reference.GetComponent<Agent>());
        //     }
        // }
        //
        // for (int i = 0; i < 2; i++) {
        //     speciesPrefabs.TryGetValue(Species.Fox, out var selectedPrefab);
        //     if (selectedPrefab != null) {
        //         GameObject reference = Instantiate(selectedPrefab, Vector3.zero, selectedPrefab.transform.rotation,
        //             this.transform);
        //         reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Fox);
        //         reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
        //         InGameAgents.TryGetValue(Species.Fox, out var x);
        //         reference.GetComponent<MovableAgent>().agent.Warp(Vector3.zero);
        //         x.Add(reference.GetComponent<Agent>());
        //     }
        // }
        
        StartCoroutine(AddDataPoint());
        // StartCoroutine(Populate()); 
    }

    void Update() {}

    public static void AddSpecies(Species species, GameObject gameObject) {
        speciesPrefabsStatic.Add(species, gameObject);
    }
    
    public void AddSpecies(Species species, Vector3 position) {
        speciesPrefabs.TryGetValue(species, out var selectedPrefab);
        if (selectedPrefab != null) {
            // GameObject reference = Instantiate(selectedPrefab, position, selectedPrefab.transform.rotation, transform);
            GameObject reference = Instantiate(selectedPrefab, position, Quaternion.identity, transform);
            Debug.Log(reference.name + " " + species);
            reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(species);
            reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
            InGameAgents.TryGetValue(species, out var x);
            x.Add(reference.GetComponent<Agent>());
        }
    }

    public HashSet<Agent> GetSpecies(Species species) {
        InGameAgents.TryGetValue(species, out var ansSet);
        return ansSet;
    }

    public HashSet<Agent> GetChickens() {
        InGameAgents.TryGetValue(Species.Chicken, out var ansSet);
        return ansSet;
    }

    public void Died(Agent agent, Species species) {
        InGameAgents.TryGetValue(species, out var set);
        set.Remove(agent);
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
        int id = 0;
        foreach (KeyValuePair<Species, HashSet<Agent>> entry in InGameAgents) {
            foreach (Agent obj in entry.Value) {
                string idStr = id.ToString();
                info.AddValue("Species" + idStr, entry.Key);
                id++;
            }
        }
    }

    public void WriteData() {
        int id = 0, count = 0;
        GameData currentData = new GameData();
        foreach (KeyValuePair<Species, HashSet<Agent>> entry in InGameAgents) {
            count += entry.Value.Count;
        }

        foreach (KeyValuePair<Species, HashSet<Agent>> entry in InGameAgents) {
            foreach (Agent obj in entry.Value) {
                currentData.addAgent(obj.gameObject);
            }
        }

        var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "json");
        if (string.Compare(path, string.Empty, StringComparison.Ordinal) == 0) {
            popup.GetComponentInChildren<TMPro.TMP_Text>().text = "You must enter a name";
            popup.SetActive(true);
            return;
        }

        popup.GetComponentInChildren<TMPro.TMP_Text>().text = !JsonManager.SaveToJson(path, currentData) ? "File is read-only!" : "File saved successfully";
        if (!path.EndsWith(".json")) {
            File.Move(path, path + ".json");
        }

        popup.SetActive(true);
    }

    // IEnumerator Populate() {
    //     while (true) {
    //         InGameAgents.TryGetValue(Species.Chicken, out var chickenSet);
    //         GameObject chicken1 = chickenSet.ElementAt(Random.Range(0, chickenSet.Count));
    //         GameObject chicken2 = chickenSet.ElementAt(Random.Range(0, chickenSet.Count));
    //         
    //         AgentStats ags = SpeciesFactory.NewAgentStats(chicken1.GetComponent<Agent>().stats, chicken2.GetComponent<Agent>().stats, Species.Chicken);
    //         
    //         speciesPrefabs.TryGetValue(Species.Chicken, out var selectedPrefab); 
    //         GameObject reference = Instantiate(selectedPrefab, this.transform);
    //         reference.GetComponent<Agent>().stats = ags;
    //         reference.GetComponent<MovableAgent>().agent.Warp(new Vector3(0, 24, 0)); 
    //         InGameAgents.TryGetValue(Species.Chicken, out var x);
    //         x.Add(reference);
    //         
    //         Debug.Log("CHICK00EN SPEED    "+ags.GetAttribute(Attribute.Speed));
    //         yield return new WaitForSeconds(1f);
    //     }
    // }

    public IEnumerator AddDataPoint() {
        while (true) {
            foreach (var x in valueLists) {
                InGameAgents.TryGetValue(x.Key, out var set);
                valueLists.TryGetValue(x.Key, out var list);
                if (set == null) {
                    list.Add(0);
                    continue;
                }
                list.Add(set.Count);
                if (x.Key.Equals(Species.Chicken)) {
                    double speedSum = 0;
                    foreach (var k in set) {
                        speedSum += k.GetComponent<Agent>().stats.GetAttribute(Attribute.Speed); 
                    }

                    int i = (int) (speedSum * 100.0 / (1.0 * set.Count)); 
                    averageChickenSpeed.Add(i);
                }
            }
            yield return new WaitForSeconds(3f); 
        }
    }

    public List<int> FetchDataPoints(Species species) {
        valueLists.TryGetValue(species, out var list);
        return list; 
    }
    
    public List<int> FetchAverageChickenSpeedList() {
        Debug.Log(averageChickenSpeed);
        return averageChickenSpeed; 
    }
    
    
    public List<int> FetchChickenSizeDataPoints()
    {
        var sizes = new List<int>();
        for (int i=0; i<10; i++) sizes.Add(0);
        InGameAgents.TryGetValue(Species.Chicken, out var chickSet);
        foreach (var c in chickSet)
        {
            int i = (int) (c.GetComponent<Agent>().stats.GetAttribute(Attribute.Size) / 0.1); 
            if ( i < 10) sizes[i] ++; 
        }

        string s = "CHICK00";
        for (int i = 0; i < 10; i++)
            s = s + "\n CLASS" + i + ":    " + sizes[i];
        Debug.Log(s);
            
        return sizes; 
    }
    
    public void Reproduce(Agent ag1, Agent ag2, Species species) {
        AgentStats ags = SpeciesFactory.NewAgentStats(ag1.stats, ag2.stats, species);
        ags.SetNeed(Need.ReproductiveUrge, 0f);
        speciesPrefabs.TryGetValue(species, out var selectedPrefab);
        GameObject reference = Instantiate(selectedPrefab, ag1.transform.position, ag1.transform.rotation, transform);
        reference.GetComponent<Agent>().stats = ags;
        reference.GetComponent<Agent>().worldController = ag1.worldController;
        InGameAgents.TryGetValue(species, out var x);
        x.Add(reference.GetComponent<Agent>());
    }

    public void AsexualReproduce(Agent ag1, Species species) {
        AgentStats ags = SpeciesFactory.NewAgentStats(ag1.stats, ag1.stats, species);
        speciesPrefabs.TryGetValue(species, out var selectedPrefab);
        Vector3 pos = ag1.transform.position;
        
        Vector3 randomVector = new Vector3(pos.x + Random.Range(-3f, 3f), 40, pos.z + Random.Range(-3f, 3f));

        var raycasthit = Physics.Raycast(randomVector, Vector3.down, out var hit);
        if (raycasthit)
        {
            GameObject reference = Instantiate(selectedPrefab, hit.point, ag1.transform.rotation, transform);
            reference.GetComponent<Agent>().stats = ags;
            reference.GetComponent<Agent>().worldController = ag1.worldController;
            InGameAgents.TryGetValue(species, out var x);
            x.Add(reference.GetComponent<Agent>());
        }
    }
}
