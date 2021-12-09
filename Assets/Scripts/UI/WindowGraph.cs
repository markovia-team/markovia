public class WindowGraph : Graph {
    public override void SetCurrentSpeciesSelected(Species species) {
        currentSpeciesSelected = species;
        valueList = agentSpawner.FetchDataPoints(species); 
    } 
}
