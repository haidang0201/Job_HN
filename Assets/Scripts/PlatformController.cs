using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;

    private bool isOpened;


    private void Awake()
    {
        SetPlatforms(false);
    }


    public void OpenPlatforms()
    {
        if (isOpened)
            return;

        isOpened = true;

        SetPlatforms(true);
    }


    private void SetPlatforms(bool active)
    {
        foreach (GameObject platform in platforms)
        {
            if (platform != null)
            {
                platform.SetActive(active);
            }
        }
    }
}