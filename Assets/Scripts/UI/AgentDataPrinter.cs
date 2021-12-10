using MathNet.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class AgentDataPrinter : MonoBehaviour {
    public GameObject agentData;
    public GameObject agentSpecies;
    public GameObject sleep;
    public GameObject hunger;
    public GameObject rep;
    public GameObject thirst;
    public GameObject age;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 1;
        } else if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit)) {
                var selection = hit.transform;
                if (selection.gameObject.CompareTag("Printable") && selection != null) {
                    Agent agent = selection.gameObject.GetComponent<Agent>();
                    DisplayData(agent);
                }
            }
        }
    }

    public void HideData() {
        agentData.SetActive(false);
        Time.timeScale = 1;
    }

    private void DisplayData(Agent agent) {
        agentSpecies.GetComponent<Text>().text = agent.ToString();
        age.GetComponent<Text>().text = "Age: " + agent.GetAge().Round(5);
        sleep.GetComponent<Text>().text = "Sleep: " + agent.stats.GetNeed(Need.Sleep).Round(5);
        hunger.GetComponent<Text>().text = "Hunger: " + agent.stats.GetNeed(Need.Hunger).Round(5);
        rep.GetComponent<Text>().text = "Rep. Urge: " + agent.stats.GetNeed(Need.ReproductiveUrge).Round(5);
        thirst.GetComponent<Text>().text = "Thirst: " + agent.stats.GetNeed(Need.Thirst).Round(5);
        agentData.SetActive(true);
        Time.timeScale = 0;
    }
}