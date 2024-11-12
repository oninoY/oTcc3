using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void Vida(float vida)
    {
        slider.value = vida;
    }

    public void VidaMaxima(float vida)
    {
        slider.maxValue = vida;
        slider.value = vida;
    }
}

