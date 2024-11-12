using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TelaMorte : MonoBehaviour
{
    public GameObject telaMorte;

    public void ShowDeathScreen()
    {
        telaMorte.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("PurgamightPrincipal");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
