using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TakeDamageScript : MonoBehaviour
{
    public float intensity = 0.5f;             // Intensity of the vignette when taking damage
    public float fadeSpeed = 0.1f;             // Speed of vignette fade-out after damage
    public PostProcessVolume targetVolume;     // Reference to the specific post-process volume to apply effects
    private Vignette vignette;

    void Start()
    {
        // Get the specific PostProcessVolume (set this manually in Inspector)
        if (targetVolume == null)
        {
            Debug.LogError("No PostProcessVolume assigned!");
            return;
        }

        // Try to get the Vignette effect from the volume
        targetVolume.profile.TryGetSettings<Vignette>(out vignette);

        if (!vignette)
        {
            Debug.Log("Error: Vignette not found in Post Process Volume.");
        }
        else
        {
            vignette.enabled.Override(false);  // Initially, disable the vignette effect
        }
    }

    void Update()
    {
        // Gradually fade out the vignette effect after taking damage
        if (vignette != null && vignette.enabled.value)
        {
            vignette.intensity.value -= fadeSpeed * Time.deltaTime;
            if (vignette.intensity.value <= 0)
            {
                vignette.enabled.Override(false);
            }
        }
    }

    // Function to trigger damage effect
    public void TakeDamage()
    {
        if (vignette != null)
        {
            vignette.enabled.Override(true);      // Enable the vignette effect
            vignette.intensity.Override(intensity); // Set intensity
        }
    }
}
