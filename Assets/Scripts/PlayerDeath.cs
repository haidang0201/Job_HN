using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;

    private Animator animator;
    private PlayerMovement movement;
    private Rigidbody rb;

    private bool isDead;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();

        deathPanel.SetActive(false);
    }


    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        // Khóa di chuyển
        if (movement != null)
            movement.enabled = false;

        // Dừng Rigidbody
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }

        // Chạy animation chết
        animator.SetTrigger("Death");

        // Chờ Death animation chạy xong
        StartCoroutine(DeathSequence());
    }


    private IEnumerator DeathSequence()
    {
        // Chờ vào state Death
        yield return null;

        while (!animator.GetCurrentAnimatorStateInfo(0)
            .IsName("Death"))
        {
            yield return null;
        }

        // Chờ animation Death chạy xong
        while (animator.GetCurrentAnimatorStateInfo(0)
            .normalizedTime < 1f)
        {
            yield return null;
        }

        // Hiện màn hình Restart
        deathPanel.SetActive(true);

        // Đóng băng game
        Time.timeScale = 0f;
    }
}