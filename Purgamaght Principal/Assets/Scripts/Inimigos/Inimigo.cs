using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [Header("Enemy Properties")]
    // Define a velocidade de movimento do inimigo
    public float speed;

    // Define a dist�ncia a partir da qual o inimigo pode atacar
    public float attackDistance;

    // Dire��o em que o inimigo se move: 1 para a direita, -1 para a esquerda
    public int direction;

    [Header("RayCast Properties")]
    // Camada que define o que � considerado "ch�o" para o inimigo
    public LayerMask layerGround;

    // Dist�ncia do Raycast que detecta o ch�o
    public float lengthGround;

    // Dist�ncia do Raycast que detecta paredes
    public float lengthWall;

    // Ponto de origem do Raycast para detectar o ch�o
    public Transform rayPointGround;

    // Ponto de origem do Raycast para detectar paredes
    public Transform rayPointWall;

    // Resultado do Raycast que detecta o ch�o
    public RaycastHit2D hitGround;

    // Resultado do Raycast que detecta uma parede
    public RaycastHit2D hitWall;

    // Refer�ncia ao componente Animator do inimigo (para anima��es)
    protected Animator animator;

    // Refer�ncia ao componente Rigidbody2D do inimigo (para f�sica)
    protected Rigidbody2D rb;

    // Refer�ncia ao jogador
    protected Transform player;

    // Dist�ncia entre o inimigo e o jogador
    protected float playerDistance;

    // Inicializa os componentes essenciais do inimigo
    protected virtual void Awake()
    {
        // Obt�m o componente Animator do inimigo
        animator = GetComponent<Animator>();

        // Obt�m o componente Rigidbody2D para manipular f�sica
        rb = GetComponent<Rigidbody2D>();

        // Encontra o jogador no cen�rio e obt�m sua posi��o
        player = FindObjectOfType<movPlayer>().transform;
    }

    // M�todo Start, chamado no in�cio (n�o est� sendo usado no momento)
    void Start()
    {
    }

    // Atualiza a cada frame para calcular a dist�ncia at� o jogador
    protected virtual void Update()
    {
        GetDistancePlayer();
    }

    // Inverte a dire��o do inimigo ao atingir uma parede ou borda
    protected virtual void Flip()
    {
        // Multiplica a dire��o por -1, invertendo-a
        direction *= -1;

        // Inverte a escala do inimigo, mantendo a escala positiva
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y);
    }

    // M�todo que verifica se o inimigo est� sobre o ch�o
    protected virtual RaycastHit2D RaycastGround()
    {
        // Lan�a um Raycast para baixo a partir do ponto rayPointGround
        hitGround = Physics2D.Raycast(rayPointGround.position, Vector2.down, lengthGround, layerGround);

        // Define a cor do Raycast para debug (vermelho se atingir o ch�o, verde se n�o)
        Color color = hitGround ? Color.red : Color.green;

        // Desenha a linha do Raycast para visualiza��o no editor
        Debug.DrawRay(rayPointGround.position, Vector2.down * lengthGround, color);

        // Retorna o resultado do Raycast (true se atingir o ch�o)
        return hitGround;
    }

    // M�todo que verifica se o inimigo est� de frente para uma parede
    protected virtual RaycastHit2D RaycastWall()
    {
        // Lan�a um Raycast para a dire��o do movimento a partir de rayPointWall
        hitWall = Physics2D.Raycast(rayPointWall.position, Vector2.right, lengthWall, layerGround);

        // Define a cor do Raycast para debug (amarelo se atingir a parede, azul se n�o)
        Color color = hitWall ? Color.yellow : Color.blue;

        // Desenha a linha do Raycast para visualiza��o no editor
        Debug.DrawRay(rayPointWall.position, Vector2.right * direction * lengthWall, color);

        // Retorna o resultado do Raycast (true se atingir uma parede)
        return hitWall;
    }

    // Calcula a dist�ncia entre o inimigo e o jogador
    protected void GetDistancePlayer()
    {
        // Subtrai a posi��o do inimigo da posi��o do jogador para obter a dist�ncia horizontal
        playerDistance = player.position.x - transform.position.x;
    }
}