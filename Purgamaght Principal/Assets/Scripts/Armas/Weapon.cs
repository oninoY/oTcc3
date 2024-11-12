using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Referência ao objeto que está atirando (por exemplo, um jogador ou inimigo)
    public GameObject shooter;

    // Prefab do projétil (bala) que será instanciado ao atirar
    public GameObject bulletPrefab;

    // Ponto de origem de onde as balas serão disparadas
    public Transform firePoint;

    // Método responsável por disparar o projétil
    public void Shoot()
    {
        // Verifica se os objetos bulletPrefab, firePoint e shooter não são nulos (estão definidos)
        if (bulletPrefab != null && firePoint != null && shooter != null)
        {
            // Instancia uma nova bala no ponto de disparo (firePoint), sem rotação (Quaternion.identity)
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity) as GameObject;

            // Obtém o componente Bullet do projétil recém-criado
            Bullet bulletComponent = bullet.GetComponent<Bullet>();

            // Define a direção do projétil com base na escala do atirador (se está virado para a esquerda ou direita)
            if (shooter.transform.localScale.x < 0)
            {
                // Se a escala no eixo X do atirador for negativa, atira para a esquerda
                bulletComponent.direction = Vector2.left;
            }
            else
            {
                // Caso contrário, atira para a direita
                bulletComponent.direction = Vector2.right;
            }
        }
    }
}

