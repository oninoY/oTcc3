using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Define a direção que a bala deve seguir
    public Vector2 direction;

    // Velocidade da bala
    public float speed;

    // Referência ao objeto de explosão que será ativado quando a bala explodir
    public GameObject explosion;

    // Controlador de animação (será usado para a animação da explosão)
    protected Animator animator;

    // Tempo de vida da bala antes de ser destruída automaticamente
    protected float livingTime = 3f;

    // Renderer do sprite da bala, usado para manipular a visibilidade
    protected SpriteRenderer _renderer;

    // Componente de física da bala, responsável pela movimentação
    protected Rigidbody2D rb;

    // Método chamado quando o objeto é instanciado
    protected virtual void Awake()
    {
        // Obtém o componente Rigidbody2D do objeto
        rb = GetComponent<Rigidbody2D>();

        // Obtém o componente Animator da explosão para ativar a animação
        animator = explosion.GetComponent<Animator>();

        // Obtém o componente SpriteRenderer para controlar a visibilidade da bala
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Método chamado no início da execução do jogo (após Awake)
    protected virtual void Start()
    {
        // Destroi o objeto da bala após 'livingTime' segundos
        Destroy(gameObject, livingTime);
    }

    // Método chamado a cada frame de física (usado para controlar o movimento)
    protected virtual void FixedUpdate()
    {
        // Chama o método Movement para mover a bala
        Movement();
    }

    // Define o movimento da bala baseado na direção e velocidade
    private void Movement()
    {
        // Calcula o movimento normalizado da bala (direção * velocidade)
        Vector2 movement = direction.normalized * speed;

        // Aplica o movimento ao Rigidbody2D, movendo a bala
        rb.velocity = movement;
    }

    // Método para simular a explosão da bala
    public void Explode()
    {
        // Define a velocidade como 0 para parar o movimento
        speed = 0f;

        // Desabilita a visibilidade do sprite da bala
        _renderer.enabled = false;

        // Desabilita o colisor da bala para evitar interações físicas
        GetComponent<BoxCollider2D>().enabled = false;

        // Ativa a explosão, caso o objeto de explosão esteja configurado
        if (explosion != null)
        {
            explosion.SetActive(true);
        }

        // Destroi o objeto da bala após 1.5 segundos da explosão
        Destroy(gameObject, 1.5f);
    }
}

