using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldController : MonoBehaviour
{
    // Lo meti en una region para organizar un poco el codigo 
    #region TickSpeed
    public static float tickSpeed = 1.0f;

    public static float TickSpeed
    {
        get => tickSpeed;
    }
    #endregion
    
    // Source for the serializable dictionary: 
    // https://wiki.unity3d.com/index.php/File:SerializableDictionary.zip
    public SerializableDictionary<Inorganic, GameObject> inorganicPrefabs = new SerializableDictionary<Inorganic, GameObject>();
    private Dictionary<Inorganic, List<GameObject>> inorganicReferences = new Dictionary<Inorganic, List<GameObject>>();
    public SerializableDictionary<Organic, GameObject> organicPrefabs = new SerializableDictionary<Organic, GameObject>();
    private Dictionary<Organic, List<GameObject>> organicReferences = new Dictionary<Organic, List<GameObject>>();

    private void Start()
    {
        // Initialized list based on inorganicPrefabs
        foreach ( var s in inorganicPrefabs )
            inorganicReferences.Add(s.Key, new List<GameObject>());
        
        
        for (int i = 0; i < 1; i++) {
            inorganicPrefabs.TryGetValue(Inorganic.Water, out var selectedPrefab); 
            GameObject reference = Instantiate(selectedPrefab, new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f)), selectedPrefab.transform.rotation, this.transform);
            inorganicReferences.TryGetValue(Inorganic.Water, out var x);
            x.Add(reference);
        }

        foreach ( var s in organicPrefabs )
            organicReferences.Add(s.Key, new List<GameObject>());

        for (int i = 0; i < 1; i++) {
            organicPrefabs.TryGetValue(Organic.Food, out var selectedPrefab); 
            GameObject reference = Instantiate(selectedPrefab, new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f)), selectedPrefab.transform.rotation, this.transform);
            organicReferences.TryGetValue(Organic.Food, out var x);
            x.Add(reference);
        }
    }

    public List<GameObject> GetWaterReferences() {
        inorganicReferences.TryGetValue(Inorganic.Water, out var x);
        return x; 
    }

    public List<GameObject> GetFoodReferences() {
        organicReferences.TryGetValue(Organic.Food, out var x);
        return x; 
    }
}
