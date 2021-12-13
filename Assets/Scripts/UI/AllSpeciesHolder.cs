using UnityEngine;
using UnityEngine.UI;

public class AllSpeciesHolder : MonoBehaviour {
    [SerializeField] private GameObject speciesHolderPrefab;
    [SerializeField] private SerializableDictionary<Species, Texture> speciesIcon = new SerializableDictionary<Species, Texture>();
    [SerializeField] private Canvas canvas;
    [SerializeField] private WorldController controller;

    void Awake() {
        var i = 0;
        foreach (var x in speciesIcon) {
            var go = Instantiate(speciesHolderPrefab, transform.position+new Vector3( (i % 2) * 120 * canvas.scaleFactor-20, (i/2)*120*canvas.scaleFactor-50),
                speciesHolderPrefab.transform.rotation, transform).transform.GetChild(0);
            var dad = go.GetComponent<DragAndDrop>();
            dad.agentSpawner = controller.GetComponent<AgentSpawner>();
            dad.canvas = canvas;
            dad.species = x.Key;
            dad.GetComponent<RawImage>().texture = x.Value;
            i++;
        }
    }
}