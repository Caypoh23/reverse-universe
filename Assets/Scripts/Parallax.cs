using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private SpriteRenderer parallaxSprite;
    [SerializeField] private float parallaxFactor;

    
    private float _length;
    private float _initialPosition;

    private float _temporaryPosition;
    private float _distance;

    private void Start()
    {
        _initialPosition = transform.position.x;
        _length = parallaxSprite.bounds.size.x;
    }

    private void FixedUpdate()
    {
        MoveParallaxObject();
    }

    private void MoveParallaxObject()
    {
        _temporaryPosition = (mainCamera.transform.position.x * (1 - parallaxFactor));
        _distance = (mainCamera.transform.position.x * parallaxFactor);

        transform.position = new Vector3(_initialPosition + _distance, transform.position.y, transform.position.z);

        CheckParallaxPosition();
    }

    private void CheckParallaxPosition()
    {
        if(_temporaryPosition > _initialPosition + _length)
        {
            _initialPosition += _length;
        }
        else if(_temporaryPosition < _initialPosition - _length)
        {
            _initialPosition -= _length;
        }
    }
}
