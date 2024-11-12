using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBife : MonoBehaviour
{
    public float vidaNova = 20f; // Valor de recupera��o de vida
    private vidaPlayer player; // Refer�ncia ao script de vida do jogador
    private bool isPlayerInRange; // Verifica se o jogador est� perto do item

    void Start()
    {
        player = FindObjectOfType<vidaPlayer>();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Coletar();
        }
    }

    private void Coletar()
    {
        player.ApplyVida(vidaNova); // Recupera a vida do jogador
        Destroy(gameObject); // Remove o item da cena
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}



