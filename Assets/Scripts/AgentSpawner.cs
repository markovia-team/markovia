using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class AgentSpawner : MonoBehaviour
{
    // Source for the serializable dictionary: 
    // https://wiki.unity3d.com/index.php/File:SerializableDictionary.zip
    public SerializableDictionary<Species, GameObject> speciesPrefabs = new SerializableDictionary<Species, GameObject>();

    
    private Dictionary<Species, List<Vector3>> NonMovableAgentsPositions = new Dictionary<Species, List<Vector3>>();  
    
    // Start is called before the first frame update
    void Start()
    {
        foreach ( var s in speciesPrefabs )
            if ( s.Value.GetComponent<NotMovableAgent>() != null )
                NonMovableAgentsPositions.Add(s.Key, new List<Vector3>());
        StartCoroutine(Populate()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Populate()
    {
        while (true)
        {
            speciesPrefabs.TryGetValue(Species.Chicken, out var selectedPrefab); 
            var reference = Instantiate(selectedPrefab, this.transform);
           //  reference.GetComponent<Agent>().stats = SpeciesFactory.NewAgentStats(Species.Chicken); 
            yield return new WaitForSeconds(5f); 
        }
    }
    

}
