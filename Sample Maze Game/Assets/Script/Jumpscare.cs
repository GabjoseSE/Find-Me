using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Jumpscare : MonoBehaviour
{
    public VideoPlayer currentVideo;
    public VideoPlayer nextVideo;
    public CanvasGroup fadeCanvasGroup; // Canvas Group for fade effect
    public float fadeDuration = 1f;


    void Start()
    {
        fadeCanvasGroup.alpha = 1f;
        currentVideo.loopPointReached += EndReached;
        currentVideo.Play(); // Ensure video starts playing
    }

    void EndReached(VideoPlayer vp)
    {
        StartCoroutine(TransitionToNextVideo());
    }

    private IEnumerator TransitionToNextVideo()
    {
        // Fade out
        yield return StartCoroutine(Fade(1f, 0f));

        // Swap videos
        currentVideo.gameObject.SetActive(false);
        nextVideo.gameObject.SetActive(true);
        nextVideo.Play();

        // Fade in
        yield return StartCoroutine(Fade(0f, 1f));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = endAlpha;

    }
}
