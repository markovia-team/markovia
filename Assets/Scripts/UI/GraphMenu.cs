using UnityEngine;
using UnityEngine.UI;

public class GraphMenu : MonoBehaviour {
    private static bool isPaused = false;
    private static bool wasPaused = false;
    public GameObject graphMenu;
    public GameObject settingsMenu;
    public GameObject button;
    public GameObject windowGraph;
    public GameObject menuGraph; 
    public GameObject agentData; 
    public Sprite playAsset;
    public Sprite pauseAsset;
    public SerializableDictionary<Species, GameObject> graphs = new SerializableDictionary<Species, GameObject>(); 
    public SerializableDictionary<Species, GameObject> sizeGraphs = new SerializableDictionary<Species, GameObject>(); 
    public SerializableDictionary<Species, GameObject> speedGraphs = new SerializableDictionary<Species, GameObject>();
    
    private void Update() {
        if (!Input.GetKeyDown(KeyCode.Escape)) 
            return;

        if (isPaused && settingsMenu.activeSelf) {
            HideSettings();
            if (windowGraph != null && (graphMenu.activeSelf || windowGraph.activeSelf))
                Pause();
        } else {
            if (windowGraph != null && (graphMenu.activeSelf || windowGraph.activeSelf)) 
                HideGraph();
            else if (agentData.activeSelf)
                agentData.SetActive(false);
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

    private void SetGraph() {
        graphMenu.SetActive(false);
        windowGraph = Instantiate(windowGraph, transform.position, windowGraph.transform.rotation, transform);
        windowGraph.SetActive(true); 
    }
    
    public void SetMenu() {
        graphMenu.SetActive(false);
        windowGraph = menuGraph; 
        windowGraph.SetActive(true);
    }
    
    public void SetChickenGraph() {
        graphs.TryGetValue(Species.Chicken, out var x);
        windowGraph = x;
        SetGraph();
    }
    
    public void SetFoxGraph() {
        graphs.TryGetValue(Species.Fox, out var x);
        windowGraph = x;
        SetGraph();
    }
    
    public void SetGrassGraph() {
        graphs.TryGetValue(Species.Grass, out var x);
        windowGraph = x;
        SetGraph();
    }
    
    public void SetChickenSizeGraph() {
        sizeGraphs.TryGetValue(Species.Chicken, out var x);
        windowGraph = x;         
        SetGraph();
    }
    
    public void SetChickenAvgSpeedGraph() {
        speedGraphs.TryGetValue(Species.Chicken, out var x);
        windowGraph = x;         
        SetGraph();
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
