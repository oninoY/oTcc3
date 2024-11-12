using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoAtkMelee : MonoBehaviour
{

[SerializeField] private int dano;
public vidaPlayer playerVida;

        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerVida.TomarDano(dano);
        }
    }
}
