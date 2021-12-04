using System;
using UnityEngine;

public class GameData {
    public int agentCount;
    public string agentList = string.Empty;

    public void addAgent(GameObject agent) {
        var agentData = agent.GetComponent<Agent>().ToString();
        if (string.Compare(agentList, string.Empty, StringComparison.Ordinal) != 0)
            agentList += "\n" + agentData;
        else agentList = agentData;
        agentCount++;
    }

    public string getAgentList() {
        return agentList;
    }
}