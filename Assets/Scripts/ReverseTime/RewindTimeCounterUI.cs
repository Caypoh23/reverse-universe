using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RewindTimeCounterUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> amountImages;

    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private Ease fadeEase = Ease.Linear;

    private int _lastElementIndex;

    private bool canPlayAnimation;

    private void Awake()
    {
        _lastElementIndex = amountImages.Count - 1;
    }

    public void PlayActivateAnimation(int currentAmountOfReverseTime)
    {
        if (currentAmountOfReverseTime < _lastElementIndex)
        {
            _lastElementIndex++;
            amountImages[_lastElementIndex]
                .DOFade(1, fadeDuration)
                .SetEase(fadeEase)
                .OnComplete(() => { });
        }
    }

    public void PlayDeactivateAnimation(int currentAmountOfReverseTime)
    {
        if (currentAmountOfReverseTime >= _lastElementIndex)
        {
            amountImages[_lastElementIndex]
                .DOFade(0, fadeDuration)
                .SetEase(fadeEase)
                .OnComplete(() => { });
            _lastElementIndex--;
        }
    }
}
