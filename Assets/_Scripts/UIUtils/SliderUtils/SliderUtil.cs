using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SliderUtil : UIComponentUtil
{
    private Slider slider;

    [SerializeField] private bool doSetValueOnStart;
    public override void Awake()
    {
        base.Awake();

        slider = GetComponent<Slider>();
    }
    public override void Start()
    {
        base.Start();

        if (doSetValueOnStart)
        {
            SetSliderValue();
        }

        slider.onValueChanged.AddListener(SliderValueUpdate);
    }
    protected override void StartListeningForEvents()
    {
        EventMessenger.StartListening(uiName + EventKey.SetSliderValue, SetSliderValue);
    }
    protected override void StopListeningToEvents()
    {
        EventMessenger.StopListening(uiName + EventKey.SetSliderValue, SetSliderValue);
    }
    private void SetSliderValue()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
        slider.value = DataMessenger.GetFloat(uiName + FloatKey.SliderNewValue);
    }
    private void SliderValueUpdate(float sliderValue)
    {
        DataMessenger.SetFloat(uiName + FloatKey.SliderValue, slider.value);
        EventMessenger.TriggerEvent(uiName + EventKey.SliderUpdated);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();

        slider.onValueChanged.RemoveAllListeners();
    }
}