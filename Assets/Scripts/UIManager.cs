using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    public GameObject interactIcon;


    void Awake()
    {
        Instance = this;

        interactIcon.SetActive(false);
    }


    public void ShowInteract()
    {
        interactIcon.SetActive(true);
    }


    public void HideInteract()
    {
        interactIcon.SetActive(false);
    }
}