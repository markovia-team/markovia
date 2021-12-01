using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas; 
    private RectTransform rectTransform;
    private Vector2 originPos;

    [SerializeField] private GameObject prefab;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originPos = rectTransform.anchoredPosition; 
    }
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = originPos; 
        
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;
        Debug.Log("RAY"+ray);
        if (Physics.Raycast (ray, out hit))
        {
            Instantiate(prefab, hit.point, prefab.transform.rotation);
            // Debug.Log("HIT!!!");
            // //draw invisible ray cast/vector
            // Debug.DrawLine (ray.origin, hit.point);
            // //log hit area to the console
            // Debug.Log(hit.point);
        }    
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; 
    } 
    
    // private static Rect RectTransformToScreenSpace(RectTransform transform)
    // {
    //     Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
    //     return new Rect((Vector2)transform.position - (size * 0.5f), size);
    // }
}
