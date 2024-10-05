[System.Serializable]
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
