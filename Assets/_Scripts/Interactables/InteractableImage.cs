using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableImage : ImageUtil, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    protected bool canInteract = true;
    protected bool canDrag = true;

    protected virtual void HandleDrag(PointerEventData eventData)
    {

    }

    protected virtual void HandlePointerDown(PointerEventData eventData)
    {

    }

    protected virtual void HandlePointerUp(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag || !canInteract)
        {
            return;
        }

        HandleDrag(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canInteract)
        {
            return;
        }

        HandlePointerDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!canInteract)
        {
            return;
        }

        HandlePointerUp(eventData);
    }
}
