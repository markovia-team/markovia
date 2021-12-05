using System;
using System.Collections.Generic;
using SFB;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private Dictionary<Species, GameObject> speciesPrefabs;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject popup;
    
    public void PlayGame() {
        if (speciesPrefabs != null) {
            foreach (var pair in speciesPrefabs)
                AgentSpawner.AddSpecies(pair.Key, pair.Value);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings() {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void GoBack() {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
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

    private void showPopup() {
        popup.SetActive(true);
    }
}
