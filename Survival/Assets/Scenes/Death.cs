using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public void iniciarJuego()
    {
        SceneManager.LoadScene(1);
    }
    public void menuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
}
