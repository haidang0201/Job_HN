using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject doorIcon;
    [SerializeField] GameObject platformIcon;


    private void Awake()
    {
        Instance = this;

        HideAll();
    }


    public void ShowDoorIcon()
    {
        HideAll();

        doorIcon.SetActive(true);
    }


    public void ShowPlatformIcon()
    {
        HideAll();

        platformIcon.SetActive(true);
    }


    public void HideAll()
    {
        doorIcon.SetActive(false);
        platformIcon.SetActive(false);
    }
}