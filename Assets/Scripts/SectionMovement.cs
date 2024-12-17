using UnityEngine;

public class SectionMovement : MonoBehaviour
{
    public float Speed = 5;
    
    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * Speed);
    }
}
