using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   
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

   public float MaxSectionCount = 2;
   public Queue<GameObject> ActiveSections = new Queue<GameObject>();

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
   
}
