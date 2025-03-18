using UnityEngine;
using UnityEngine.UI;

// ComponentUtil class for Images
public class ImageUtil : UIComponentUtil
{
    protected Image image;

    [SerializeField] private float alphaHitThreshold = 0.5f;
    public override void Awake()
    {
        base.Awake();

        image = GetComponent<Image>();

        image.alphaHitTestMinimumThreshold = alphaHitThreshold;
    }
    protected override void Enable()
    {
        image.enabled = true;
    }
    protected override void Disable()
    {
        image.enabled = false;
    }
    protected override bool IsActive()
    {
        return image.enabled;
    }
}