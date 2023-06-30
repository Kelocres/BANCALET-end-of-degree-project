using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejaPartida : MonoBehaviour
{
    //Per a agilitzar les probes del joc
    public bool ferIniciarEscena = true;
    /*
     * ESTES VARIABLES JA ES MODIFIQUEN AMB EL ACT1 SEQUENCIA1 TIMELINE
    public Transform posDetectiu_Inici;
    public Transform posSereno_Inici;
    public GameObject Sereno;
    public Transform posCameraJugador_Inici;
    public GameObject CameraJugador;*/
    public string inici_stateDetectiu = "cinematica";
    public IniciaTimeline inici;

    //Per a poder jugar en un idioma que elegixca el jugador
    [HideInInspector]
    public static readonly string[] idiomesPossibles = {"valencià", "español", "english"};
    public string idiomaActual;

    //Si es vol canviar l'anim de Detectiu al escomençar la cinematica
    public bool canviarAnimPJ = true;

    public Animator animatorPantallaNegra;

    // Start is called before the first frame update
    void Awake()
    {
        if(inici == null && GetComponent<IniciaTimeline>() != null)
            inici = GetComponent<IniciaTimeline>();

        if (animatorPantallaNegra == null)
        {
            Animator anim = GameObject.FindGameObjectWithTag("UI_PantallaNegra").GetComponent<Animator>();
            if (anim != null)
                animatorPantallaNegra = anim;
            else
                Debug.Log("ManejaPartida Awake() Animator de la PantallaNegra no trobat!!!");
        }

        //Determinar idioma del joc (es farà segons la opció seleccionada en la pantalla d'inici)
        idiomaActual = idiomesPossibles[0];
        //Començar segons el guió
        if (ferIniciarEscena) IniciarEscena();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IniciarEscena()
    {
        //Posicionar jugador
        Debug.Log("ManejaPartida IniciarEscena()");

        GameObject detectiu = GameObject.FindGameObjectWithTag("Player");
        PJ_StateManager dStateManager = detectiu.GetComponent<PJ_StateManager>();
        /*
        detectiu.transform.position = posDetectiu_Inici.position;
        if (Sereno != null && posSereno_Inici != null)
            Sereno.transform.position = posSereno_Inici.position;
        if (CameraJugador != null && posCameraJugador_Inici != null)
            CameraJugador.transform.position = posCameraJugador_Inici.position;
        */
        //Debug.Log("ManejaPartida IniciarEscena() inici_stateDetectiu == " + inici_stateDetectiu);
        if (inici_stateDetectiu == "cinematica")
        {
            if(inici == null)
            {
                Debug.Log("ManejaPartida IniciarEscena() ES DEVIA FER CINEMÀTICA PERÒ NO HI HA INICIA TIMELINE");
            }
            //Debug.Log("ManejaPartida IniciarEscena() canviar a cinematica State");
            if (animatorPantallaNegra != null)
                animatorPantallaNegra.SetTrigger("instaOpac");

            dStateManager.CanviarState_Cinematica(canviarAnimPJ);
            //IniciaTimeline
        }
        else
        {
            //Inicialitzar el detectiu amb ExplorarState
            //DetectiuStateManager dStateManager = detectiu.GetComponent<DetectiuStateManager>();
            dStateManager.CanviarState_Explorar();
        }

        if (inici != null)
            inici.StartTimeline();

    }
}
