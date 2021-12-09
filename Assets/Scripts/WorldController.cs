using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldController : MonoBehaviour {
    #region TickSpeed
    public static float tickSpeed = 1.0f;
    public static float TickSpeed => tickSpeed;
    #endregion
    
    public SerializableDictionary<Inorganic, GameObject> inorganicPrefabs = new SerializableDictionary<Inorganic, GameObject>();
    private Dictionary<Inorganic, List<GameObject>> inorganicReferences = new Dictionary<Inorganic, List<GameObject>>();

    private void Start() {
        foreach (var s in inorganicPrefabs)
            inorganicReferences.Add(s.Key, new List<GameObject>());
        
        for (int i = 0; i < 1; i++) {
            inorganicPrefabs.TryGetValue(Inorganic.Water, out var selectedPrefab); 
            GameObject reference = Instantiate(selectedPrefab, new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f)), selectedPrefab.transform.rotation, this.transform);
            inorganicReferences.TryGetValue(Inorganic.Water, out var x);
            x.Add(reference);
        }
    }

    public List<GameObject> GetWaterReferences() {
        inorganicReferences.TryGetValue(Inorganic.Water, out var x);
        return x; 
    }
}
