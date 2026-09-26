using UnityEngine;


public class UIInteractTest : MonoBehaviour
{
    public GameObject interactButton;


    void Start()
    {
        // Test: hiện icon khi vào game
        interactButton.SetActive(true);
    }


    public void OnClickInteract()
    {
        // Click icon thì ẩn nó
        interactButton.SetActive(false);
    }
}