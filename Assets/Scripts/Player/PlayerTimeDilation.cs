using System;
using Player.Data;
using Player.Input;
using Player.PlayerFiniteStateMachine;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Player
{
    public class PlayerTimeDilation : MonoBehaviour
    {
        [SerializeField]
        private PlayerInputHandler inputHandler;
        [SerializeField]
        private PlayerData playerData;

        [SerializeField] private Image slowMoEffect;

        private float _startTime;

        private bool _isHolding;

        private bool _canActivateEffect = true;

        private void Awake() => _startTime = Time.time;

        public void Update()
        {
            SlowdownTime();
            ResetTimeScale();
        }

        private void SlowdownTime()
        {
            if (inputHandler.CanDelayTimeInput)
            {
                Debug.Log(inputHandler.CanDelayTimeInput + " pressed");
                inputHandler.UseTimeDilationInput();
                _isHolding = true;
                ActivateSlowMoEffect();

                Time.timeScale = playerData.timeDilationTimeScale;
                // smooth time dilation
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                _startTime = Time.unscaledTime;
            }
        }

        private void ResetTimeScale()
        {
            if (_isHolding)
            {
                if (Time.unscaledTime >= _startTime + playerData.maxTimeDilationHoldTime)
                {
                    DeactivateSlowMoEffect();
                    _isHolding = false;
                    Time.timeScale = 1f;
                    Debug.Log("Released");
                }
            }
        }

        private void ActivateSlowMoEffect()
        {
            if(_canActivateEffect)
            {
                slowMoEffect.DOFade(.2f, .5f);
                _canActivateEffect = false;
            }
        }

        private void DeactivateSlowMoEffect()
        {
            slowMoEffect.DOFade(0f, .5f);
            _canActivateEffect = true;
        }
    }
}
