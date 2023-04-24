using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public ParticleSystem dust;
    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    private bool facingRight = true;
    public bool canMove = true;
    private bool inCombat = false;

    [Header("Vertical Movement")] 
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    public float jumpTimer;
    

    [Header("Components")]
    private Rigidbody2D rb;
    private PlayerAnimation playerAnimation;
    private SoundManager soundManager;
    private Animator animator;
    [SerializeField] private Vignette vignette;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiply = 5f;

    [Header("Collision")] 
    public bool onGround = false;
    public bool onPlatform = false;
    public float groundLength = 1.05f;
    public Vector3 colliderOffset;
    public LayerMask groundLayer;

    private float maxVelocityX;

    public bool FacingRight
    { 
        get { return facingRight; }
    }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }
    
    public bool InCombat
    {
        get { return inCombat; }
        set { inCombat = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAnimation = GetComponent<PlayerAnimation>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        
    }

    void Update()
    {
        bool wasOnGround = onGround;
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) 
                   || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if (!wasOnGround && onGround)
        {
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }
        
        if (Input.GetButtonDown("Jump") && onGround && canMove && !inCombat || Input.GetButtonDown("Jump") && onPlatform && canMove && !inCombat)
        {
            jumpTimer = Time.time + jumpDelay;
        }
        
        if (onGround)
        {
            if (direction.x == 0 && direction.y == 0 && rb.velocity.y >= 0 )
            {
                animator.SetBool("Idle",true);
                animator.SetBool("Jumping",false);
            }
            else if(direction.x != 0 || direction.y != 0)
            {
                animator.SetBool("Jumping",false);
                animator.SetBool("Idle",false);
            }
            
            animator.SetBool("Jumping",false);
            animator.SetBool("Falling",false);
            animator.SetFloat("Vertical",0f);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        else if (rb.velocity.y < 0)
        {
            animator.SetBool("Falling",true);
        }
        
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    void FixedUpdate(){

        if (canMove)
        {
            MoveCharacter(direction.x);
            if (jumpTimer > Time.time && onGround || jumpTimer > Time.time && onPlatform)
            {
                Jump();
            }
        }
        modifyPhysics();
    }
    void MoveCharacter(float horizontal){
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }
        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        animator.SetFloat("Horizontal",Mathf.Abs(direction.x));
        animator.SetFloat("Vertical",direction.y);
        
    }
    
    private void Jump()
    {
        CreateDust();
        SoundManager.instace.Play(SoundManager.SoundName.Jump);
        animator.SetBool("Jumping",true);
        //playerAnimation.State = PlayerAnimation.PlayerState.Jump;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed,ForceMode2D.Impulse);
        jumpTimer = 0f;
        StartCoroutine(JumpSqueeze(0.5f,1.2f,0.1f));
    }
    void modifyPhysics(){
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) ||  (direction.x < 0 && rb.velocity.x > 0);

        if (onGround)
        {
            if(Mathf.Abs(direction.x) < 0.4f || changingDirections){
                rb.drag = linearDrag;
            }
            else{
                rb.drag = 0f;
            }

            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiply;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiply / 2);
            }
        }
        
        
    }
    void Flip(){
        CreateDust();
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
    
    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds) 
    {
        CreateDust();
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset,transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset,transform.position - colliderOffset + Vector3.down * groundLength);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("OneWayPlatform"))
        {
            if (direction.x == 0 && direction.y == 0 && rb.velocity.y >= 0 )
            {
                animator.SetBool("Idle",true);
                animator.SetBool("Jumping",false);
            }
            else if(direction.x != 0 || direction.y != 0)
            {
                animator.SetBool("Jumping",false);
                animator.SetBool("Idle",false);
            }
            
            animator.SetBool("Jumping",false);
            animator.SetBool("Falling",false);
            animator.SetFloat("Vertical",0f);
            onPlatform = true;
            //rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }
    
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("OneWayPlatform"))
        {
            onPlatform = false;
            //rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    public void CreateDust()
    {
        dust.Play();
    }

    public void OpenDialogue()
    {
        vignette.intensity.value = 0.7f;
        rb.velocity = Vector2.zero;
        direction = Vector2.zero;
        animator.SetFloat("Horizontal",0);
        animator.SetFloat("Vertical",0);
        animator.SetBool("Jumping",false);
        animator.SetBool("Falling",false);
        animator.SetBool("Idle",true);
        canMove = false;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CloseDialogue()
    {
        canMove = true;
        Cursor.visible = false;
        vignette.intensity.value = 0.3f;
    }
}