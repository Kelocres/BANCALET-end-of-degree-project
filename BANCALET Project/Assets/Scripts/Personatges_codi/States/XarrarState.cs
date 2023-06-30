using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class XarrarState : IStateJugador
{
    //Input per a passar
    bool ObtinNextLinia() {return Input.GetKeyDown(KeyCode.K);}

    public void StateUpdate(PJ_StateManager cj)
    {
        //Debug.Log("Soc Xarrar");
        //Si el jugador pulsa el botó, se li envia ordre a ManejaDialeg de passar a la següent linia
        if (ObtinNextLinia())
        {
            Debug.Log("XarrarState StateUpdate() crida a ManejaDialeg MostrarOracioActual()");
            GameObject.FindObjectOfType<ManejaDialeg>().MostrarOracioActual();
        }
    }


    
    IStateJugador CanviaState(PJ_StateManager cj)
    {
        return cj.explorarState;
    }
}
