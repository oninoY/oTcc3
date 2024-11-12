using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemManager itemManager;
    private bool isPlayerInRange; // Verifica se o jogador est� perto do item
    private ItemPatins itemPatins; // Refer�ncia ao ItemPatins

    void Start()
    {
        // Encontra o ItemManager automaticamente na cena
        itemManager = FindObjectOfType<ItemManager>();
        itemPatins = GetComponent<ItemPatins>(); // Obt�m o componente ItemPatins, se existir
    }

    void Update()
    {
        // Verifica se o jogador est� perto e se a tecla "E" foi pressionada
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (itemPatins != null) // Se for o item de patins
            {
                itemManager.CollectItem(gameObject); // Chama o m�todo CollectItem do ItemManager
                itemPatins.Coletar(); // Chama o m�todo Coletar do ItemPatins
            }
            else
            {
                itemManager.SelectItem(gameObject); // Seleciona o item normalmente se n�o for patins
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ativa a �rea de intera��o ao detectar o jogador
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Desativa a �rea de intera��o quando o jogador sai
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}




