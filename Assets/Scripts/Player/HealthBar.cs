using System.Collections;
using System.Collections.Generic;
using Cores.CoreComponents;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthBarSlider;
    [SerializeField]
    private Stats playerStats;

    [SerializeField]
    private float decreaseDuration = 2f;
    [SerializeField]
    private Ease ease = Ease.InOutSine;

    private void Awake() => healthBarSlider.maxValue = playerStats.MaxHealth;

    // private void Update()
    // {
    //     UpdateHealth();
    // }


    public void UpdateHealth()
    {
        healthBarSlider.DOValue(playerStats.CurrentHealthAmount, decreaseDuration).SetEase(ease);
    }
}
