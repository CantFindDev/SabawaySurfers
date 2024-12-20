using PurrNet;
using PurrNet.Modules;
using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEngine;

public class NetworkPlayerSpawner : PurrMonoBehaviour
{
    GameManager gm;
    private PlayerID ClientID;
    private PurrMonoBehaviour _purrMonoBehaviourImplementation;
    
    public override void Subscribe(NetworkManager manager, bool asServer)
    {
        if (asServer && manager.TryGetModule(out ScenePlayersModule scenePlayersModule, true)) scenePlayersModule.onPlayerLoadedScene += OnPlayerLoadedScene;
    }
    
    public override void Unsubscribe(NetworkManager manager, bool asServer)
    {
        if (asServer && manager.TryGetModule(out ScenePlayersModule scenePlayersModule, true)) scenePlayersModule.onPlayerLoadedScene -= OnPlayerLoadedScene;
    }
    
    private void OnPlayerLoadedScene(PlayerID player, SceneID scene, bool asServer)
    {
        if (!NetworkManager.main.TryGetModule(out ScenesModule scenes, true))
            return;
    
        var unityScene = gameObject.scene;
            
        if (!scenes.TryGetSceneID(unityScene, out var sceneID))
            return;
            
        if (sceneID != scene)
            return;
        
        bool isDestroyOnDisconnectEnabled = NetworkManager.main.networkRules.ShouldDespawnOnOwnerDisconnect();
        if (!isDestroyOnDisconnectEnabled && NetworkManager.main.TryGetModule(out GlobalOwnershipModule ownership, true) && 
            ownership.PlayerOwnsSomething(player))
            return;
        
        ClientID = player;
        
       SpawnLane();
    }
    
    private void Start() => gm = GameManager.Instance;
    
    public void SpawnPlayer()
    {
            int ActivePlayerCount = gm.ActivePlayers.Count;
            var spawnTransform = gm.PlayerLanes[ActivePlayerCount].transform.GetChild(1).transform; //Problem
            var PlayerObject = Instantiate(gm.PlayerPrefab, spawnTransform.position, spawnTransform.rotation);
            PlayerObject.name = "Player " + ActivePlayerCount;
            PlayerObject.GetComponent<PlayerController>().GiveOwnership(ClientID);
            gm.ActivePlayers.Add(PlayerObject);
            for (int j = 0; j < gm.PlayerLanes[ActivePlayerCount].transform.childCount; j++)
            {
                PlayerObject.GetComponent<PlayerController>().PlayerPositions.Add(GameManager.Instance.PlayerLanes[ActivePlayerCount].transform.GetChild(j).transform); //Broken
            }
    }
    
    public void SpawnLane()
    {
            int LaneCount = gm.PlayerLanes.Count;
            var LaneObject = Instantiate(gm.PlayerLanePrefab, new Vector3(0,0,LaneCount -1 * 3)  , quaternion.identity);
            LaneObject.name = "Lane " + LaneCount;
            gm.PlayerLanes.Add(LaneObject);
            SpawnPlayer();
    }
}
