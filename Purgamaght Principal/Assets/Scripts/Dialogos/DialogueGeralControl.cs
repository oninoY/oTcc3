using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueGeralControl : MonoBehaviour
{
    [Header("Componentes")]
    // Componentes - Local, fundo, texto e o com quem voc� est� falando
    public GameObject dialogueObj;
    public Image fotoAutor;
    public Text conversaTexto;
    public Text nomeAutor;

    [Header("Configura��es")]
    // Configura��es do dialogo
    public float velocidadeTexto;
    // Esse daqui armazena v�rios texto, pra caso tenha um dialogo em v�rias partes
    private string[] sequencia;
    private int index;

    private Coroutine currentCoroutine;

    private DialogoNpc dialogoNpc;

    private void Start()
    {
        dialogoNpc = FindObjectOfType<DialogoNpc>();
    }

    public void Conversa(Sprite p, string[] txt, string nomeAut)
    {
        dialogueObj.SetActive(true);
        fotoAutor.sprite = p;
        sequencia = txt;
        nomeAutor.text = nomeAut;
        conversaTexto.text = ""; // Limpa o conte�do anterior
        index = 0; // Reinicia o �ndice


        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(EscreverSequencia());

    }

    IEnumerator EscreverSequencia()
    {
        // Exibe o texto gradualmente
        foreach (char letra in sequencia[index].ToCharArray())
        {
            conversaTexto.text += letra;
            yield return new WaitForSeconds(velocidadeTexto);
        }

        while (Input.GetKeyDown(KeyCode.E) == false)
        {
            yield return null;
        }

        // Avan�a para o pr�ximo texto
        if (index < sequencia.Length - 1)
        {
            index++;
            conversaTexto.text = "";
            currentCoroutine = StartCoroutine(EscreverSequencia());
        }
        else
        {
            // Se n�o houver mais textos na sequ�ncia, encerra o di�logo
            index = 0;
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
            dialogueObj.SetActive(false);
            dialogoNpc.ResetRadious();
        }
    }
}