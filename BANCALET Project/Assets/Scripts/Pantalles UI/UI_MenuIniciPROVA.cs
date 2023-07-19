using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuIniciPROVA : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mostrarElements;
    public void IniciarPartida(string sceneName)
    {
        if (mostrarElements != null)
            mostrarElements.SetActive(true);
        //LevelManager.Instance.LoadScene(sceneName);
        SceneManager.LoadScene("EscenaBancalet");
    }

    // Update is called once per frame
    public void Eixir()
    {
        Application.Quit();
    }
}
