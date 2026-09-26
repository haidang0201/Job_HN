using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        gameObject.SetActive(false);
    }
}