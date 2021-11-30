using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
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
                Time.timeScale = 0;
                IsPaused = true;
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
