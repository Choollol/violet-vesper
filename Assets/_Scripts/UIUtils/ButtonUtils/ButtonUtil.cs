using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class ButtonUtil : UIComponentUtil
{
    protected Button button;
    public override void Awake()
    {
        base.Awake();

        button = GetComponent<Button>();

        button.onClick.AddListener(Clicked);
    }
    protected virtual void Clicked()
    {
        HandleClick();
    }
    protected virtual void HandleClick()
    {

    }
    protected override void Enable()
    {
        base.Enable();

        button.enabled = true;
    }
    protected override void Disable()
    {
        base.Disable();

        button.enabled = false;
    }
    protected override bool IsActive()
    {
        return button.enabled;
    }
    public override void OnDestroy()
    {
        base.OnDestroy();

        button.onClick.RemoveAllListeners();
    }
}