using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPatrulha : Inimigo
{
    [Header("Attack Properties")]
    public float timerWaitAttack;
    public float timerShootAttack;

    private bool shoot;
    private bool idle;
    private Weapon weapon;

    // New variable to prevent continuous flipping
    private bool justFlipped = false;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    protected override void Update()
    {
        base.Update();

        // Check if the enemy is grounded and not against a wall
        bool isGrounded = RaycastGround().collider;
        bool isWall = RaycastWall().collider;

        // Debugging: Log raycast results
        Debug.DrawRay(transform.position, Vector2.down * 0.1f, Color.red); // Ground check
        Debug.DrawRay(transform.position, Vector2.right * direction * 0.1f, Color.blue); // Wall check

        // Flip if not grounded (ground check) and not hitting a wall
        if (!isGrounded && !justFlipped)
        {
            Flip();
            justFlipped = true; // Set to true to prevent immediate flipping back
        }
        else if (isGrounded)
        {
            justFlipped = false; // Reset when grounded
        }
    }

    private void FixedUpdate()
    {
        if (CanAttack())
        {
            Attack();
        }
        else
        {
            Movement();
        }
    }

    private void Movement()
    {
        if (animator.GetBool("walk"))
        {
            animator.Play("MagoAndando");
        }
        else
        {
            animator.SetBool("walk", true);
        }

        idle = false;
        animator.SetBool("idle", false);
        float horizontalVelocity = speed * direction;
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
    }

    private bool CanAttack()
    {
        return Mathf.Abs(playerDistance) <= attackDistance;
    }

    private void Attack()
    {
        StopMovement();
        DistancieFlipPlayer();
        CanShoot();
    }

    private void StopMovement()
    {
        if (!animator.GetBool("idle"))
        {
            animator.Play("Idle");
            animator.SetBool("idle", true);
        }
        rb.velocity = Vector3.zero;
        idle = true;
    }

    private void DistancieFlipPlayer()
    {
        if (playerDistance >= 0 && direction == -1)
        {
            Flip();
        }
        else if (playerDistance < 0 && direction == 1)
        {
            Flip();
        }
    }

    private void CanShoot()
    {
        if (!shoot && idle)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        shoot = true;
        yield return new WaitForSeconds(timerWaitAttack);
        AnimationShoot();
        ShootPrefab(); // Call the method to shoot the projectile
        yield return new WaitForSeconds(timerShootAttack);
        shoot = false;
    }

    private void AnimationShoot()
    {
        animator.Play("InimigoMagoAtaque");
    }

    private void ShootPrefab()
    {
        if (weapon != null)
        {
            weapon.Shoot();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Invert the X scale
        transform.localScale = scale;

        // Log the current rotation for debugging
        Debug.Log("Current Rotation: " + transform.rotation.eulerAngles.z);

        direction *= -1; // Invert the direction variable
    }

    // Raycast methods to check for ground and walls
   private RaycastHit2D RaycastGround()
{
    // Cast a ray downwards to check for ground
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
    
    // Check if the hit collider is not null and has the "piso" tag
    if (hit.collider != null && hit.collider.CompareTag("piso"))
    {
        return hit; // Return the hit if it collides with the "piso" tagged object
    }
    
    // Return an empty RaycastHit2D if it doesn't hit anything or the hit is not "piso"
    return new RaycastHit2D(); 
}
    private RaycastHit2D RaycastWall()
    {
        // Cast a ray in the direction the enemy is facing to check for walls
        return Physics2D.Raycast(transform.position, Vector2.right * direction, 0.1f, LayerMask.GetMask("Wall"));
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Inimigo" tag
        if (collision.gameObject.CompareTag("Inimigo") || collision.gameObject.CompareTag("saida"))
        {
            // Ignore the collision
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

}
