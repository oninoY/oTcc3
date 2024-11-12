using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInimigo : Bullet
{
    // Sobrescreve o m�todo Awake da classe Bullet (chamado na inicializa��o)
    protected override void Awake()
    {
        base.Awake(); // Chama o m�todo Awake da classe base Bullet
    }

    // Sobrescreve o m�todo Start da classe Bullet (chamado no in�cio)
    protected override void Start()
    {
        base.Start(); // Chama o m�todo Start da classe base Bullet
    }

    // Sobrescreve o m�todo FixedUpdate da classe Bullet (chamado em cada frame de f�sica)
    protected override void FixedUpdate()
    {
        base.FixedUpdate(); // Chama o m�todo FixedUpdate da classe base Bullet
    }

    // Detecta colis�es com outros objetos que possuem Collider2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se a bala colidiu com o jogador
        if (collision.CompareTag("Player"))
        {
            Explode(); // Chama o m�todo Explode para destruir a bala
        }

        // Verifica se a bala colidiu com o ch�o (ou piso)
        if (collision.CompareTag("Piso"))
        {
            Explode(); // Chama o m�todo Explode para destruir a bala
        }
    }
}

