using System.Collections;
using UnityEngine;

public class DummyWaterController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.cyan;
        StartCoroutine(rotate()); 
    }
    
    IEnumerator rotate() {
        while (true) {
            transform.Rotate(Vector3.up, Space.World);
            yield return null; 
        }
    }

    void Update() {}
}
