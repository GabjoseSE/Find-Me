using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite; // SQLite support
using System.Data;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField emailInput; // For signup
    public InputField passwordInput;
    
    private string dbPath;

    void Start()
    {
        dbPath = "URI=file:" + Application.persistentDataPath + "/findme.db";
    }

    // Signup Logic
    public void SignUp()
    {
        string username = usernameInput.text;
        string email = emailInput.text;
        string password = passwordInput.text;

        if (InsertNewPlayer(username, email, password))
        {
            Debug.Log("Signup Success! User created.");
            SceneManager.LoadSceneAsync(0);


        }
        else
        {
            Debug.LogError("Signup Failed! Username already exists.");
        }
    }

    private bool InsertNewPlayer(string username, string email, string password)
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                // Check if username exists
                command.CommandText = "SELECT COUNT(*) FROM players WHERE username=@username";
                command.Parameters.AddWithValue("@username", username);
                int userExists = System.Convert.ToInt32(command.ExecuteScalar());

                if (userExists > 0)
                {
                    return false; // Username already exists
                }

                // Insert new player
                command.CommandText = "INSERT INTO players (username, email, password) VALUES (@username, @email, @password)";
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);
                command.ExecuteNonQuery();
            }
        }
        return true; // Signup success
    }
    public void Login()
{
    string username = usernameInput.text;
    string password = passwordInput.text;

    if (AuthenticatePlayer(username, password))
    {
        Debug.Log("Login Success! Welcome, " + username);
        SceneManager.LoadSceneAsync(2);
    }
    else
    {
        Debug.LogError("Login Failed! Invalid credentials.");
    }
}

private bool AuthenticatePlayer(string username, string password)
{
    using (var connection = new SqliteConnection(dbPath))
    {
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            // Verify player credentials
            command.CommandText = "SELECT COUNT(*) FROM players WHERE username=@username AND password=@password";
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            int isValidUser = System.Convert.ToInt32(command.ExecuteScalar());

            return isValidUser > 0; // Return true if user exists
        }
    }
}

}