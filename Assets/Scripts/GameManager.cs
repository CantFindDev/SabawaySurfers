using System;
using System.Collections.Generic;
using DG.Tweening;
using PurrNet;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
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
   
   public bool GameStarted = false;
   public float GameSpeed { get; private set;} = 10;
   public float MaximumGameSpeed { get; private set;} = 0.1f;
   [SerializeField] private float GameSpeedIncreaseRate = 0.1f;
   public float MaxSectionCount = 2;
   public Queue<GameObject> ActiveSections = new Queue<GameObject>();
   
   public Transform IdealCameraPosition, BeanBestView;
   public GameObject PlayerPrefab;
   public GameObject PlayerLanePrefab;
   public List<GameObject> ActivePlayers = new List<GameObject>();
   public List<GameObject> PlayerLanes = new List<GameObject>();

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
        GameStarted = ActivePlayers.Count == 2;
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        Application.targetFrameRate = 60;
    }
   
}
