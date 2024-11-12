using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoVida : MonoBehaviour
{
    [SerializeField] private float vidaAtual;
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private List<Drop> drops;
    
    [System.Serializable] public class Drop
    {
        public GameObject dropPrefab;
        public float dropProbability;
    }


    void Start()
    {
        vidaAtual = vidaMaxima;
    }


        public void TomarDanoInimigo(float quantiaVida)
    {
        vidaAtual -= quantiaVida;

        if (vidaAtual <= 0)
        {
            gameObject.SetActive(false);
            SistemaDrop();
        }
    }


    private void SistemaDrop()
    {
        // Verifica se deve deixar cair algum item
        foreach (var drop in drops)
        {
            if (Random.value < drop.dropProbability && drop.dropPrefab != null)
            {
                Instantiate(drop.dropPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
