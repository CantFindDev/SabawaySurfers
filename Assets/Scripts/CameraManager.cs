using PurrNet;
using UnityEngine;

public class CameraManager : NetworkBehaviour
{
  public GameObject Camera;
  protected override void OnSpawned(bool asServer)
  {
    base.OnSpawned(asServer);
      Camera.SetActive(isOwner);
  }
}
