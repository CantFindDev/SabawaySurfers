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
        if (other.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().isServer)
        {
            SpawnSection();
        }
    }
    
    
    public void SpawnSection()
    {
        var obj = Instantiate(SectionPrefab,
            ParentSection.position + new Vector3(0, 0, ParentSection.GetChild(0).transform.localScale.x * 2),
            ParentSection.rotation);
        obj.name = "Section " + GameManager.Instance.ActiveSections.Count;
        GameManager.Instance.ActiveSections.Enqueue(obj);
        Destroy(gameObject);
    }
}
