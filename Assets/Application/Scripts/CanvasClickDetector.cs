using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasClickDetector : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            Debug.Log("Clicked on canvas: " + canvas.name);
        }
    }
}