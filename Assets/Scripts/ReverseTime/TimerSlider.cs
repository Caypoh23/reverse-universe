using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using ReverseTime;

public class TimerSlider : MonoBehaviour
{
    [SerializeField]
    private Slider timerSlider;

    [SerializeField]
    private Material GlowMaterial;

    private const string GlowMaterialName = "_Glow";

    private void Awake() => ChangeGlowIntensity(0);

    public void UpdateTimerSlider(float currentReverseTimerAmount)
    {
        timerSlider.value = currentReverseTimerAmount;

        ChangeGlowIntensity(30);

        if (timerSlider.value >= timerSlider.maxValue)
        {
            ChangeGlowIntensity(0);
        }
    }

    private void ChangeGlowIntensity(float value) => GlowMaterial.SetFloat(GlowMaterialName, value);
}
