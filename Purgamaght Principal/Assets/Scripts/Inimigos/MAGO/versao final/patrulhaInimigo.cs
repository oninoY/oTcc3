using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrulhaInimigo : MonoBehaviour
{
    public float speed;
    private int direction = 1;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("parede"))
        {
            Flip();
        }
    }

    private void Flip()
    {
        direction *= -1;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
