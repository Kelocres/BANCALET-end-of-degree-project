using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// REF: https://www.youtube.com/watch?v=OmobsXZSRKo
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;

    //Per a visualitzar millor la barra de la pantalla
    private float target;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // async: El codi s'executarà durant varios frames (similar a una corutina), pero
    // evita que es bloqueje el thread principal (ideal per a tasques pesades)
    // MÉS INFO: https://gamedevbeginner.com/async-in-unity/
    public async void LoadScene(string sceneName)
    {
        Debug.Log("LevelManager LoadScene()");
        _progressBar.fillAmount = 0f;
        target = 0f;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        //Per a evitar que la escena s'active de manera automàtica
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(100); //No funciona en este projecte, ni és necessari excepte en el tutorial

            //_progressBar.fillAmount = scene.progress;
            target = scene.progress;

        } while (scene.progress < 0.9f); //Unity carrega l'escena al 90%

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
    }

    private void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, target, 3 * Time.deltaTime);
    }
}
