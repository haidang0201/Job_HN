using UnityEngine;

public class PlatformButton : MonoBehaviour, IInteractable
{
    [SerializeField]
    private PlatformController platformController;


    public void Interact()
    {
        platformController.OpenPlatforms();

        // Ẩn Button3D sau khi đã sử dụng
        gameObject.SetActive(false);
    }
}