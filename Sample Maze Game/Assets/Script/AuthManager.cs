using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AuthManager : MonoBehaviour
{
    // Input fields
    public InputField usernameInput;
    public InputField emailInput;   // Only for signup
    public InputField passwordInput;

    // URL to your PHP files
    private string signUpURL = "http://localhost/FindMe/add_player.php"; // Adjust according to your setup
    private string loginURL = "http://localhost/FindMe/login_player.php"; // You'll create this login PHP file

    // Signup Button
    public void SignUp()
    {
        StartCoroutine(SignUpUser(usernameInput.text, emailInput.text, passwordInput.text));
        
    }

    // Login Button
    public void Login()
    {
        StartCoroutine(LoginUser(usernameInput.text, passwordInput.text));

    }

    // Coroutine for signup
    private IEnumerator SignUpUser(string username, string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("email", email);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(signUpURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Sign Up Error: " + www.error);
            }
            else
            {
                Debug.Log("Sign Up Success: " + www.downloadHandler.text);
                SceneManager.LoadSceneAsync(0);
            }
        }
    }

    // Coroutine for login
    private IEnumerator LoginUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(loginURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Login Error: " + www.error);
            }
            else
            {
                Debug.Log("Login Success: " + www.downloadHandler.text);
                
            }
        }
    }
}
