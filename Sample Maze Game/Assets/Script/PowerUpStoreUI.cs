using UnityEngine;
using UnityEngine.UI;

public class PowerUpStoreUI : MonoBehaviour
{
    public GameObject powerUp1Panel;
    public GameObject powerUp2Panel;
    public GameObject powerUp3Panel;
    public GameObject powerUp4Panel;

    public Button powerUp1Button;
    public Button powerUp2Button;
    public Button powerUp3Button;
    public Button powerUp4Button;

    public Button powerUp1ConfirmButton;
    public Button powerUp1CancelButton;
    public Button powerUp2ConfirmButton;
    public Button powerUp2CancelButton;
    public Button powerUp3ConfirmButton;
    public Button powerUp3CancelButton;
    public Button powerUp4ConfirmButton;
    public Button powerUp4CancelButton;

    // Start is called before the first frame update
    void Start()
    {
        // Hide all power-up panels at the start
        HideAllPanels();

        // Add listeners for the buttons
        powerUp1Button.onClick.AddListener(ShowPowerUp1Panel);
        powerUp2Button.onClick.AddListener(ShowPowerUp2Panel);
        powerUp3Button.onClick.AddListener(ShowPowerUp3Panel);
        powerUp4Button.onClick.AddListener(ShowPowerUp4Panel);

        // Add listeners for the cancel buttons
        powerUp1CancelButton.onClick.AddListener(HideAllPanels);
        powerUp2CancelButton.onClick.AddListener(HideAllPanels);
        powerUp3CancelButton.onClick.AddListener(HideAllPanels);
        powerUp4CancelButton.onClick.AddListener(HideAllPanels);

        // Add listeners for confirm buttons
        powerUp1ConfirmButton.onClick.AddListener(() => ConfirmPurchase(1));
        powerUp2ConfirmButton.onClick.AddListener(() => ConfirmPurchase(2));
        powerUp3ConfirmButton.onClick.AddListener(() => ConfirmPurchase(3));
        powerUp4ConfirmButton.onClick.AddListener(() => ConfirmPurchase(4));
    }

    // Function to hide all panels
    private void HideAllPanels()
    {
        powerUp1Panel.SetActive(false);
        powerUp2Panel.SetActive(false);
        powerUp3Panel.SetActive(false);
        powerUp4Panel.SetActive(false);
    }

    // Functions to show specific panels
    private void ShowPowerUp1Panel()
    {
        HideAllPanels();
        powerUp1Panel.SetActive(true);
    }

    private void ShowPowerUp2Panel()
    {
        HideAllPanels();
        powerUp2Panel.SetActive(true);
    }

    private void ShowPowerUp3Panel()
    {
        HideAllPanels();
        powerUp3Panel.SetActive(true);
    }

    private void ShowPowerUp4Panel()
    {
        HideAllPanels();
        powerUp4Panel.SetActive(true);
    }

    // Confirm purchase function
    private void ConfirmPurchase(int powerUpId)
    {
        Debug.Log("Power-Up " + powerUpId + " purchased!");
        HideAllPanels();


        // Add your purchasing logic here, e.g., deduct coins, update database, etc.

        
    }

}
