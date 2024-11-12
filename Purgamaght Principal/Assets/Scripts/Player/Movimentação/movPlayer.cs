using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class movPlayer : MonoBehaviour
{
    #region Variáveis
    // Movimentação
    private float horizontal;
    private bool puloDuploDisponivel = true;
    [SerializeField] private float velocidade = 8f;
    private float velocidadeAtual;
    [SerializeField] private float forcaPulo;
    [SerializeField] private bool viradoDireita;

    // Variáveis/Componentes
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Transform verificarPiso;
    [SerializeField] private LayerMask camadaPiso;
    [SerializeField] private TrailRenderer tr;

    // Dash
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float forcaDash = 24f;
    [SerializeField] private float tempoDash = 0.2f;
    [SerializeField] private float cooldownDash = 1f;

    // Animações
    [SerializeField] public Animator animator;

    // Menu Pause
    private bool isPaused;
    [Header("Paineis e Menu")]
    public GameObject pausePainel;
    public string Cena;

    #endregion

    private void Awake()
    {
        // Ensure the player GameObject is not destroyed on load
        DontDestroyOnLoad(gameObject);
    }


    #region Método Start
    void Start()
    {
        Time.timeScale = 1f;
        velocidadeAtual = velocidade;
    }
    #endregion

    #region Método Update
    private void Update()
    {
        if (!isPaused)
        {
            Flip();
            MetodoDash();
            VerificarPuloDuplo();
            AtualizarAnimacoes();
            Move();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen();
        }
    }
    #endregion

    #region Método Move
    private void Move()
    {
        // Sistema de Input para a movimentação
        horizontal = Input.GetAxisRaw("Horizontal");

        // Verifica se a tecla espaço foi digitada e se o player está no chão 
        if (Input.GetButtonDown("Jump") && VerificarPiso())
        {
            rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    #endregion

    #region Método FixedUpdate
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * velocidadeAtual, rb.velocity.y);
    }
    #endregion

    #region Método Flip
    private void Flip()
    {
        if (viradoDireita && horizontal < 0f || !viradoDireita && horizontal > 0f)
        {
            viradoDireita = !viradoDireita;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    #endregion

    #region Método VerificarPiso
    public bool VerificarPiso()
    {
        return Physics2D.OverlapCircle(verificarPiso.position, 0.2f, camadaPiso);
    }
    #endregion

    #region Método Dash
    private void MetodoDash()
    {
        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * forcaDash, 0f);
        tr.emitting = true;

        animator.SetBool("Dash", true); // Adiciona animação de dash
        yield return new WaitForSeconds(tempoDash);
        animator.SetBool("Dash", false); // Reseta animação após o dash

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(cooldownDash);
        canDash = true;
    }
    #endregion

    #region Método Pulo Duplo
    private void PuloDuplo()
    {
        rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
    }
    #endregion

    #region Método Verificar Pulo Duplo
    private void VerificarPuloDuplo()
    {
        if (VerificarPiso())
        {
            puloDuploDisponivel = true;
        }
        else if (puloDuploDisponivel && Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) > 0.1f)
        {
            PuloDuplo();
            puloDuploDisponivel = false;
        }
    }
    #endregion

    #region Método AplicarBoostVelocidade
    public void AplicarBoostVelocidade(float velocidadePatins)
    {
        // Aumenta a velocidade atual
        velocidade += velocidadePatins; // Use "velocidade" em vez de "velocidadeAtual"
    }

    #endregion


    #region M�todo AtualizarAnima��es

    private void AtualizarAnimacoes()
    {
        if (EstaPulando())
        {
            animator.SetBool("Pulando", true);
            animator.SetBool("Correndo", false);
            animator.SetBool("Caindo", false);
        }
        else if (EstaCaindo())
        {
            animator.SetBool("Pulando", false);
            animator.SetBool("Correndo", false);
            animator.SetBool("Caindo", true);
        }
        else if (EstaAndando())
        {
            animator.SetBool("Pulando", false);
            animator.SetBool("Correndo", true);
            animator.SetBool("Caindo", false);
        }
        else
        {
            animator.SetBool("Pulando", false);
            animator.SetBool("Correndo", false);
            animator.SetBool("Caindo", false);
        }
    }

    private bool EstaPulando()
    {
        return rb.velocity.y > 0 && !VerificarPiso();
    }

    private bool EstaCaindo()
    {
        return rb.velocity.y < 0 && !VerificarPiso() && !EstaPulando();
    }

    private bool EstaAndando()
    {
        float direcaoHorizontal = Input.GetAxisRaw("Horizontal");
        return direcaoHorizontal != 0 && VerificarPiso();
    }

    #endregion

    #region M�todo Bot�o menu principal
    public void MenuPrincipalBotao()
    {
        SceneManager.LoadSceneAsync(0);
    }
    #endregion

    #region M�todo Pause
    public void PauseScreen()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            pausePainel.SetActive(false);
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0f;
            pausePainel.SetActive(true);
        }


    }
    #endregion

    #region Volta pro menu
    public void BackMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
    #endregion

        private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("antichato") || collision.gameObject.CompareTag("saida"))
        {
            
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

}

