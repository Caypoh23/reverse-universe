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

    public void UpdateTimerSlider(RewindTime rewindTime, float currentReverseTimerAmount)
    {
        timerSlider.value = currentReverseTimerAmount;
    }
}
