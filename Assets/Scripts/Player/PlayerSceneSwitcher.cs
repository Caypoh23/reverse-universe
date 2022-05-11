using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerSceneSwitcher : MonoBehaviour
{
    [SerializeField] private Tag playerTag;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Image image;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.HasTag(playerTag))
        {
            // image.DoFade(1f, 2f);
            sceneLoader.LoadOakWoodsScene();
        }
    }
}
