public class SizeGraph : Graph {
    public override void SetCurrentSpeciesSelected(Species species) {
        currentSpeciesSelected = species; 
        valueList = agentSpawner.FetchChickenSizeDataPoints(); 
    } 
}