using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Chưa ẩn cửa ở đây.
        // PlayerInteraction sẽ ẩn sau khi animation chạy xong.
    }

    public void HideDoor()
    {
        gameObject.SetActive(false);
    }
}