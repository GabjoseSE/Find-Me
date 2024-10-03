using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // This is necessary for serialization
public class LoginResponse
{
    public string status;
    public string message;
    public string username;
}
public class SignUpResponse
{
    public string status;
    public string message;
}

[System.Serializable]
public class CoinsResponse
{
    public string status; // Status of the request
    public int coins; // Number of coins retrieved
    public string message; // Message from the server
}

// Create a class for ServerResponse
[System.Serializable]
public class ServerResponse
{
    public string status; // Status of the request
    public string message; // Message from the server
    public int coins; // Coins returned from the server 
}
