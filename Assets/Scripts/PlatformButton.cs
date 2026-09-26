using UnityEngine;

public class PlatformButton : MonoBehaviour, IInteractable
{
    [SerializeField] private PlatformController platformController;

    public void Interact()
    {
        if (platformController == null)
            return;

        // Mở platform
        platformController.OpenPlatforms();

        // Ẩn Button 3D
        gameObject.SetActive(false);
    }
}