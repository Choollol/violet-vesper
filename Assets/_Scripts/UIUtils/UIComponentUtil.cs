using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponentUtil : UIUtil
{
    protected RectTransform rectTransform;
    public override void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public override void OnDestroy()
    {

    }
    public override void OnEnable()
    {
        base.OnEnable();

        StartListeningForEvents();
    }
    public override void OnDisable()
    {
        base.OnDisable();

        StopListeningToEvents();
    }
    protected override void Enable()
    {

    }
    protected override void Disable()
    {

    }
}