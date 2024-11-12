using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoMelee : MonoBehaviour
{
    private float tempoCooldown = Mathf.Infinity;
    [SerializeField] private float cooldownAtaque;
    [SerializeField] private float range;
    [SerializeField] private float colisorDistancia;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject perseguirPlayer;
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private bool viradoEsquerda = false;
    
void Start()
{
    perseguirPlayer = GameObject.FindGameObjectWithTag("Player");
}

void Update()
{
    tempoCooldown += Time.deltaTime;

    if (VisaoJogador())
    {
        Debug.Log("Player in sight");
        if (tempoCooldown >= cooldownAtaque)
        {
            tempoCooldown = 0;
            animator.SetTrigger("Ataque");
        }
    }

    Perseguir();
}
    
    
    private void Perseguir()
    {
        if (perseguirPlayer != null)
        {
            distance = Vector2.Distance(transform.position, perseguirPlayer.transform.position);
        }

        if (perseguirPlayer != null)
        {
            if (distance < 18)
            {
                if (!animator.GetBool("walk"))
                {
                animator.Play("InimigoMelleAndando");
                animator.SetBool("walk" , true);
                }
                Vector2 direcao = (perseguirPlayer.transform.position - transform.position).normalized;
                direcao.y = 0f;
            
                if (direcao.x < 0 && !viradoEsquerda)
                {
                    FlipX();
                    viradoEsquerda = true;
                }
                
                else if (direcao.x > 0 && viradoEsquerda)
                {
                    FlipX();
                    viradoEsquerda = false;
                }

            transform.Translate(direcao * speed * Time.deltaTime);
        }
    }
}


private void FlipX()
{
    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

    foreach (Transform child in transform)
    {
        child.localScale = new Vector3(-child.localScale.x, child.localScale.y, child.localScale.z);
    }

    Transform hitBoxAtk = transform.Find("HitBoxAtk");
    if (hitBoxAtk != null)
    {
        hitBoxAtk.localScale = new Vector3(-hitBoxAtk.localScale.x, hitBoxAtk.localScale.y, hitBoxAtk.localScale.z);
    }
}


private bool VisaoJogador()
{
    RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colisorDistancia,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);
    
    bool playerDetected = hit.collider != null;
    if (playerDetected)
    {
        Debug.Log("Player detected!");
    }
    else
    {
        Debug.Log("No player detected.");
    }
    return playerDetected;
}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colisorDistancia,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
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
