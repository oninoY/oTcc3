using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInimigo : Bullet
{
    [Header("Bullet Properties")]
    [SerializeField] private float speed = 10f; // Velocidade da bala
    private int direction; // Direção da bala

    // Sobrescreve o método Awake da classe Bullet (chamado na inicialização)
    protected override void Awake()
    {
        base.Awake(); // Chama o método Awake da classe base Bullet
    }

    // Sobrescreve o método Start da classe Bullet (chamado no início)
    protected override void Start()
    {
        base.Start(); // Chama o método Start da classe base Bullet
        // Define a direção da bala com base na direção do inimigo
        direction = transform.localScale.x > 0 ? 1 : -1; // Assume que a escala x determina a direção
    }

    // Sobrescreve o método FixedUpdate da classe Bullet (chamado em cada frame de física)
    protected override void FixedUpdate()
    {
        base.FixedUpdate(); // Chama o método FixedUpdate da classe base Bullet
        Move(); // Chama o método para mover a bala
    }

    // Método para mover a bala
    private void Move()
    {
        // Move a bala na direção especificada
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
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