using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Refer�ncia ao objeto que est� atirando (por exemplo, um jogador ou inimigo)
    public GameObject shooter;

    // Prefab do proj�til (bala) que ser� instanciado ao atirar
    public GameObject bulletPrefab;

    // Ponto de origem de onde as balas ser�o disparadas
    public Transform firePoint;

    // M�todo respons�vel por disparar o proj�til
    public void Shoot()
    {
        // Verifica se os objetos bulletPrefab, firePoint e shooter n�o s�o nulos (est�o definidos)
        if (bulletPrefab != null && firePoint != null && shooter != null)
        {
            // Instancia uma nova bala no ponto de disparo (firePoint), sem rota��o (Quaternion.identity)
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity) as GameObject;

            // Obt�m o componente Bullet do proj�til rec�m-criado
            Bullet bulletComponent = bullet.GetComponent<Bullet>();

            // Define a dire��o do proj�til com base na escala do atirador (se est� virado para a esquerda ou direita)
            if (shooter.transform.localScale.x < 0)
            {
                // Se a escala no eixo X do atirador for negativa, atira para a esquerda
                bulletComponent.direction = Vector2.left;
            }
            else
            {
                // Caso contr�rio, atira para a direita
                bulletComponent.direction = Vector2.right;
            }
        }
    }
}

