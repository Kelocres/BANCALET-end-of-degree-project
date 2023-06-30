using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejaCamares : MonoBehaviour
{
    // Start is called before the first frame update

    public string camInicial;
    public NombraCamera camActual;

    //private FaceCameraPersonajes [] fcPersonatges;
    //private FaceCameraElementos []  fcElements;
    private List<FaceCameraPersonajes> fcPersonatges;
    private List<FaceCameraElementos> fcElements;
    private PJ_StateManager dsm;

    public NombraCamera [] nomsCameresEscenari;
    public NombraCamera nomCameraJugador;

    public bool started = false;

    void Start()
    {
        if (!started) StartManejaCamares();
    }

    public void StartManejaCamares()
    {
        started = true;

        //nomCameraJugador = new NombraCamera("CameraJugador", Camera.main);
        nomCameraJugador = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NombraCamera>();
        nomsCameresEscenari = FindObjectsOfType<NombraCamera>();
        camActual = null;

        //Omplir fcPersonatges i fcElements
        fcPersonatges = new List<FaceCameraPersonajes>();
        FaceCameraPersonajes[] _fcPersonatges = FindObjectsOfType<FaceCameraPersonajes>();
        foreach (FaceCameraPersonajes personatge in _fcPersonatges)
            GetFCPersonajes(personatge);

        fcElements = new List<FaceCameraElementos>();
        FaceCameraElementos[] _fcElements = FindObjectsOfType<FaceCameraElementos>();
        foreach (FaceCameraElementos element in _fcElements)
            GetFCElementos(element);
        dsm = FindObjectOfType<PJ_StateManager>();

        if (camInicial == null || camInicial == "")
            CanviarCamara("CameraJugador");
        else
            CanviarCamara(camInicial);

    }

    public void GetFCElementos(FaceCameraElementos element)
    {
        if (element.started) return;

        if (!fcElements.Contains(element))
            fcElements.Add(element);

        element.started = true;
    }

    public void GetFCPersonajes(FaceCameraPersonajes personatge)
    {
        if (personatge.started) return;

        if (!fcPersonatges.Contains(personatge))
            fcPersonatges.Add(personatge);

        personatge.started = true;
    }

    public void DestroyFCElementos(FaceCameraElementos element)
    {
        if (element != null) fcElements.Remove(element);
    }

    public void DestroyFCPersonajes(FaceCameraPersonajes personatge)
    {
        if (personatge != null) fcPersonatges.Remove(personatge);
    }


    //En cas d activar ho en un timeline, es fa amb un Signal Track
    //REFERÈNCIA PER A TREBALLAR AMB SIGNAL TRACK:
    //https://www.youtube.com/watch?v=PlfSWUiBCoo&t=184s
    public void CanviarCamara(string nomCam)
    {
        if (!started) StartManejaCamares();
        Debug.Log("ManejaCamares CanviarCamara("+nomCam+")");
        //En cas de que cam i camActual siga la mateixa, s han de desactivar i activar, en aquest ordre
        if (nomCam==null)
        {
            Debug.Log("Cam is null");
            return;
        }
        
            bool decidit = false;
            foreach(NombraCamera nc in nomsCameresEscenari)
            {
            if(nc == null) Debug.Log("ManejaCamares CanviarCamara() nc == null");
            else if (nc.nom == null) Debug.Log("ManejaCamares CanviarCamara() nc.nom == null");
            else if (nomCam == null) Debug.Log("ManejaCamares CanviarCamara() nomCam == null");
            if (nc.nom == nomCam)
                {
                    camActual = nc;
                    nc.camera.enabled = true;
                Debug.Log("ManejaCamares CanviarCamara() camActual (" + camActual.nom + ") activada i nc("+nc.nom+").camera.enabled = true");
                    decidit = true;
                    Debug.Log("ManejaCamares CanviarCamara() Camera " + nc.nom + " activada");
                    //break;
                }
                else
                {
                    
                    nc.camera.enabled = false;
                    Debug.Log("ManejaCamares CanviarCamara() Camera " + nc.nom + " desactivada");
                }
            }
            if(!decidit)
            {
                Debug.Log("ERROR: Nom no corresponent a cap camera");
                return;
            }
        //}

        if (camActual.camera == null) Debug.Log("NO HAY CAMARA EN CamActual");
        if (camActual.camera != null) Debug.Log("PUES SÍ QUE HAY CAMARA EN CamActual");
        // Ix un missatge d'error (NullReferenceException), però no pareix que faça res

        camActual.camera.enabled = true;
        camActual.camera.GetComponent<AudioListener>().enabled = true;

        //Debug.Log("Camera actual assignada");

        dsm.CanviarCamara(camActual.camera);

        foreach(FaceCameraPersonajes pers in fcPersonatges)
            pers.CanviarCamara(camActual.camera);

        foreach(FaceCameraElementos elem in fcElements)
            elem.CanviarCamara(camActual.camera);
    }
}


