using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMenuScene() => SceneManager.LoadScene((int) Scenes.Menu);
    
    public void LoadGameScene() => SceneManager.LoadScene((int) Scenes.Game);

    public void LoadOakWoodsScene() => SceneManager.LoadScene((int) Scenes.OakWoods);
}
