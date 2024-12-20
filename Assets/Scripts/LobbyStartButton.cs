using PurrNet;
using PurrNet.Modules;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyStartButton : NetworkBehaviour
{
    public void OnStartButtonClicked()
    {
        LoadSceneOnServer();
    }

    [ServerRpc(requireOwnership:false)]
    public void LoadSceneOnServer()
    {
        var settings = new PurrSceneSettings();
        settings.isPublic = true; 
        settings.mode = LoadSceneMode.Single; 
        networkManager.sceneModule.LoadSceneAsync("LobbyScene", settings);
    }
    
}
