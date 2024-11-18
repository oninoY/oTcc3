using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoAtkMelee : MonoBehaviour
{
    [SerializeField] private int dano;
    public vidaPlayer playerVida;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerVida.TomarDano(dano);
        }
    }
}