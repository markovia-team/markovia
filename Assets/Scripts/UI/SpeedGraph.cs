public class SpeedGraph : Graph {
    public override void SetCurrentSpeciesSelected(Species species) {
        currentSpeciesSelected = species; 
        valueList = agentSpawner.FetchAverageChickenSpeedList(); 
    } 
}

