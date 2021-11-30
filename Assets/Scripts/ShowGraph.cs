using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGraph : MonoBehaviour {
    public GameObject canvas;
    void Start() {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void SetGraph() {
        canvas.SetActive(true);
    }
}
