using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cores.CoreComponents;
using ReverseTime;

public class Ending : MonoBehaviour
{
    [SerializeField] private Stats playerStats;

    [SerializeField] private Stats bossStats;

    [SerializeField] private Image endingBackground;

    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private GameObject losePanel;

    [SerializeField] private TimerSlider timerSlider;

    private float _fadeDuration = .2f;

    private bool _canActivatePanel = true;

    private float _currentTimerAmount = 5f;


    private void Update()
    {
        CheckLoseCondition();

        if(bossStats != null)
        {
            CheckWinCondition();
        }
    }

    private void CheckLoseCondition()
    {
        if(playerStats.CurrentHealthAmount <= 0)
        {
            if(_currentTimerAmount >= 0)
            {
                _currentTimerAmount -= Time.deltaTime;
                timerSlider.UpdateTimerSlider(_currentTimerAmount);    
            }
            else
            {
                ActivateEndingBackground(losePanel);
                _currentTimerAmount = 5;
            }
        }
    }

    private void CheckWinCondition()
    {
        if(bossStats.CurrentHealthAmount <= 0) 
        {
            ActivateEndingBackground(creditsPanel);
        }
    }

    private void ActivateEndingBackground(GameObject panelToActivate)
    {
        if(_canActivatePanel)
        { 
            endingBackground.DOFade(1, _fadeDuration)
            .OnComplete(() => 
            {
                panelToActivate.SetActive(true);
            });
            _canActivatePanel = false;
        }
    }
}
