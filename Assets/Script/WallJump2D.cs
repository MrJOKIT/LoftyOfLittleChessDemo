using UnityEngine;

public class WallJump2D : MonoBehaviour
{
    public float jumpForce = 400f;
    public LayerMask wallLayer;

    private Rigidbody2D rb;
    private new CapsuleCollider2D collider;
    private bool isTouchingWall;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        isTouchingWall = Physics2D.OverlapBox(transform.position, collider.size, 0, wallLayer);

        if (isTouchingWall && Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 jumpDirection = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jumpDirection;
        }
    }
}