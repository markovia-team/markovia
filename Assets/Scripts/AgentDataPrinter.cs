using UnityEngine;
using UnityEngine.UI;

public class AgentDataPrinter : MonoBehaviour {
    private int timer = -1;

    private void Start() {
        if (gameObject.GetComponent<Text>() != null)
            gameObject.GetComponent<Text>().text = string.Empty;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                var selection = hit.transform;
                if (selection.gameObject.CompareTag("Printable") && selection != null)
                    if (gameObject.GetComponent<Text>() != null) {
                        gameObject.GetComponent<Text>().text = selection.gameObject.GetComponent<Agent>().ToString();
                        timer = 0;
                    }
            }
        }
    }

    private void FixedUpdate() {
        if (timer != -1) {
            timer++;
            if (timer >= 250)
                if (gameObject.GetComponent<Text>() != null) {
                    gameObject.GetComponent<Text>().text = string.Empty;
                    timer = -1;
                }
        }
    }
}