using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float dashMultiplier = 2f;
    private float dashTimer = 0.3f;
    private float dashCooldown = 1f;

    private bool isMoving = false;
    private bool isDashing = false;
    private bool canDash = true;

    private Rigidbody2D rb;
    private Collider2D col2d;
    private Animator animator;

    private Vector2 movement;
    private Vector2 lastMovement = new Vector2(0, -1);
    private float footstepTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");    
        movement.Normalize();

        isMoving = movement != Vector2.zero && !isDashing;
        if (isMoving)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                AudioManager audioManager =
                    Object.FindFirstObjectByType<AudioManager>();

                audioManager.PlaySFX(audioManager.playerFootstepSound);

                footstepTimer = audioManager.playerFootstepSound.length;
            }

            lastMovement = movement;

            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
        }
        else
        {
            // last movement should not be diagonal when not moving
            if (!isDashing)
            {
                if (Mathf.Abs(lastMovement.x) > Mathf.Abs(lastMovement.y))
                {
                    lastMovement = new Vector2(Mathf.Sign(lastMovement.x), 0);
                }
                else
                {
                    lastMovement = new Vector2(0, Mathf.Sign(lastMovement.y));
                }
            }
            animator.SetFloat("MoveX", lastMovement.x);
            animator.SetFloat("MoveY", lastMovement.y);
        }

        // only start timer once key is pressed
        bool dashPressed = Input.GetButtonDown("Fire3");
        if (dashPressed && !isDashing && canDash)
        {
            FindObjectOfType<AudioManager>().PlaySFX(FindObjectOfType<AudioManager>().dashSound);

            isDashing = true;
            canDash = false;
            StartCoroutine(StartDashTimer());
        }

        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsDashing", isDashing);
    }

    void FixedUpdate()
    {
        // just so you can still dash when you're not moving, celeste-style
        if (isDashing && !isMoving)
        {
            rb.linearVelocity = lastMovement * moveSpeed;
        }
        else
        {
            rb.linearVelocity = movement * moveSpeed;
        }
    }

    IEnumerator StartDashTimer()
    {
        col2d.enabled = false;
        moveSpeed *= dashMultiplier;
        yield return new WaitForSeconds(dashTimer);

        // player slowdown after dash, maybe replace later with lerp from 0 to moveSpeed?
        col2d.enabled = true;
        moveSpeed /= dashMultiplier * 2;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);

        moveSpeed *= 2;
        canDash = true;
    }
}