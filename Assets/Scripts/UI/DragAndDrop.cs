using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    public Canvas canvas;
    public AgentSpawner agentSpawner;
    public Species species;
    private RectTransform rectTransform;
    private Vector2 originPos;

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
            agentSpawner.AddSpecies(species, hit.point);
        }
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}