using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemManager itemManager;
    private bool isPlayerInRange; // Verifica se o jogador está perto do item
    private ItemPatins itemPatins; // Referência ao ItemPatins

    void Start()
    {
        // Encontra o ItemManager automaticamente na cena
        itemManager = FindObjectOfType<ItemManager>();
        itemPatins = GetComponent<ItemPatins>(); // Obtém o componente ItemPatins, se existir
    }

    void Update()
    {
        // Verifica se o jogador está perto e se a tecla "E" foi pressionada
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (itemPatins != null) // Se for o item de patins
            {
                itemManager.CollectItem(gameObject); // Chama o método CollectItem do ItemManager
                itemPatins.Coletar(); // Chama o método Coletar do ItemPatins
            }
            else
            {
                itemManager.SelectItem(gameObject); // Seleciona o item normalmente se não for patins
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ativa a área de interação ao detectar o jogador
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Desativa a área de interação quando o jogador sai
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}




