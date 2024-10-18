using TMPro;
using UnityEngine;

public class PowerupDisplay : MonoBehaviour
{
    public TextMeshProUGUI freezeText;
    public TextMeshProUGUI speedupText;
    public TextMeshProUGUI invisibilityText;
    public TextMeshProUGUI navigationText;

    private void Start()
    {
        PowerupManager powerupManager = PowerupManager.instance;
        powerupManager.LoadPowerups();

        // Subscribe to the power-up updated event
        powerupManager.OnPowerupUpdated += UpdatePowerupDisplay;

        // Initial display update
        UpdatePowerupDisplay();
    }

    private void OnDestroy()
    {
        // Unsubscribe when this object is destroyed to avoid memory leaks
        PowerupManager.instance.OnPowerupUpdated -= UpdatePowerupDisplay;
    }

    public void UpdatePowerupDisplay()
    {
        PowerupManager powerupManager = PowerupManager.instance;

        freezeText.text = powerupManager.freeze.ToString();
        speedupText.text = powerupManager.speedup.ToString();
        invisibilityText.text = powerupManager.invisibility.ToString();
        navigationText.text = powerupManager.navigation.ToString();

        Debug.Log("Power-up display updated.");
    }
}
