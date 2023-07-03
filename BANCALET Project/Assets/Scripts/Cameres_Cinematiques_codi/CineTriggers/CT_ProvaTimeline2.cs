using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_ProvaTimeline2 : CineTriggerScript
{

    public Transform destiProvaTimeline2;

    public override void ActivarAlEntrar()
    {
        activat = false;
        Debug.Log("CineCollider_ProvaTimeline2 ha col·lisionat!");
        // Fer que el detectiu canvie a CinemàticaState
        PJ_StateManager jugador = FindObjectOfType<PJ_StateManager>();
        //jugador.CanviarState_Cinematica();

        // Ordenar que es moga cap al destí
        jugador.CaminarCapDesti_DespresExplorar(destiProvaTimeline2);

    }

    public override void ActivarAlEixir()
    {

    }
}
