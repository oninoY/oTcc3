using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [Header("Enemy Properties")]
    // Define a velocidade de movimento do inimigo
    public float speed;

    // Define a distância a partir da qual o inimigo pode atacar
    public float attackDistance;

    // Direção em que o inimigo se move: 1 para a direita, -1 para a esquerda
    public int direction;

    [Header("RayCast Properties")]
    // Camada que define o que é considerado "chão" para o inimigo
    public LayerMask layerGround;

    // Distância do Raycast que detecta o chão
    public float lengthGround;

    // Distância do Raycast que detecta paredes
    public float lengthWall;

    // Ponto de origem do Raycast para detectar o chão
    public Transform rayPointGround;

    // Ponto de origem do Raycast para detectar paredes
    public Transform rayPointWall;

    // Resultado do Raycast que detecta o chão
    public RaycastHit2D hitGround;

    // Resultado do Raycast que detecta uma parede
    public RaycastHit2D hitWall;

    // Referência ao componente Animator do inimigo (para animações)
    protected Animator animator;

    // Referência ao componente Rigidbody2D do inimigo (para física)
    protected Rigidbody2D rb;

    // Referência ao jogador
    protected Transform player;

    // Distância entre o inimigo e o jogador
    protected float playerDistance;

    // Inicializa os componentes essenciais do inimigo
    protected virtual void Awake()
    {
        // Obtém o componente Animator do inimigo
        animator = GetComponent<Animator>();

        // Obtém o componente Rigidbody2D para manipular física
        rb = GetComponent<Rigidbody2D>();

        // Encontra o jogador no cenário e obtém sua posição
        player = FindObjectOfType<movPlayer>().transform;
    }

    // Método Start, chamado no início (não está sendo usado no momento)
    void Start()
    {
    }

    // Atualiza a cada frame para calcular a distância até o jogador
    protected virtual void Update()
    {
        GetDistancePlayer();
    }

    // Inverte a direção do inimigo ao atingir uma parede ou borda
    protected virtual void Flip()
    {
        // Multiplica a direção por -1, invertendo-a
        direction *= -1;

        // Inverte a escala do inimigo, mantendo a escala positiva
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y);
    }

    // Método que verifica se o inimigo está sobre o chão
    protected virtual RaycastHit2D RaycastGround()
    {
        // Lança um Raycast para baixo a partir do ponto rayPointGround
        hitGround = Physics2D.Raycast(rayPointGround.position, Vector2.down, lengthGround, layerGround);

        // Define a cor do Raycast para debug (vermelho se atingir o chão, verde se não)
        Color color = hitGround ? Color.red : Color.green;

        // Desenha a linha do Raycast para visualização no editor
        Debug.DrawRay(rayPointGround.position, Vector2.down * lengthGround, color);

        // Retorna o resultado do Raycast (true se atingir o chão)
        return hitGround;
    }

    // Método que verifica se o inimigo está de frente para uma parede
    protected virtual RaycastHit2D RaycastWall()
    {
        // Lança um Raycast para a direção do movimento a partir de rayPointWall
        hitWall = Physics2D.Raycast(rayPointWall.position, Vector2.right, lengthWall, layerGround);

        // Define a cor do Raycast para debug (amarelo se atingir a parede, azul se não)
        Color color = hitWall ? Color.yellow : Color.blue;

        // Desenha a linha do Raycast para visualização no editor
        Debug.DrawRay(rayPointWall.position, Vector2.right * direction * lengthWall, color);

        // Retorna o resultado do Raycast (true se atingir uma parede)
        return hitWall;
    }

    // Calcula a distância entre o inimigo e o jogador
    protected void GetDistancePlayer()
    {
        // Subtrai a posição do inimigo da posição do jogador para obter a distância horizontal
        playerDistance = player.position.x - transform.position.x;
    }
}