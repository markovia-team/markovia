using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldController : MonoBehaviour {
    #region TickSpeed
    public static float tickSpeed = 1.0f;
    public static float TickSpeed => tickSpeed;
    #endregion
    
    public SerializableDictionary<Inorganic, GameObject> inorganicPrefabs = new SerializableDictionary<Inorganic, GameObject>();
    public SerializableDictionary<Inorganic, GameObject> inorganicPrefabsDesert = new SerializableDictionary<Inorganic, GameObject>();

    private Dictionary<Inorganic, List<GameObject>> inorganicReferences = new Dictionary<Inorganic, List<GameObject>>();

    private void Start() {
        if (AgentSpawner.isDesert) inorganicPrefabs = inorganicPrefabsDesert; 

        foreach (var s in inorganicPrefabs)
            inorganicReferences.Add(s.Key, new List<GameObject>());
    }

    public List<GameObject> GetWaterReferences()
    {
            
        inorganicReferences.TryGetValue(Inorganic.Water, out var x);
        return x; 
    }

    public void NewWater(Vector3 pos)
    {
        inorganicPrefabs.TryGetValue(Inorganic.Water, out var selectedPrefab);
        Physics.Raycast(pos, Vector3.down, out var hit);
        Quaternion rot = Quaternion.LookRotation(hit.normal, Vector3.forward);
        pos = hit.point + hit.normal * 0.05f; 
        GameObject reference = Instantiate(selectedPrefab, pos, rot, this.transform);
        inorganicReferences.TryGetValue(Inorganic.Water, out var x);
        x.Add(reference);
    }

    public void SetWaters() {
        for (int i = 0; i < 25; i++) {
            Vector3 randomVector = new Vector3(Random.Range(-90f, 90f), 40, Random.Range(-90f, 90f));
            NewWater(randomVector);
            // // GameObject reference = Instantiate(selectedPrefab, hit.point + hit.normal * 0.05f , rot, this.transform);
            // inorganicReferences.TryGetValue(Inorganic.Water, out var x);
            // x.Add(reference);
        }
    }
}
