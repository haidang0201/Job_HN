using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;

    [SerializeField] private DeathUI deathUI;

    private bool isDead;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<PlayerMovement>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Die();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            Die();
        }
    }


    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        // Dừng Player
        if (movement != null)
        {
            movement.enabled = false;
        }

        // Death animation
        animator.SetTrigger("Death");

        // Hiện UI chết
        deathUI.Show();
    }
}