using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DialogoNpc : MonoBehaviour
{
    public Sprite personaFoto;
    public string[] conversaTexto;
    public string autorTexto;

    public LayerMask playerLayer;
    public float radiousFloat;
    public Vector2 radiousVector2;

    private DialogueGeralControl dc;
    bool onRadious;
    private Vector3 originalPosition;


    // Aqui eu t� chamando o DialogueControl para j� seguir as configs de l� por padr�o
    private void Start()
    {
        dc = FindObjectOfType<DialogueGeralControl>();
        originalPosition = transform.position;
    }

    //S� pra evitar de dar caca por conta do physics2d o m�todo vai ser chamado por aqui.
    private void FixedUpdate()
    {
        interacaoNpc();
    }

    private void Update()
    {
        //Condi��o para o tx
        if(Input.GetKeyDown(KeyCode.E) && onRadious)
        {
            dc.Conversa(personaFoto, conversaTexto, autorTexto);

            
            radiousVector2 = new Vector2(100f, 500f);
            transform.position = new Vector3(radiousVector2.x, radiousVector2.y, transform.position.z);

        }
    }

    public void ResetRadious()
    {
        transform.position = originalPosition;
    }

    public void interacaoNpc()
    {
        // Isso daqui � pra quando o player chegar perto ele ter a op��o de interagir.
        // O primeiro � a posi��o da �rea de intera��o, nesse caso o npc, o segundo � o alcance da �rea e o terceiro � pra ele s� interagir com o player, se n�o o script seria executado s� de ele estar no alcance do ch�o por exemplo.
        Collider2D alcance = Physics2D.OverlapCircle(transform.position, radiousFloat, playerLayer);
       
        if (alcance != null )
        {
            onRadious = true;
        }
        else
        {
            onRadious = false;
        }
    }

    // Sem esse corno a gente n�o consegue ver o raio de alcance para executar o dialogo, ele s� aparece na programa��o e some em game
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, radiousFloat);
    }
}

//NORMAL