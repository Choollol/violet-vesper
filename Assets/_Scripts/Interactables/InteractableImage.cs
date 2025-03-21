using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractableImage : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    protected Image image;
    protected RectTransform rectTransform;

    protected bool canInteract = true;
    protected bool canDrag = true;

    [SerializeField] private float alphaHitThreshold = 0.5f;

    public virtual void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        image.alphaHitTestMinimumThreshold = alphaHitThreshold;
    }
    public virtual void Start()
    {
        
    }
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
