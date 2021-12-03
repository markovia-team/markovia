using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;
using System.Linq;
using SFB;
// using UnityEngine.UIElements;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Dictionary<Species, GameObject> speciesPrefabs = new Dictionary<Species, GameObject>();
    public GameObject button;
    public GameObject popup;

    private void Awake() {
        // if (!File.Exists(Application.dataPath + "/Scripts/AgentSpawnerFile.json")) {
        //     button.GetComponent<Button>().interactable = false;
        // }
    }

    public void PlayGame() {
        foreach (var pair in speciesPrefabs)
            AgentSpawner.AddSpecies(pair.Key, pair.Value);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AddFox() {
        if (speciesPrefabs.ContainsKey(Species.Fox)) {
            speciesPrefabs.Remove(Species.Fox);
        } else {
            GameObject prefab = (GameObject) Resources.Load("Fox", typeof(GameObject));
            speciesPrefabs.Add(Species.Fox, prefab);
            // AgentSpawner.AddSpecies(Species.Fox, prefab);
        }
    }
    
    public void AddChicken() {
        if (speciesPrefabs.ContainsKey(Species.Chicken)) {
            speciesPrefabs.Remove(Species.Chicken);
        } else {
            GameObject prefab = (GameObject) Resources.Load("Chicken", typeof(GameObject));
            speciesPrefabs.Add(Species.Chicken, prefab);
            // AgentSpawner.AddSpecies(Species.Chicken, prefab);
        }
    }
    
    public void AddGrass() {
        if (speciesPrefabs.ContainsKey(Species.Grass)) {
            speciesPrefabs.Remove(Species.Grass);
        } else {
            GameObject prefab = (GameObject) Resources.Load("Grass", typeof(GameObject));
            speciesPrefabs.Add(Species.Grass, prefab);
            // AgentSpawner.AddSpecies(Species.Grass, prefab);
        }
    }

    public void StartFile() {
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false)[0];
        if (string.Compare(path, string.Empty, StringComparison.Ordinal) == 0) {
            popup.GetComponentInChildren<TMPro.TMP_Text>().text = "You must choose a json file";
            showPopup();
            return;
        }

        speciesPrefabs = new Dictionary<Species, GameObject>();
        GameObject prefab;
        GameData savedData;
        try {
            if (!JsonManager.ReadFromJson(path, out savedData)) {
                return;
            }
        } catch(ArgumentException) {
            popup.GetComponentInChildren<TMPro.TMP_Text>().text = "Invalid json";
            showPopup();
            return;
        }
        
        string[] agents = savedData.getAgentList().Split('\n');

        foreach (string agent in agents) {
            if (string.Compare(agent, "Grass", StringComparison.Ordinal) == 0) {
                prefab = (GameObject) Resources.Load(agent, typeof(GameObject));
                speciesPrefabs.Add(Species.Grass, prefab);
            }
            else if (string.Compare(agent, "Fox", StringComparison.Ordinal) == 0) {
                prefab = (GameObject) Resources.Load(agent, typeof(GameObject));
                speciesPrefabs.Add(Species.Fox, prefab);
            }
            else if (string.Compare(agent, "Chicken", StringComparison.Ordinal) == 0) {
                prefab = (GameObject) Resources.Load(agent, typeof(GameObject));
                speciesPrefabs.Add(Species.Chicken, prefab);
            }
        }
        PlayGame();
    }
        
    public void hidePopup() {
        popup.SetActive(false);
    }

    public void showPopup() {
        popup.SetActive(true);
    }
}
