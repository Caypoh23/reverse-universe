using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerSceneSwitcher : MonoBehaviour
{
    [SerializeField] private Tag playerTag;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Image image;


    private float _fadeDuration = 1.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.HasTag(playerTag))
        {
            LoadOakWoodsScene();
        }
    }

    private void Start()
    {
        FadeOut();
    }

    private void FadeOut() => image?.DOFade(0f, _fadeDuration);
    

    private void LoadOakWoodsScene()
    {
        image.DOFade(1f, _fadeDuration)
            .OnComplete(() => 
            { 
                sceneLoader.LoadOakWoodsScene();
            });
    }
}
