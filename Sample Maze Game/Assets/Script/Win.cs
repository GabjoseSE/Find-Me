using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Win : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;        // The CanvasGroup used for fading the entire scene
    public VideoPlayer videoPlayer;            // The VideoPlayer component for playing the video
    public Image imageToFade;                  // Image component to fade in alongside the video
    public float fadeDuration = 1.5f;          // Duration of the fade-in effect
    public float imageDelay = 2f;              // Delay before the image starts fading in

    private void Start()
    {
        // Start with the video and scene fully transparent
        fadeCanvasGroup.alpha = 0f; // Set to 0 for a fade-in effect
        videoPlayer.Play(); // Start playing the video (if auto-play is disabled in VideoPlayer settings)

        // Start the fade-in coroutine
        StartCoroutine(FadeInSceneWithVideo());
    }

    private IEnumerator FadeInSceneWithVideo()
    {
        float elapsedTime = 0f;

        // Gradually increase the alpha from 0 to 1 to make the scene visible
        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure everything is fully visible at the end
        fadeCanvasGroup.alpha = 1f;

        // Delay for the specified time before fading in the image
        yield return new WaitForSeconds(imageDelay);

        // Start fading in the image after the delay
        yield return StartCoroutine(FadeInImage());
    }

    private IEnumerator FadeInImage()
    {
        Color imageColor = imageToFade.color;
        imageColor.a = 0; // Start with the image fully transparent
        imageToFade.color = imageColor;

        float elapsedTime = 0f;

        // Gradually increase the alpha from 0 to 1 to fade in the image
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(elapsedTime / fadeDuration); // Update alpha
            imageToFade.color = imageColor;
            yield return null; // Wait for the next frame
        }

        // Ensure the image is fully visible at the end
        imageColor.a = 1;
        imageToFade.color = imageColor;
    }
}
