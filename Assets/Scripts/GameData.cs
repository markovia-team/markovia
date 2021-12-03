using UnityEngine;

public class GameData{
    public int agentCount = 0;
    public string agentList = string.Empty;

    public void addAgent(GameObject agent){
        string agentData = agent.GetComponent<Agent>().ToString();
        if(agentList.CompareTo(string.Empty) != 0)
            agentList += "\n" + agentData;
        else agentList = agentData;
        agentCount++;
    }

    public string getAgentList(){
        return agentList;
    }

}