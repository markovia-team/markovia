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
            var go = Instantiate(speciesHolderPrefab, new Vector2(120 + i * 120 * canvas.scaleFactor, 100), speciesHolderPrefab.transform.rotation, transform).transform.GetChild(0);
            var dad = go.GetComponent<DragAndDrop>();
            dad.agentSpawner = controller.GetComponent<AgentSpawner>();
            dad.canvas = canvas;
            dad.species = x.Key;
            dad.GetComponent<RawImage>().texture = x.Value;
            i++;
        }
    }
}