using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleMove : MonoBehaviour
{
    [Tooltip("The speed at which the enemy moves.")]
    public float moveSpeed = 5f;

    [Tooltip("The range of movement for the enemy.")]
    public float moveRange = 5f;

    [Tooltip("The distance at which the enemy will detect the player.")]
    public float detectionRange = 10f;

    [Tooltip("The layer mask for the player.")]
    public LayerMask playerLayer;

    public LayerMask wallMask;

    [Tooltip("The distance at which the enemy will stop moving towards the player.")]
    public float stopDistance = 1f;

    [Tooltip("The bullet prefab to shoot at the player.")]
    public Rigidbody2D bulletPrefab;

    [Tooltip("The speed at which the bullet travels.")]
    public float bulletSpeed = 10f;

    [Tooltip("The position from which the enemy will shoot.")]
    public Transform shootPosition;

    [Tooltip("The delay between shots.")]
    public float shootDelay = 1f;

    private float initialPosition;
    private bool movingRight = true;
    private bool foundPlayer = false;
    private Transform playerTransform;

    public Transform groundCheckPos;
    public bool mustTurn;
    public LayerMask groundLayer;

    private float lastShotTime = 0f;

    private void Start()
    {
        // Store the initial position of the enemy
        initialPosition = transform.position.x;
    }

    private void Update()
    {
        if (!foundPlayer )
        {
            // Check if the player is within detection range
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
            if (playerCollider != null )
            {
                foundPlayer = true;
                playerTransform = playerCollider.transform;
            }
        }
        else
        {
            // Move towards the player
            float direction = playerTransform.position.x > transform.position.x ? 1f : -1f;
            float distanceToPlayer = Mathf.Abs(transform.position.x - playerTransform.position.x);
            if (distanceToPlayer > stopDistance)
            {
                transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
            }

            // Check if the enemy has reached the player
            if (distanceToPlayer < 0.5f)
            {
                foundPlayer = false;
            }
            else
            {
                // Shoot a bullet at the player
                if (Time.time - lastShotTime > shootDelay)
                {
                    Vector2 projectileVelocity = 
                        CalculateProjectileVelocity(shootPosition.position, playerTransform.position, bulletSpeed);
                    Rigidbody2D bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.identity);
                    bullet.velocity = projectileVelocity;
                    //bullet.GetComponent<Rigidbody2D>().velocity = (playerTransform.position - shootPosition.position).normalized * bulletSpeed;
                    lastShotTime = Time.time;
                }
            }
        }

        if (!foundPlayer)
        {
            // Move the enemy left or right based on its speed and direction
            float direction = movingRight ? 1f : -1f;

            transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

            // Check if the enemy has reached the end of its range
            if (Mathf.Abs(transform.position.x - initialPosition) >= moveRange)
            {
                // Change direction and move back
                movingRight = !movingRight;
                transform.Translate(Vector3.right * -direction * moveSpeed * Time.deltaTime);
            }
        }

        // Check if the player has exited the enemy's detection range
        if (foundPlayer && Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer) == null)
        {
            foundPlayer = false;
        }
        else if (foundPlayer)
        {
            if (playerTransform.gameObject.GetComponent<PlayerHealth>().IsDead)
            {
                foundPlayer = false;
            }
        }

    }

    private void FixedUpdate()
    {
        mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        if (!foundPlayer)
        {
            if (mustTurn)
            {
                float direction = movingRight ? 1f : -1f;
                movingRight = !movingRight;
                transform.Translate(Vector3.right * -direction * moveSpeed * Time.deltaTime);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection range for the enemy
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (((1 << col.gameObject.layer) & wallMask) != 0)
        {
            float direction = movingRight ? 1f : -1f;
            movingRight = !movingRight;
            transform.Translate(Vector3.right * -direction * moveSpeed * Time.deltaTime);
            mustTurn = true;
        }
    }
    
    Vector2 CalculateProjectileVelocity( Vector2 origin , Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        float disX = distance.x;
        float disY = distance.y;

        float velocityX = disX / time;
        float velocityY = disY / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        Vector2 result = new Vector2(velocityX, velocityY);

        return result;
        
    }
    
}
