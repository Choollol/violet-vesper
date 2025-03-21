using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonInteractable : InteractableImage
{
    protected Button button;
    public override void Start()
    {
        base.Start();

        button = GetComponent<Button>();
    }

    private void Cleanup()
    {
        button.onClick.RemoveAllListeners();
    }

    public void OnDestroy()
    {
        Cleanup();
    }
}
