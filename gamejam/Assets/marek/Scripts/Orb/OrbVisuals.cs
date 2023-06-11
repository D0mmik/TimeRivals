using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbVisuals : MonoBehaviour
{
    [SerializeField] private Slider _orbHealthSlider;

    private void Start()
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        Orb.Instance.OnHealthChange += UpdateSliderValue;
    }

    private void UpdateSliderValue(float value)
    {
        Debug.Log(value);
        _orbHealthSlider.value = value;
    }
}
