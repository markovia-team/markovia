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
    public int agentCount = 0;
    public string agentList = string.Empty;

    public void addAgent(GameObject agent){
        string agentData = agent.ToString().Split('(')[0];
        if(agentList.CompareTo(string.Empty) != 0)
            agentList += "\n" + agentData;
        else agentList = agentData;
        agentCount++;
    }

    public string getAgentList(){
        return agentList;
    }

}