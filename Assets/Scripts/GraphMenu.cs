using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphMenu : MonoBehaviour {
    public static bool IsPaused = false;
    public GameObject graphMenu;
    public GameObject graphMenu2;

    private void Update() {
        if (!Input.GetKeyDown(KeyCode.Escape)) 
            return;
        
        if (IsPaused)
            HideGraph();
        else
            ShowGraph();
    }

    private void HideGraph() {
        if (graphMenu2.activeSelf)
            graphMenu2.SetActive(false);
        // UnSetGraph();
        graphMenu.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }

    public void ShowGraph() {
        if (graphMenu2.activeSelf)
            graphMenu2.SetActive(false);
        graphMenu.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void SetGraph() {
        graphMenu.SetActive(false);
        graphMenu2.SetActive(true);
    }

    public void UnSetGraph() {
        graphMenu2.SetActive(false);
        graphMenu.SetActive(true);
    }
}
