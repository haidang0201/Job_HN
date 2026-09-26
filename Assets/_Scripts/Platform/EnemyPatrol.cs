using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private Animator animator;

    private bool attacking;
    private bool playerDead;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }


    private void Start()
    {
        target = pointB;
    }


    private void Update()
    {
        if (playerDead || attacking)
            return;

        Patrol();
    }


    private void Patrol()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        LookAt(target.position);


        if (Vector3.Distance(
            transform.position,
            target.position) < 0.1f)
        {
            target = target == pointA
                ? pointB
                : pointA;
        }
    }


    // Enemy chạm Player
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;


        PlayerDeath playerDeath =
            other.GetComponentInParent<PlayerDeath>();

        if (playerDeath == null)
            return;


        // Enemy chạy Attack
        attacking = true;

        animator.SetBool("Attack", true);


        // Player chết
        playerDeath.Die();

        playerDead = true;
    }


    private void LookAt(Vector3 position)
    {
        Vector3 direction =
            position - transform.position;

        direction.y = 0;


        if (direction.sqrMagnitude > 0.01f)
        {
            transform.forward =
                direction.normalized;
        }
    }
}