using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 1f;
    private bool isFacingRight = true;
    private bool doubleJump;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 1.5f;
    public Vector2 wallJumpingPower = new Vector2(12f, 24f);

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 18f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private Animator anim;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    internal static GameObject currentControlledObject;

    public bool isControlled = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isControlled)
        {
            if (isDashing)
            {
                return;
            }

            float moveInput = horizontal = Input.GetAxisRaw("Horizontal");

            if (moveInput == 0)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }

            if (IsGrounded() && !Input.GetButton("Jump"))
            {
                doubleJump = false;
                anim.SetBool("isJumping", false); // Grounded olduðunda zýplama animasyonunu kapat
                isWallJumping = false; // Grounded olduðunda duvar zýplamayý kapat
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (IsGrounded())
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                    doubleJump = true;
                    anim.SetBool("isJumping", true); // Ýlk zýplama baþladýðýnda animasyonu aç
                }
                else if (doubleJump && !isWallSliding)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                    doubleJump = false;
                    anim.SetTrigger("DoubleJump"); // Çift zýplama baþladýðýnda animasyonu aç
                }
            }

            if (Input.GetButton("Jump") && !IsGrounded())
            {
                anim.SetBool("isJumping", true); // Havada olduðunda animasyonu aç
            }

            if (Input.GetButtonUp("Jump"))
            {
                anim.SetTrigger("isJumping"); // Space tuþunu býrakýnca animasyonu kapat
                if (rb.velocity.y > 0f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }

            WallSlide();
            WallJump();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing && !isWallJumping)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        Flip(horizontal); // Flip fonksiyonunu burada çaðýrýyoruz
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded())
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));

            // Karakterin baktýðý yöne göre duvar zýplama yönünü ayarla
            wallJumpingDirection = isFacingRight ? -1f : 1f;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.z *= -1f;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }

        // Karþý duvara zýplama kontrolü
        if (IsWalled() && !isWallJumping && !IsGrounded() && Input.GetButtonDown("Jump"))
        {
            float oppositeDirection = -Mathf.Sign(horizontal);
            rb.velocity = new Vector2(wallJumpingPower.x * oppositeDirection, wallJumpingPower.y);
            isWallJumping = true;
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }




    private void Flip(float moveInput)
    {
        if (isFacingRight && moveInput < 0f || !isFacingRight && moveInput > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);  // Y ekseni etrafýnda 180 derece döndür
        }
    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

