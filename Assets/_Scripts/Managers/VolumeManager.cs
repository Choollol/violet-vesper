using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    // Examples: SFXVolume, BGMVolume
    [SerializeField] private List<string> volumeCategories;

    private readonly List<UnityAction> updateVolumeFunctions = new();

    [SerializeField] private AudioMixer mixer;

    [SerializeField] private float defaultVolume = 0.6f;

    private void Awake()
    {
        foreach (string volumeCategory in volumeCategories)
        {
            updateVolumeFunctions.Add(() => UpdateVolumeCategory(volumeCategory));
        }

        DataMessenger.SetStringList(StringListKey.VolumeCategories, volumeCategories);
    }
    private void OnEnable()
    {
        for (int i = 0, len = volumeCategories.Count; i < len; ++i)
        {
            EventMessenger.StartListening(volumeCategories[i] + EventKey.SliderUpdated, updateVolumeFunctions[i]);
        }
    }
    private void OnDisable()
    {
        for (int i = 0, len = volumeCategories.Count; i < len; ++i)
        {
            EventMessenger.StopListening(volumeCategories[i] + EventKey.SliderUpdated, updateVolumeFunctions[i]);
        }
    }
    private void Start()
    {
        // Set default volumes
        if (defaultVolume == 0)
        {
            defaultVolume = 0.001f;
        }
        List<float> volumes = new();
        for (int i = 0, len = volumeCategories.Count; i < len; ++i)
        {
            volumes.Add(defaultVolume);
        }
        SetVolumes(volumes, false);
    }

    private void UpdateVolumeCategory(string volumeCategory)
    {
        float volume = DataMessenger.GetFloat(volumeCategory + FloatKey.SliderValue);
        if (volume == 0)
        {
            volume = 0.001f;
        }
        DataMessenger.SetFloat(volumeCategory, volume);
        UpdateMixer();
    }
    private void UpdateMixer()
    {
        foreach (string volumeCategory in volumeCategories)
        {
            mixer.SetFloat(volumeCategory, Mathf.Log10(DataMessenger.GetFloat(volumeCategory)) * 20);
        }
    }
    public void UpdateSliders()
    {
        foreach (string volumeCategory in volumeCategories)
        {
            DataMessenger.SetFloat(volumeCategory + FloatKey.SliderNewValue,
                DataMessenger.GetFloat(volumeCategory));
            EventMessenger.TriggerEvent(volumeCategory + EventKey.SetSliderValue);
        }
    }
    private void SetVolumes(List<float> newVolumes, bool doUpdateSliders = true)
    {
        for (int i = 0, len = newVolumes.Count; i < len; ++i)
        {
            DataMessenger.SetFloat(volumeCategories[i], newVolumes[i]);
        }

        if (doUpdateSliders)
        {
            UpdateSliders();
        }
        UpdateMixer();
    }
}