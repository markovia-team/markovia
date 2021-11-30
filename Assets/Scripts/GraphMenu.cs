using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphMenu : MonoBehaviour {
    public static bool IsPaused = false;
    public GameObject graphMenu;
    public GameObject windowGraph;
    public GameObject settingsMenu;

    private void Update() {
        if (!Input.GetKeyDown(KeyCode.Escape)) 
            return;

        if (IsPaused && settingsMenu.activeSelf) {
            HideSettings();
            if (graphMenu.activeSelf || windowGraph.activeSelf) {
                Time.timeScale = 0;
                IsPaused = true;
            }
        }
        else
            ShowSettings();
    }

    public void HideGraph() {
        if (windowGraph.activeSelf)
            windowGraph.SetActive(false);
        graphMenu.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }

    public void ShowGraph() {
        if (windowGraph.activeSelf)
            windowGraph.SetActive(false);
        graphMenu.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void SetGraph() {
        graphMenu.SetActive(false);
        windowGraph.SetActive(true);
    }

    public void UnSetGraph() {
        windowGraph.SetActive(false);
        graphMenu.SetActive(true);
    }

    public void ShowSettings() {
        Time.timeScale = 0;
        IsPaused = true;
        settingsMenu.SetActive(true);
    }

    public void HideSettings() {
        Time.timeScale = 1;
        IsPaused = false;
        settingsMenu.SetActive(false);
    }

    public void Forward() {
        WorldController.tickSpeed += 1f;
    }
    
    public void Rewind() {
        WorldController.tickSpeed -= 1f;
    }

    public void Pause() {
        if (IsPaused) {
            Time.timeScale = 1;
            IsPaused = false;
        }
        else {
            Time.timeScale = 0;
            IsPaused = true;
        }
    }
}
