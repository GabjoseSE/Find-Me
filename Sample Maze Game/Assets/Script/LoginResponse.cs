using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // This is necessary for serialization
public class LoginResponse
{
    public string status;
    public string message;
}
public class SignUpResponse
{
    public string status;
    public string message;
}

[System.Serializable]
public class ServerResponse
{
    public string status;
    public string message;
}