using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoreExplorar_Zona : MonoBehaviour
{
    public Transform posCameraZona;
    private Controla_Camara controlaCamara;

    //Si existix un SeguixJugador en l'escena, els canvis de posCamera i objectiu
    //s'enviaran a ell
    // Si no existix, els canvis es faran en Controla_Camara
    public SeguixJugador seguixJugador;

    private bool realitzable;

    //Per a regristrar les zones menudes dins d'una zona gran
    public List<VoreExplorar_Zona> zonesMenudes;
    // Start is called before the first frame update
    public VoreExplorar_Zona zonaGran;
    public bool esZonaGran = false;
    void Start()
    {
        realitzable = true;
        zonesMenudes = new List<VoreExplorar_Zona>();
        Controla_Camara[] ccs = FindObjectsOfType<Controla_Camara>();
        if (ccs.Length == 0)
            Debug.Log("VoreExplorar_Zona Start() No hi ha Controla_Camara!!");
        else if (ccs.Length > 1)
            Debug.Log("VoreExplorar_Zona Start() Hi han " + ccs.Length + " Controla_Camara, el programa no funcionarà");
        else
            controlaCamara = ccs[0];

        if (seguixJugador == null)
        {
            SeguixJugador[] sjs = FindObjectsOfType<SeguixJugador>();
            if (sjs.Length == 0)
                Debug.Log("VoreExplorar_Zona Start() No hi ha SeguixJugador!!");
            else if (sjs.Length > 1)
                Debug.Log("VoreExplorar_Zona Start() Hi han " + ccs.Length + " SeguixJugador, el programa no funcionarà");
            else
                seguixJugador = sjs[0];
        }

        if (posCameraZona == null)
            Debug.Log("VoreExplorar_Zona Start() No hi ha posCameraZona!!");

        if (seguixJugador == null && controlaCamara == null)
            realitzable = false;
        else
        {
            //Al ser realitzable, es busquen i configuren els VoreExplorar_Trigger
            InitializeTriggers();
        }
    }

    private void InitializeTriggers()
    {
        VoreExplorar_Trigger[] triggers = GetComponentsInChildren<VoreExplorar_Trigger>();
        if (triggers == null || triggers.Length != 2)
        {
            Debug.Log("VoreExplorar_Zona Start() número de triggers = " + triggers.Length);
        }

        //Per a verificar si s'han inicialitzat els dos triggers com deurien
        bool triggerEntradaFet = false;
        bool triggerEixidaFet = false;
        foreach (VoreExplorar_Trigger trigger in triggers)
        {
            if(trigger.tipusTrigger == VoreExplorar_Trigger.VE_TipusTrigger.Entrada)
            {
                trigger.dtJugador += JugadorEntra;
                triggerEntradaFet = true;
            }
            if (trigger.tipusTrigger == VoreExplorar_Trigger.VE_TipusTrigger.Eixida)
            {
                trigger.dtJugador += JugadorSurt;
                if(esZonaGran)
                {
                    //NOTA: El trigger d'eixida de la zona gran necessitarà un rigidbody
                    //      i la zona menuda un Trigger en el objecte pare
                    trigger.buscaZonesMenudes = true;
                    trigger.dtZonaMenuda += ZonaMenudaTrobada;
                }
                triggerEixidaFet = true;
            }
        }

        //Comprobar que s'ha realitzat adequadament
        if (!triggerEntradaFet) Debug.Log("VoreExplorar_Zona InitializeTriggers() trigger d'entrada no inicialitzat!!");
        if (!triggerEixidaFet) Debug.Log("VoreExplorar_Zona InitializeTriggers() trigger d'eixida no inicialitzat!!");
    }

    // Update is called once per frame
    /* void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("VoreExplorar_Zona OnTriggerEnter() Has entrat en la zona");
            if (realitzable)
            {
                if (posCameraZona != null && controlaCamara != null)
                {
                    if (seguixJugador != null)
                    {
                        Debug.Log("VoreExplorar_Zona OnTriggerEnter() seguixJugador !=null, es crida a seguixJugador.NouPosCameraExplorar(posCameraZona)");
                        seguixJugador.NouPosCameraExplorar(posCameraZona);
                    }
                    else
                    {
                        Debug.Log("VoreExplorar_Zona OnTriggerEnter() seguixJugador ==null, es crida a controlaCamara.NouPosCamera(posCameraZona)");
                        controlaCamara.NouPosCamera(posCameraZona);
                    }
                }
                else
                    Debug.Log("VoreExplorar_Zona OnTriggerEnter() posCameraZona == null o controlaCamara== null!!");
            }
            else
                Debug.Log("VoreExplorar_Zona OnTriggerEnter() No hi ha Controla_Camara ni SeguixJugador, no es realitza canvi!!!");
        }
        //Buscar les zones menudes dins (si es una zona gran)
        else if(esZonaGran && other.GetComponent<VoreExplorar_Zona>())
        //else if (other.GetComponent<VoreExplorar_Zona>())
        {
            //En cas de que no s'haja inicialitzat correctament
            if (zonesMenudes == null) zonesMenudes = new List<VoreExplorar_Zona>();

            VoreExplorar_Zona menuda = other.GetComponent<VoreExplorar_Zona>();

            zonesMenudes.Add(menuda);
            menuda.zonaGran = this;
        }
    }*/

    public void JugadorEntra()
    {
        Debug.Log("VoreExplorar_Zona OnTriggerEnter() Has entrat en la zona");
        if (realitzable)
        {
            if (posCameraZona != null && controlaCamara != null)
            {
                if (seguixJugador != null)
                {
                    Debug.Log("VoreExplorar_Zona OnTriggerEnter() seguixJugador !=null, es crida a seguixJugador.NouPosCameraExplorar(posCameraZona)");
                    seguixJugador.NouPosCameraExplorar(posCameraZona);
                }
                else
                {
                    Debug.Log("VoreExplorar_Zona OnTriggerEnter() seguixJugador ==null, es crida a controlaCamara.NouPosCamera(posCameraZona)");
                    controlaCamara.NouPosCamera(posCameraZona);
                }
            }
            else
                Debug.Log("VoreExplorar_Zona OnTriggerEnter() posCameraZona == null o controlaCamara== null!!");
        }
        else
            Debug.Log("VoreExplorar_Zona OnTriggerEnter() No hi ha Controla_Camara ni SeguixJugador, no es realitza canvi!!!");

    }


    /*private void OnTriggerExit(Collider other)
    {
        //Si el jugador ix de la zona i no ha entrat ja en una altra zona menuda,
        //la càmara es situa segons la zona gran
        
        if (other.tag == "Player")
        {
            if (esZonaGran || zonaGran == null)
            {
                Debug.Log("VoreExplorar_Zona OnTriggerExit() esZonaGran || zonaGran == null");
                return;
            }


            if(controlaCamara == null || posCameraZona == null)
            {
                Debug.Log("VoreExplorar_Zona OnTriggerExit() variables essencials no definides!!");
                return;
            }

            if(controlaCamara.posCamara == posCameraZona)
            {
                Debug.Log("VoreExplorar_Zona OnTriggerExit() canviar a posCamera de la zona Gran!!");
                if (seguixJugador != null)
                {
                    Debug.Log("VoreExplorar_Zona OnTriggerExit() seguixJugador !=null, es crida a seguixJugador.NouPosCameraExplorar(zonaGran.posCameraZona)");
                    seguixJugador.NouPosCameraExplorar(zonaGran.posCameraZona);
                }
                else
                {
                    Debug.Log("VoreExplorar_Zona OnTriggerExit() seguixJugador ==null, es crida a controlaCamara.NouPosCamera(zonaGran.posCameraZona)");
                    controlaCamara.NouPosCamera(zonaGran.posCameraZona);
                }
            }
        }

        
    }*/

    public void JugadorSurt()
    {
        if (esZonaGran || zonaGran == null)
        {
            Debug.Log("VoreExplorar_Zona OnTriggerExit() esZonaGran || zonaGran == null");
            return;
        }


        if (controlaCamara == null || posCameraZona == null)
        {
            Debug.Log("VoreExplorar_Zona OnTriggerExit() variables essencials no definides!!");
            return;
        }

        if (controlaCamara.posCamara == posCameraZona)
        {
            Debug.Log("VoreExplorar_Zona OnTriggerExit() canviar a posCamera de la zona Gran!!");
            if (seguixJugador != null)
            {
                Debug.Log("VoreExplorar_Zona OnTriggerExit() seguixJugador !=null, es crida a seguixJugador.NouPosCameraExplorar(zonaGran.posCameraZona)");
                seguixJugador.NouPosCameraExplorar(zonaGran.posCameraZona);
            }
            else
            {
                Debug.Log("VoreExplorar_Zona OnTriggerExit() seguixJugador ==null, es crida a controlaCamara.NouPosCamera(zonaGran.posCameraZona)");
                controlaCamara.NouPosCamera(zonaGran.posCameraZona);
            }
        }
    }

    public void ZonaMenudaTrobada(VoreExplorar_Zona zona)
    {

        //En cas de que no s'haja inicialitzat correctament
        if (zonesMenudes == null) zonesMenudes = new List<VoreExplorar_Zona>();

        
        zonesMenudes.Add(zona);
        zona.zonaGran = this;
    }
}
