using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Require Rigidbody2D component
public class GroundCheck : MonoBehaviour
{
    [Tooltip("The layer that represents the ground.")]
    public LayerMask groundLayer;
    
    
    [Tooltip("Whether or not the object is currently on the ground.")]
    public bool isGrounded = false;

    private Rigidbody2D rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get reference to Rigidbody2D component
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the ground layer
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the collision is with the ground layer
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }
    
    
}
