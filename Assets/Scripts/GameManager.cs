using System;
using System.Collections.Generic;
using DG.Tweening;
using PurrNet;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
   private void Awake()
   {
       if (Instance == null)
       {
           Instance = this;
       }
       else
       {
           Destroy(gameObject);
       }
   }
   public static GameManager Instance;
   
   public SyncVar<bool> GameStarted =new( false,ownerAuth:false);
   public float GameSpeed { get; private set;} = 10;
   public float MaximumGameSpeed { get; private set;} = 0.1f;
   [SerializeField] private float GameSpeedIncreaseRate = 0.1f;
   public float MaxSectionCount = 2;
   public Queue<GameObject> ActiveSections = new Queue<GameObject>();
   
   public Transform IdealCameraPosition, BeanBestView;
   public GameObject PlayerPrefab;
   public GameObject PlayerLanePrefab;
   public List<GameObject> ActivePlayers;
   public List<GameObject> PlayerLanes;

   private void Update()
    {
        if (!GameStarted) StartGame();
         if (ActiveSections.Count > 0)
         {
                if (ActiveSections.Count > MaxSectionCount * 2) 
                {
                    Destroy(ActiveSections.Dequeue());
                }
         }
    }
   
    public void StartGame()
    {
        GameStarted.value = ActivePlayers.Count == 2;
    }
   
}
