using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphMenu : MonoBehaviour {
    public static bool IsPaused = false;
    public GameObject graphMenu;
    public GameObject windowGraph;
    public GameObject settingsMenu;
    public GameObject button;

    private void Update() {
        if (!Input.GetKeyDown(KeyCode.Escape)) 
            return;

        if (IsPaused && settingsMenu.activeSelf) {
            HideSettings();
            if (graphMenu.activeSelf || windowGraph.activeSelf) {
                Pause();
            }
        }
        else {
            if (graphMenu.activeSelf || windowGraph.activeSelf) 
                HideGraph();
            else
                ShowSettings();
        }
    }

    public void HideGraph() {
        if (windowGraph.activeSelf)
            windowGraph.SetActive(false);
        graphMenu.SetActive(false);
        Pause();
    }

    public void ShowGraph() {
        if (windowGraph.activeSelf)
            windowGraph.SetActive(false);
        graphMenu.SetActive(true);
        Pause();
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
        Pause();
        settingsMenu.SetActive(true);
    }

    public void HideSettings() {
        Pause();
        settingsMenu.SetActive(false);
    }

    public void Forward() {
        WorldController.tickSpeed += 1f;
    }
    
    public void Rewind() {
        WorldController.tickSpeed -= 1f;
    }

    public Sprite playAsset;
    public Sprite pauseAsset;
    public void Pause() {
        if (IsPaused) {
            // button.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Skip");
            button.GetComponent<Image>().sprite = pauseAsset;
            Time.timeScale = 1;
            IsPaused = false;
        }
        else {
            button.GetComponent<Image>().sprite = playAsset;
            Time.timeScale = 0;
            IsPaused = true;
        }
    }
}
