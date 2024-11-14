using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animporta : MonoBehaviour
{
public Animator animator;
public GameObject portafechou;
public GameObject essamesmo;

private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            animator.SetTrigger("fechando");            
            essamesmo.SetActive(false);
            portafechou.SetActive(true);
        }
    }
}
