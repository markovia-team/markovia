using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllSpeciesHolderUI : MonoBehaviour
{
    [SerializeField] private GameObject speciesHolderPrefab; 
    public SerializableDictionary<Species, Texture> speciesIcon = new SerializableDictionary<Species, Texture>();

    public Canvas canvas; 
    
    void Awake()
    {
        var i = 0; 
        foreach (var x in speciesIcon)
        {
            var go = Instantiate(speciesHolderPrefab, new Vector2(-700 + i * 120, -250), speciesHolderPrefab.transform.rotation,
                this.transform).transform.GetChild(0); 
            var dad = go.GetComponent<DragAndDrop>(); 
            dad.Canvas = canvas;
            dad.species = x.Key;
            dad.GetComponent<RawImage>().texture = x.Value; 
            i++; 
        }
    }
}
