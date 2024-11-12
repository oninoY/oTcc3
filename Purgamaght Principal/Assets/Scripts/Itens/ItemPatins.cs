using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPatins : MonoBehaviour
{
    public float aumentoVelocidade = 3f; // Aumento de velocidade espec�fico para os patins
    private movPlayer player; // Refer�ncia ao jogador

    void Start()
    {
        // Encontra o Player na cena
        player = FindObjectOfType<movPlayer>();
    }

    public void Coletar()
    {
        // Aplica o boost de velocidade no Player
        player.AplicarBoostVelocidade(aumentoVelocidade);

        // Remove o item da cena
        Destroy(gameObject); // Destroi o pr�prio item
    }
}









