using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using System.Runtime.Serialization;
using System.IO;
using System.Text;

public class GameData{
    private int agentCount = 0;
    private string agentList = string.Empty;

    public void addAgent(GameObject agent){
        string agentData = agent.ToString();
        agentList += "\n" + agent;
        agentCount++;
    }

    public string getAgentList(){
        return agentList;
    }

}