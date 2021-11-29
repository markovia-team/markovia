using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphMenu : MonoBehaviour {
    public static bool IsPaused = false;
    public GameObject graphMenu;
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsPaused)
                HideGraph();
            else
                ShowGraph();
        }
    }

    void HideGraph() {
        graphMenu.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }

    void ShowGraph() {
        graphMenu.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
    }
}
