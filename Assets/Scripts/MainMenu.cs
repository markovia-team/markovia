using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AddFox() {
        GameObject prefab = (GameObject) Resources.Load("Fox", typeof(GameObject));
        AgentSpawner.AddSpecies(Species.Fox, prefab);
    }
    
    public void AddChicken() {
        GameObject prefab = (GameObject) Resources.Load("Chicken", typeof(GameObject));
        AgentSpawner.AddSpecies(Species.Chicken, prefab);
    }
    
    public void AddGrass() {
        GameObject prefab = (GameObject) Resources.Load("Grass", typeof(GameObject));
        AgentSpawner.AddSpecies(Species.Grass, prefab);
    }
}
