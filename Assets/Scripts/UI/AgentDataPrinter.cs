
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
    public static bool displayingData = false;
    private int timer = -1;


    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                var selection = hit.transform;
                if (selection.gameObject.CompareTag("Printable") && selection != null){
                    Agent agent = selection.gameObject.GetComponent<Agent>();
                    timer = 0;
                    displayData(agent);
                }
            }
        }
    }

/*
    private void FixedUpdate() {
        if (timer != -1) {
            timer++;
            if (timer >= 250)
                if (gameObject.GetComponent<Text>() != null) {
                    gameObject.GetComponent<Text>().text = string.Empty;
                    timer = -1;
                    hideData();
                }
        }
    }
    */

    public void hideData(){
        agentData.SetActive(false);
        Time.timeScale = 1;
        displayingData = false;
    }

    public void displayData(Agent agent){
        agentSpecies.GetComponent<Text>().text = agent.ToString();
        age.GetComponent<Text>().text = "Age->" + agent.GetAge().ToString();
        sleep.GetComponent<Text>().text = "Sleep->" + agent.stats.GetNeed(Need.Sleep).ToString();
        hunger.GetComponent<Text>().text = "Hunger->" + agent.stats.GetNeed(Need.Hunger).ToString();
        rep.GetComponent<Text>().text = "Rep. Urge->" + agent.stats.GetNeed(Need.ReproductiveUrge).ToString();
        thirst.GetComponent<Text>().text = "Thirst->" + agent.stats.GetNeed(Need.Thirst).ToString();
        agentData.SetActive(true);
        Time.timeScale = 0;
        displayingData = true;
    }
}