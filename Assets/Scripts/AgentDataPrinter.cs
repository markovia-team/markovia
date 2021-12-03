using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AgentDataPrinter : MonoBehaviour {

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)){
                var selection = hit.transform;
                if(selection.gameObject.CompareTag("Printable") && selection!= null){
                    Debug.Log(selection.gameObject.name.ToString());
                }
            }
        }
    }
}