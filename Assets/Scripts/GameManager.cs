using System;
using System.Collections.Generic;
using DG.Tweening;
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

   private void Update()
    {
         if (ActiveSections.Count > 0)
         {
                if (ActiveSections.Count > MaxSectionCount * 2) 
                {
                    Destroy(ActiveSections.Dequeue());
                }
         }
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Camera.main.transform.DOMove(BeanBestView.position,1f).SetEase(Ease.InSine).onComplete += () =>
        {
            Camera.main.transform.DORotate(IdealCameraPosition.rotation.eulerAngles,.5f).SetEase(Ease.InOutSine);
            Camera.main.transform.DOMove(IdealCameraPosition.position,1f).SetEase(Ease.InOutSine).onComplete += () =>
            {
                GameStarted = true;
            };
        };
    }
   
}
