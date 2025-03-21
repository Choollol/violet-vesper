using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAndDragInteractable : InteractableImage, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private const float snapDistance = 50;

    [SerializeField] private Transform destinationToSnapTo;


    private void SetPosition(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position,
            eventData.pressEventCamera, out Vector3 mousePos);
        rectTransform.position = mousePos;

        BoundPosition();
    }
    private void BoundPosition()
    {
        float leftBound = rectTransform.rect.width / 2 * rectTransform.lossyScale.x;
        float rightBound = Screen.width - rectTransform.rect.width / 2 * rectTransform.lossyScale.x;

        if (rectTransform.position.x < leftBound)
        {
            rectTransform.SetPosX(leftBound);
        }
        else if (rectTransform.position.x > rightBound)
        {
            rectTransform.SetPosX(rightBound);
        }

        float bottomBound = rectTransform.rect.height / 2 * rectTransform.lossyScale.y;
        float topBound = Screen.height - rectTransform.rect.height / 2 * rectTransform.lossyScale.y;

        if (rectTransform.position.y < bottomBound)
        {
            rectTransform.SetPosY(bottomBound);
        }
        else if (rectTransform.position.y > topBound)
        {
            rectTransform.SetPosY(topBound);
        }
    }
    protected override void HandleDrag(PointerEventData eventData)
    {
        SetPosition(eventData);
    }

    protected override void HandlePointerDown(PointerEventData eventData)
    {
        SetPosition(eventData);
    }

    protected override void HandlePointerUp(PointerEventData eventData)
    {
        if (destinationToSnapTo != null && Vector2.Distance(rectTransform.position, destinationToSnapTo.position) <= snapDistance)
        {
            rectTransform.position = destinationToSnapTo.position;
            canInteract = false;
        }
    }
}
