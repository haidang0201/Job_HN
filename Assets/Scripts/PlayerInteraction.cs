using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable current;


    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable =
            other.GetComponent<IInteractable>();

        if (interactable == null)
            return;

        current = interactable;

        UIManager.Instance.ShowInteract();
    }


    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable =
            other.GetComponent<IInteractable>();

        if (interactable == null)
            return;

        if (current == interactable)
        {
            current = null;

            UIManager.Instance.HideInteract();
        }
    }


    public void Interact()
{
    if (current == null)
        return;

    current.Interact();

    current = null;

    UIManager.Instance.HideInteract();
}
}