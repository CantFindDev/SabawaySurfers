using System;
using PurrNet;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BuildType
{
    Server,
    Client
}

public class ServerToggle: MonoBehaviour
{
    public BuildType BuildType;

    private void Start()
    {
        var NetworkManager = GetComponent<NetworkManager>();
        
        if (BuildType == BuildType.Client)
        {
            NetworkManager.StartClient();
            SceneManager.LoadSceneAsync("LobbyScene", LoadSceneMode.Additive);
        }
        else
        {
            NetworkManager.StartServer();
        }
    }
}