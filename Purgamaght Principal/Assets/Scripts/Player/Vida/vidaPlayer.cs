using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class vidaPlayer : MonoBehaviour
{
    [SerializeField] private float vidaAtual;
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private BarraDeVida barraDeVida;

    void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarBarraDeVida();
    }

    public void TomarDano(float quantiaVida)
    {
        vidaAtual -= quantiaVida;
        AtualizarBarraDeVida();
    }

    public void AtualizarBarraDeVida()
    {
        barraDeVida.Vida(vidaAtual);
    }

    public void ApplyVida(float vidaNova)
    {
        vidaAtual += vidaNova;
        if (vidaAtual > vidaMaxima)
        {
            vidaAtual = vidaMaxima;
        }
        AtualizarBarraDeVida();
    }
}

