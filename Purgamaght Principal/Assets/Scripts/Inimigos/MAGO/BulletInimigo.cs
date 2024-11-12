using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInimigo : Bullet
{
    // Sobrescreve o método Awake da classe Bullet (chamado na inicialização)
    protected override void Awake()
    {
        base.Awake(); // Chama o método Awake da classe base Bullet
    }

    // Sobrescreve o método Start da classe Bullet (chamado no início)
    protected override void Start()
    {
        base.Start(); // Chama o método Start da classe base Bullet
    }

    // Sobrescreve o método FixedUpdate da classe Bullet (chamado em cada frame de física)
    protected override void FixedUpdate()
    {
        base.FixedUpdate(); // Chama o método FixedUpdate da classe base Bullet
    }

    // Detecta colisões com outros objetos que possuem Collider2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se a bala colidiu com o jogador
        if (collision.CompareTag("Player"))
        {
            Explode(); // Chama o método Explode para destruir a bala
        }

        // Verifica se a bala colidiu com o chão (ou piso)
        if (collision.CompareTag("Piso"))
        {
            Explode(); // Chama o método Explode para destruir a bala
        }
    }
}

