
using UnityEngine;
using UnityEngine.UI;

public class AgentDataPrinter : MonoBehaviour {

    public GameObject agentData;
    public GameObject agentSpecies;
    public GameObject sleep;
    public GameObject hunger;
    public GameObject rep;
    public GameObject thirst;
    public GameObject currentState;
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
                    GameObject agent = selection.gameObject.GetComponent<Agent>();
                    agentSpecies.GetComponent<Text>.text = agent.ToString();
                    age.GetComponent<Text>().text = agent.GetAge().ToString();


                    timer = 0;
                    displayData();
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

    public void displayData(){
        agentSpecies.GetComponent<Text>().text = "hola";
        agentData.SetActive(true);
        Time.timeScale = 0;
        displayingData = true;
    }
}