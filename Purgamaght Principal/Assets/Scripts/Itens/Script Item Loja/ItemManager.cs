using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private GameObject selectedItem;
    public GameObject saida;

    public void SelectItem(GameObject item)
    {
        // Se j� houver um item selecionado e ele for diferente do atual, destr�i o anterior
        if (selectedItem != null && selectedItem != item)
        {
            Destroy(selectedItem);
        }

        // Define o novo item como o item selecionado
        selectedItem = item;

        // Destr�i todos os outros itens na cena que possuem o script Item
        foreach (Item otherItem in FindObjectsOfType<Item>())
        {
            if (otherItem.gameObject != selectedItem)
            {
                Destroy(otherItem.gameObject);
            }
        }

        // Destr�i o item selecionado ap�s a coleta
        Destroy(selectedItem);
    }

    public void CollectItem(GameObject item)
    {
        SelectItem(item); // Chama SelectItem para lidar com a sele��o e destrui��o
    }
}





