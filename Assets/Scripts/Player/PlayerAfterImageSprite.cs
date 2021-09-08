using UnityEngine;

namespace Player.Old
{
    public class PlayerAfterImageSprite : MonoBehaviour
    {
        [SerializeField] private float activeTime = 0.1f;
        private float _timeActivated;
        private float _alpha;
        [SerializeField] private float alphaSet = 0.8f;
        [SerializeField] private float alphaDecay = 0.65f;

        private Transform _player;

        private SpriteRenderer _SR;
    
        private SpriteRenderer _playerSR;
        [SerializeField] private Tag afterImageTag;

        private Color _color;

        private void OnEnable()
        {
            _SR = GetComponent<SpriteRenderer>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _playerSR = _player.GetComponent<SpriteRenderer>();
        
            _alpha = alphaSet;
            _SR.sprite = _playerSR.sprite;
            transform.position = _player.position;
            transform.rotation = _player.rotation;
            _timeActivated = Time.time;
        } 

        private void Update()
        {
            _alpha -= alphaDecay * Time.deltaTime;
            _color = new Color(1f, 1f, 1f, _alpha);
            _SR.color = _color;
        
            if (Time.time >= (_timeActivated + activeTime))
            {
                /*
            var playerTransform = _player.transform;
            ObjectPooler.Instance.SpawnFromPool(afterImageTag, playerTransform.position, playerTransform.rotation);*/
                gameObject.SetActive(false);
            }
        }
    }
}
