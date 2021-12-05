using UnityEngine;
using UnityEngine.UI;

public class AllSpeciesHolder : MonoBehaviour {
    [SerializeField] private GameObject speciesHolderPrefab;
    public SerializableDictionary<Species, Texture> speciesIcon = new SerializableDictionary<Species, Texture>();
    public Canvas canvas;

    void Awake() {
        var i = 0;
        foreach (var x in speciesIcon) {
            var go = Instantiate(speciesHolderPrefab, new Vector2(120 + (i * 120) * canvas.scaleFactor, 100), speciesHolderPrefab.transform.rotation, transform).transform.GetChild(0);
            var dad = go.GetComponent<DragAndDrop>();
            dad.Canvas = canvas;
            dad.species = x.Key;
            dad.GetComponent<RawImage>().texture = x.Value;
            i++;
        }
    }
}