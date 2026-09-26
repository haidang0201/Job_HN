using UnityEngine;

public class DeathTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerDeath player =
            other.GetComponent<PlayerDeath>();

        if (player != null)
        {
            player.Die();
        }
    }
}