using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    public Canvas canvas;
    public AgentSpawner agentSpawner;
    public WorldController wc; 
    public Species species;
    public Inorganic inorganic; 
    private RectTransform rectTransform;
    private Vector2 originPos;

    public Boolean isSpecies = true;
    public Boolean isTree = false; 

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        originPos = rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData) {}

    public void OnBeginDrag(PointerEventData eventData) {}

    public void OnEndDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition = originPos;

        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out var hit)) {
            if (isTree)
                agentSpawner.AddTree(hit.point);
            else if (isSpecies)
                agentSpawner.AddSpecies(species, hit.point);
            else 
                wc.NewWater( new Vector3(hit.point.x, 40, hit.point.z));
        }
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}