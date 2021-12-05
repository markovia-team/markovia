using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    [SerializeField] private Canvas canvas;

    public Canvas Canvas {
        get => canvas;
        set => canvas = value;
    }

    private RectTransform rectTransform;
    private Vector2 originPos;

    // TODO: esto no deberia existir, deberia pasar por AgentSpawner
    [SerializeField] private GameObject prefab;

    public Species species;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        originPos = rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData) {
    }

    public void OnBeginDrag(PointerEventData eventData) {
    }

    public void OnEndDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition = originPos;

        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out var hit)) {
            // TODO:  Reemplazar esto por un llamado a un metodo de AgentSpawner!!!!!!
            Instantiate(prefab, hit.point, prefab.transform.rotation);
        }
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}