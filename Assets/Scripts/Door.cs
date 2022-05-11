using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider2D doorCollider;
    [SerializeField] private SpriteRenderer doorSprite;

    [SerializeField] private GameObject boss;

    [SerializeField] private Tag playerTag;

    private void Start()
    {
        ActivateDoor(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.HasTag(playerTag))
        {
            boss.SetActive(true);
            ActivateDoor(true);
        }
    }

    private void ActivateDoor(bool isActive)
    {
        doorCollider.isTrigger = !isActive;
        doorSprite.enabled = isActive;
    }
}
