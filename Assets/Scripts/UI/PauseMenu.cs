using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Image pauseMenuBackground;
    [SerializeField]
    private float animationDuration = 0.5f;

    [SerializeField]
    private GameObject buttonsPanel;
    [SerializeField]
    private GameObject promptPanel;
    [SerializeField]
    private GameObject audioPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuBackground.gameObject.activeInHierarchy)
        {
            ActivatePauseMenu();
        }
        else if (
            Input.GetKeyDown(KeyCode.Escape) && pauseMenuBackground.gameObject.activeInHierarchy
        )
        {
            ContinueGame();
        }
    }

    private void ActivatePauseMenu()
    {
        pauseMenuBackground.gameObject.SetActive(true);
        pauseMenuBackground
            .DOFade(1f, animationDuration)
            .OnComplete(
                () =>
                {
                    Time.timeScale = 0;
                }
            );
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pauseMenuBackground
            .DOFade(0, animationDuration)
            .OnComplete(
                () =>
                {
                    pauseMenuBackground.gameObject.SetActive(false);
                    ResetPanels();
                }
            );
    }

    private void ResetPanels()
    {
        buttonsPanel.SetActive(true);
        promptPanel.SetActive(false);
        audioPanel.SetActive(false);
    }
}
