using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Jumpscare : MonoBehaviour
{
    public VideoPlayer currentVideo;
    public VideoPlayer nextVideo;
    public CanvasGroup fadeCanvasGroup;    // Canvas Group for fade effect between videos
    public CanvasGroup textCanvasGroup;    // Canvas Group for TextMeshPro text
    public TextMeshProUGUI transitionText; // TextMeshPro text element
    public float fadeDuration = 1f;        // Fade duration

    void Start()
    {
        fadeCanvasGroup.alpha = 1f;
        textCanvasGroup.alpha = 0f;        // Make sure the text is hidden initially
        currentVideo.loopPointReached += EndReached;
        currentVideo.Play();                // Play the first video
    }

    void EndReached(VideoPlayer vp)
    {
        StartCoroutine(TransitionToNextVideo());
    }

    private IEnumerator TransitionToNextVideo()
    {
        // Fade out current video
        yield return StartCoroutine(FadeCanvasGroup(fadeCanvasGroup, 1f, 0f));

        // Swap to the next video
        currentVideo.gameObject.SetActive(false);
        nextVideo.gameObject.SetActive(true);
        nextVideo.Play();

        // Fade in the next video
        yield return StartCoroutine(FadeCanvasGroup(fadeCanvasGroup, 0f, 1f));

        // ** Now fade in the TMP text **
        yield return StartCoroutine(FadeCanvasGroup(textCanvasGroup, 0f, 1f));

        // Wait for a few seconds to display the text (adjust delay as needed)
        yield return new WaitForSeconds(3f);

        // Fade out the next video and TMP text
        yield return StartCoroutine(FadeCanvasGroup(fadeCanvasGroup, 1f, 0f));

        // Load the next scene after the fade-out
        SceneManager.LoadSceneAsync(0);
    }

    // Fade method for CanvasGroup
    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}
