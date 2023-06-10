using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbTestVisuals : MonoBehaviour
{
    [SerializeField] private Slider _orbHealthSlider;

    private void Start()
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        OrbTest.Instance.OnHealthChange += UpdateSliderValue;
    }

    private void UpdateSliderValue(float value)
    {
        _orbHealthSlider.value = value;
    }
}
