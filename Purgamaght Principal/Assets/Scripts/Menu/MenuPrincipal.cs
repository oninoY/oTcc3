using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
   public void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }



    public void Quit()
    {
        Application.Quit();
    }
}