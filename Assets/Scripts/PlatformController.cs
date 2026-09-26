using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;

    private bool isOpened;


    public void OpenPlatforms()
    {
        if (isOpened)
            return;

        isOpened = true;

        foreach (GameObject platform in platforms)
        {
            platform.SetActive(true);
        }
    }
}