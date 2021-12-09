public class SpeedGraph : Graph {
    public override void SetCurrentSpeciesSelected(Species species) {
        valueList = agentSpawner.FetchAverageChickenSpeedList(); 
    } 
}

