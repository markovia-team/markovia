using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    private Dictionary<Species, GameObject> speciesPrefabs = new Dictionary<Species, GameObject>();
    
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
        speciesPrefabs = new Dictionary<Species, GameObject>();
        if (!File.Exists(Application.dataPath + "/Scripts/AgentSpawnerFile.txt")) 
            return;
        
        List<string> lines = new List<string>();
        lines = File.ReadAllLines(Application.dataPath + "/Scripts/AgentSpawnerFile.txt").ToList();
        int count = 0, agents, species;
        foreach (string line in lines) {
           if (count == 0) {
               if (!Int32.TryParse(line, out agents))
                    return;
           }
           else {
               if (!Int32.TryParse(line, out species)) {
                    speciesPrefabs = new Dictionary<Species, GameObject>();
                    return;
               }
               GameObject prefab;
               switch (species) {
                   case (int) Species.Grass:
                        prefab = (GameObject) Resources.Load("Grass", typeof(GameObject));
                        speciesPrefabs.Add(Species.Grass, prefab);
                        break;
                   case (int) Species.Chicken:
                        prefab = (GameObject) Resources.Load("Chicken", typeof(GameObject));
                        speciesPrefabs.Add(Species.Chicken, prefab);
                        break;
                   case (int) Species.Fox:
                        prefab = (GameObject) Resources.Load("Fox", typeof(GameObject));
                        speciesPrefabs.Add(Species.Fox, prefab);
                        break;
               }
           }
           count++;
        }
        
        PlayGame();
    }
}
