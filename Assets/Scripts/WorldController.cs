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
    }

    public List<GameObject> GetWaterReferences() {
        inorganicReferences.TryGetValue(Inorganic.Water, out var x);
        return x; 
    }

    public void SetWaters()
    {
        inorganicPrefabs.TryGetValue(Inorganic.Water, out var selectedPrefab);
        for (int i = 0; i < 25; i++) {
            Vector3 randomVector = new Vector3(Random.Range(-90f, 90f), 40, Random.Range(-90f, 90f)); 
            var raycasthit = Physics.Raycast(randomVector, Vector3.down, out var hit);
            Quaternion rot = Quaternion.LookRotation(hit.normal, Vector3.forward);
            GameObject reference = Instantiate(selectedPrefab, hit.point + hit.normal * 0.05f , rot, this.transform);
            inorganicReferences.TryGetValue(Inorganic.Water, out var x);
            x.Add(reference);
        }
    }
}
