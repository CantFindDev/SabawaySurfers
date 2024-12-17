using System;
using UnityEngine;

public class SectionTriggerScript : MonoBehaviour
{
    public GameObject SectionPrefab;
    private Transform ParentSection;
    
    private void Start()
    {
        ParentSection = gameObject.transform.parent.gameObject.transform;
        GameManager.Instance.ActiveSections.Enqueue(ParentSection.gameObject);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var obj = Instantiate(SectionPrefab,
                ParentSection.position + new Vector3(0, 0, ParentSection.GetChild(0).transform.localScale.x * 2),
                ParentSection.rotation);
            obj.name = SectionPrefab.gameObject.name + " " + GameManager.Instance.ActiveSections.Count;
        GameManager.Instance.ActiveSections.Enqueue(obj);
        }
    }
}
