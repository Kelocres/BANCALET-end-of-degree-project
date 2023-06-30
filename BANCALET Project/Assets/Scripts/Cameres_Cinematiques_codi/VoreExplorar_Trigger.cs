using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoreExplorar_Trigger : MonoBehaviour
{
    //Delegate per anunciar que el jugador ha entrat o eixit del trigger
    public delegate void delTriggerJugador();
    public event delTriggerJugador dtJugador;

    //Delegate per anunciar que el jugador ha entrat o eixit del trigger
    public delegate void delTriggerZonaMenuda(VoreExplorar_Zona zona);
    public event delTriggerZonaMenuda dtZonaMenuda;

    //Bool per afirmar que es busquen zones menudes
    [HideInInspector]
    public bool buscaZonesMenudes = false;

    //Per a indicar si es un trigger de entrada o eixida
    public enum VE_TipusTrigger : short { Entrada = 0, Eixida = 1}
    public VE_TipusTrigger tipusTrigger = VE_TipusTrigger.Entrada;

    //public int numTrigger;
    void OnTriggerEnter(Collider other)
    {
        if (tipusTrigger == VE_TipusTrigger.Entrada)
        {
            if (other.tag == "Player" && dtJugador != null)
            {
                //Avisar de que el jugador entra en la zona
                dtJugador();
                
            }
        }
        else if(buscaZonesMenudes)
        {
            VoreExplorar_Zona zona = other.GetComponent<VoreExplorar_Zona>();
            if (zona != null && dtZonaMenuda != null)
                dtZonaMenuda(zona);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (tipusTrigger == VE_TipusTrigger.Eixida)
        {
            if (other.tag == "Player" && dtJugador != null)
            {
                //Avisar de que el jugador entra en la zona
                dtJugador();

            }
        }
    }
}
