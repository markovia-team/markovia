using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using System.Runtime.Serialization;
using System.IO;
using System.Text;


public class AgentSpawner : MonoBehaviour, ISerializable
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
        
        for (int i = 0; i < 1; i++) {
            speciesPrefabs.TryGetValue(Species.Grass, out var selectedPrefab);
            if (selectedPrefab != null) {
                GameObject reference = Instantiate(selectedPrefab, this.transform);
                reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Grass);
                reference.GetComponent<Agent>().worldController = GetComponent<WorldController>();
                InGameAgents.TryGetValue(Species.Grass, out var x);
                x.Add(reference);
            }
        }

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
        // string filePath = "/home/sal/Documents/ITBA/ITBAGit/inge/markovia/Assets/Scripts/AgentSpawnerFile";
        //File.Create(filePath);
        // WriteData(filePath);
        WriteData();
        
        SaveToJson();
        ReadFromJson();
        
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

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
        int id = 0;
        foreach (KeyValuePair<Species, HashSet<GameObject>> entry in InGameAgents) {
            foreach (GameObject obj in entry.Value) {
                string idStr = id.ToString();
                info.AddValue("Species" + idStr, entry.Key);
                id++;
            }
        }
    }

	public void WriteData() {
		int id = 0, count = 0;
		List<string> lines = new List<string>();
        foreach (KeyValuePair<Species, HashSet<GameObject>> entry in InGameAgents) {
            count += entry.Value.Count;
        }
        lines.Add(count.ToString());
        foreach (KeyValuePair<Species, HashSet<GameObject>> entry in InGameAgents) {
            foreach (GameObject obj in entry.Value) {
                lines.Add(((int) entry.Key).ToString());
                id++;
            }
        }
        File.WriteAllLines(Application.dataPath + "/Scripts/AgentSpawnerFile.txt", lines);
	}

    // ---------- Ejemplo ----------
    
    private void SaveToJson() {
        // Example
        Holder holder = new Holder();
        holder.count = 2;
        holder.text = "Hola Chalva";

        string textJson = JsonUtility.ToJson(holder);
        File.WriteAllText(Application.dataPath + "/Scripts/AgentSpawnerFile.json", textJson);
    }

    private void ReadFromJson() {
        string textJson = File.ReadAllText(Application.dataPath + "/Scripts/AgentSpawnerFile.json");
        Holder holder = JsonUtility.FromJson<Holder>(textJson);
        
        Debug.Log(holder.count + " " + holder.text);
    }

    private class Holder {
        public int count;
        public String text;
    }
    
    // ------------------------

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
