using UnityEngine;

public class SectionMovement : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.GameStarted) transform.Translate(transform.forward * Time.deltaTime * GameManager.Instance.GameSpeed);
    }
}
