using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordToggle : MonoBehaviour
{
    public InputField passwordInputField;
    public InputField confirmpasswordInputField;
    public Button toggleButton0;
    public Button toggleButton1;
    public Sprite showPasswordIcon;   // Eye open icon
    public Sprite hidePasswordIcon;   // Eye closed icon
    private bool isPasswordVisible = false;

    void Start()
    {
        // Initialize button icon
        toggleButton0.image.sprite = hidePasswordIcon;
        toggleButton1.image.sprite = hidePasswordIcon;

        // Add listener to the button to toggle password visibility
        toggleButton0.onClick.AddListener(TogglePasswordVisibility);
        toggleButton1.onClick.AddListener(TogglePasswordVisibility);
        
    }

    void TogglePasswordVisibility()
    {
        isPasswordVisible = !isPasswordVisible;

        if (isPasswordVisible)
        {
            // Set to visible (standard text)
            passwordInputField.contentType = InputField.ContentType.Standard;
            confirmpasswordInputField.contentType = InputField.ContentType.Standard;

            toggleButton0.image.sprite = showPasswordIcon;  // Change icon to eye open
            toggleButton1.image.sprite = showPasswordIcon;  
        }
        else
        {
            // Set to hidden (password format)
            passwordInputField.contentType = InputField.ContentType.Password;
            confirmpasswordInputField.contentType = InputField.ContentType.Password;
            toggleButton0.image.sprite = hidePasswordIcon;  // Change icon to eye closed
            toggleButton1.image.sprite = hidePasswordIcon;  
        }

        // Force the InputField to update its text by "re-enabling" it
        passwordInputField.ForceLabelUpdate();
        confirmpasswordInputField.ForceLabelUpdate();
        
    }
}
