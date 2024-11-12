using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Define a dire��o que a bala deve seguir
    public Vector2 direction;

    // Velocidade da bala
    public float speed;

    // Refer�ncia ao objeto de explos�o que ser� ativado quando a bala explodir
    public GameObject explosion;

    // Controlador de anima��o (ser� usado para a anima��o da explos�o)
    protected Animator animator;

    // Tempo de vida da bala antes de ser destru�da automaticamente
    protected float livingTime = 3f;

    // Renderer do sprite da bala, usado para manipular a visibilidade
    protected SpriteRenderer _renderer;

    // Componente de f�sica da bala, respons�vel pela movimenta��o
    protected Rigidbody2D rb;

    // M�todo chamado quando o objeto � instanciado
    protected virtual void Awake()
    {
        // Obt�m o componente Rigidbody2D do objeto
        rb = GetComponent<Rigidbody2D>();

        // Obt�m o componente Animator da explos�o para ativar a anima��o
        animator = explosion.GetComponent<Animator>();

        // Obt�m o componente SpriteRenderer para controlar a visibilidade da bala
        _renderer = GetComponent<SpriteRenderer>();
    }

    // M�todo chamado no in�cio da execu��o do jogo (ap�s Awake)
    protected virtual void Start()
    {
        // Destroi o objeto da bala ap�s 'livingTime' segundos
        Destroy(gameObject, livingTime);
    }

    // M�todo chamado a cada frame de f�sica (usado para controlar o movimento)
    protected virtual void FixedUpdate()
    {
        // Chama o m�todo Movement para mover a bala
        Movement();
    }

    // Define o movimento da bala baseado na dire��o e velocidade
    private void Movement()
    {
        // Calcula o movimento normalizado da bala (dire��o * velocidade)
        Vector2 movement = direction.normalized * speed;

        // Aplica o movimento ao Rigidbody2D, movendo a bala
        rb.velocity = movement;
    }

    // M�todo para simular a explos�o da bala
    public void Explode()
    {
        // Define a velocidade como 0 para parar o movimento
        speed = 0f;

        // Desabilita a visibilidade do sprite da bala
        _renderer.enabled = false;

        // Desabilita o colisor da bala para evitar intera��es f�sicas
        GetComponent<BoxCollider2D>().enabled = false;

        // Ativa a explos�o, caso o objeto de explos�o esteja configurado
        if (explosion != null)
        {
            explosion.SetActive(true);
        }

        // Destroi o objeto da bala ap�s 1.5 segundos da explos�o
        Destroy(gameObject, 1.5f);
    }
}

