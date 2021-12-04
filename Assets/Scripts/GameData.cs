using UnityEngine;
using SFB;
using System;
using System.Text;

public class GameData{
    public int agentCount = 0;
    public string agentList = string.Empty;

    public void addAgent(GameObject agent){
        string agentData = agent.GetComponent<Agent>().ToString();
        if(string.Compare(agentList, string.Empty, StringComparison.Ordinal) != 0)
            agentList += "\n" + agentData;
        else agentList = agentData;
        agentCount++;
    }

    public string getAgentList(){
        return agentList;
    }

}