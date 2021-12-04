using UnityEngine;
using UnityEngine.UI;

public class GraphMenu : MonoBehaviour {
    private static bool isPaused = false;
    private static bool wasPaused = false;
    public GameObject graphMenu;
    public GameObject windowGraph;
    public GameObject settingsMenu;
    public GameObject button;
    public Sprite playAsset;
    public Sprite pauseAsset;

    private void Update() {
        if (!Input.GetKeyDown(KeyCode.Escape)) 
            return;

        if (isPaused && settingsMenu.activeSelf) {
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
        if (!wasPaused)
            Play();
    }

    public void ShowGraph() {
        if (windowGraph.activeSelf)
            windowGraph.SetActive(false);
        graphMenu.SetActive(true);
        wasPaused = isPaused;
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
        settingsMenu.SetActive(true);
        wasPaused = isPaused;
        Pause();
    }

    public void HideSettings() {
        settingsMenu.SetActive(false);
        if (!wasPaused)
            Play();
    }

    public void Forward() {
        WorldController.tickSpeed += 1f;
    }
    
    public void Rewind() {
        if (WorldController.tickSpeed > 0)
            WorldController.tickSpeed -= 1f;
    }

    public void PlayOrPause() {
        if (isPaused)
            Play();
        else
            Pause();
    }

    private void Pause() {
        button.GetComponent<Image>().sprite = playAsset;
        Time.timeScale = 0;
        isPaused = true;
    }

    private void Play() {
        button.GetComponent<Image>().sprite = pauseAsset;
        Time.timeScale = 1;
        isPaused = false;
    }
}
