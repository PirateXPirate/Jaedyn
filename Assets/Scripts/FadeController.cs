using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    const float FLASH_SCENE_TIME = 1.0f;

    [Header("white screen")]
    [SerializeField] CanvasGroup fadeCanvas;
    [SerializeField] float fadeDuration = 1f;

    [Header("button opacity")]
    [SerializeField] CanvasGroup fadeButton;
    [SerializeField] float buttonFadeDuration;

    public GameObject dialogPanel;
    public CanvasGroup dialogGroupForFade;
    public bool isPlayButtonPressed;
    public bool isGotoNextScenePressed;
    public bool isFlashSceneAllowed;



    void Start()
    {
        StartCoroutine(FadeIntro());
    }

    void Update()
    {
        if (isPlayButtonPressed)
        {
            StartCoroutine(FadeToDialog());
        }

        if (isGotoNextScenePressed)
        {
            isGotoNextScenePressed = false;
            fadeCanvas.gameObject.SetActive(true);
            StartCoroutine(FadeOut(fadeCanvas,fadeDuration));
        }
    }

    IEnumerator FadeIntro()
    {
        StartCoroutine(FadeIn(fadeCanvas, fadeDuration));
        yield return new WaitForSeconds(fadeDuration);

        if (isFlashSceneAllowed)
        yield return new WaitForSeconds(FLASH_SCENE_TIME);

        if (fadeButton != null) 
        {
            StartCoroutine(FadeOut(fadeButton, buttonFadeDuration));
            yield return new WaitForSeconds(buttonFadeDuration);
        }

        fadeCanvas.gameObject.SetActive(false);
        yield return null;
    }

    public IEnumerator FadeToDialog()
    {
        isPlayButtonPressed = false;
        fadeCanvas.gameObject.SetActive(true);
        StartCoroutine(FadeOut(fadeCanvas, fadeDuration));
        yield return new WaitForSeconds(fadeDuration);
        dialogPanel.SetActive(true);
        StartCoroutine(FadeIn(fadeCanvas, fadeDuration));
        yield return new WaitForSeconds(fadeDuration);
        StartCoroutine(FadeOut(dialogGroupForFade, fadeDuration));
        yield return new WaitForSeconds(fadeDuration);
        fadeCanvas.gameObject.SetActive(false);
        yield return null;
    }

    public IEnumerator FadeOut(CanvasGroup valve, float duration)
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            valve.alpha = Mathf.Lerp(0, 1, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        valve.alpha = 1;
    }

    public IEnumerator FadeIn(CanvasGroup valve, float duration)
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            valve.alpha = Mathf.Lerp(1, 0, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        valve.alpha = 0;
    }

}