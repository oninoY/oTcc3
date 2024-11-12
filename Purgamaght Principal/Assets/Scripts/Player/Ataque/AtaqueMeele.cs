using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueMeele : MonoBehaviour
{
    [SerializeField] private Transform controleAtaque;
    [SerializeField] private float raioAtaque;
    [SerializeField] private float danoAtaque;
    [SerializeField] private float tempoEntreAtaque;
    [SerializeField] private float tempoProximoAtaque;
    [SerializeField] private float knockbackDistance; // New field for knockback strength
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    private void Update()
    {
        if (tempoProximoAtaque > 0)
        {
            tempoProximoAtaque -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && tempoProximoAtaque <= 0)
        {
            Ataque();
            tempoProximoAtaque = tempoEntreAtaque;
        }
    }

    private void Ataque()
{
    animator.SetTrigger("Atacando");
    Collider2D[] objetos = Physics2D.OverlapCircleAll(controleAtaque.position, raioAtaque);

    foreach (Collider2D colisao in objetos)
    {
        if (colisao.CompareTag("Inimigo"))
        {
            // Apply damage
            colisao.transform.GetComponent<InimigoVida>().TomarDanoInimigo(danoAtaque);

            // Calculate knockback direction
            Vector2 knockbackDirection = (colisao.transform.position - controleAtaque.position).normalized;
            knockbackDirection.y = 0; // Zero out the Y component
            knockbackDirection.Normalize(); // Normalize again to ensure it's a unit vector

            // Calculate the target position
            Vector2 targetPosition = (Vector2)colisao.transform.position + knockbackDirection * knockbackDistance;

            // Move the enemy to the target position
            StartCoroutine(MoveEnemy(colisao.transform, targetPosition, 0.1f)); // Move over a short duration
        }
    }
}

// Coroutine to move the enemy smoothly
private IEnumerator MoveEnemy(Transform enemyTransform, Vector2 targetPosition, float duration)
{
    float elapsedTime = 0f;
    Vector2 startingPosition = enemyTransform.position;

    while (elapsedTime < duration)
    {
        enemyTransform.position = Vector2.Lerp(startingPosition, targetPosition, (elapsedTime / duration));
        elapsedTime += Time.deltaTime;
        yield return null; // Wait for the next frame
    }

    // Ensure the enemy ends up exactly at the target position
    enemyTransform.position = targetPosition;
}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controleAtaque.position, raioAtaque);
    }
}