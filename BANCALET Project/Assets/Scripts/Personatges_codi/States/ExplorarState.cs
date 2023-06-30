using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class ExplorarState : IStateJugador
{
    public float velocitat = 5f;

    //Input del moviment
    float ObtinInputX() {return Input.GetAxis("Horizontal");}
    float ObtinInputY() {return Input.GetAxis("Vertical");}

    //Input per a xarrar amb NPCs
    //bool ObtinInputXarrar() {return Input.GetKeyDown(KeyCode.K);}
    //bool ObtinInputXarrar() { return Input.GetKeyDown(KeyCode.J); }
    //bool ObtinInputAccioContextual() { return Input.GetKeyDown(KeyCode.J); }

    //Inputs del ratolí
    bool ObtinInputAccioSlot() { return Input.GetMouseButtonDown(0); }
    bool ObtinInputAccioContextual() { return Input.GetMouseButtonDown(1); }

    //Per a recollir items
    public Explorar_Recollir ctrRecollir;

    public CharacterController controlador;
    //public Animator animacion;


    public void StateUpdate(PJ_StateManager cj)
    {
        //if(controlador == null)
        //    controlador = cj.GetComponent<CharacterController>();

        if (cj.accioEnMarxa) 
        {
            cj.ContarTempsAccio();
            return;
        }

        //Debug.Log("Soc Explorar");
        GestionarInputMoure(cj);

        //Primer es comprova si es pot conversar
        //si no, es comproven les accions contextuals
        if (ObtinInputAccioContextual())
        {
            //GestionarInputXarrar(cj);
            GameObject selectedObject = CursorSelector.GetSelectedObject();
            if (selectedObject != null)
            {
                cj.RotateWhenAction(selectedObject);
                //Comprobar si està en el radi de distancia

                //Buscar si alguna de les Explorar_AccioContextual funciona amb
                //eixe element
                cj.GestionarAccioContextual(selectedObject);
            }
            //Si no hi ha objecte seleccionat, es comprovarà si alguna acció que no
            //requerixca un element extern es pot activar
            else
                cj.GestionarAccioContextual();
        }

        //Comprovació d'acció de slot disponible
        if(ObtinInputAccioSlot())
        {
            GameObject selectedObject = CursorSelector.GetSelectedObject();
            if(selectedObject!= null)
            {
                cj.RotateWhenAction(selectedObject);
                //Comprobar si està en el radi de distancia

                //Activar Slot seleccionat en la barra d'items
                //cj.itemsBar.ActionSlot(selectedObject, cj.stamina, cj.feed);
                cj.itemsBar.ActionSlot(selectedObject, cj);
            }
        }
    }
    
    IStateJugador CanviaState(PJ_StateManager cj)
    {
        return cj.xarrarState;
    }

    void GestionarInputXarrar(PJ_StateManager cj)
    {
        Debug.Log("Input Xarrar pulsat");
        //Buscar un objecte amb MainTriggersDialog que puga ser activat
        MainTriggersDialog [] listaTriggers = GameObject.FindObjectsOfType<MainTriggersDialog>();
        bool triggerDetectat = false;
        foreach(MainTriggersDialog tri in listaTriggers)
        {
            if (tri.ActivarDialeg())
            {
                triggerDetectat = true;
                break;
            }
        }

        //Sols si s'ha trobat un trigger es canvia a XarrarState
        if (triggerDetectat)
            cj.CanviarState_Xarrar();
        else
            cj.GestionarAccioContextual();
    }

    void GestionarInputMoure(PJ_StateManager cj)
    {
        cj.inputMoure = new Vector3(ObtinInputX(), 0, ObtinInputY());
        //Usar input de velocidad para que el personaje se oriente 
        //según las coordenadas de la cámara
        cj.SnapAlignCharacterWithCamera();

        //Per a animar Idle o Caminar
        if (cj.inputMoure.x == 0 && cj.inputMoure.z == 0)
        {
            //Debug.Log("ExplorarState GestionarInputMoure() isCaminant == false");
            cj.animacion.SetBool("isCaminant", false);
        }
        else
        {
            //Debug.Log("ExplorarState GestionarInputMoure() isCaminant == true");
            cj.animacion.SetBool("isCaminant", true);
        }

        if(cj.inputMoure.x != 0 && cj.inputMoure.z != 0)
            cj.inputMoure = new Vector3(0,0,Mathf.Abs(cj.inputMoure.x * cj.inputMoure.z));
        else if (cj.inputMoure.x == 0)
            cj.inputMoure = new Vector3(0,0,Mathf.Abs(cj.inputMoure.z));
        else if (cj.inputMoure.z == 0)
            cj.inputMoure = new Vector3(0,0,Mathf.Abs(cj.inputMoure.x));

        cj.inputMoure.Normalize();

        //cambiar velocidad a "ir adelante"
        Vector3 velocidadActual = cj.transform.TransformDirection(cj.inputMoure);
        velocidadActual = new Vector3(velocidadActual.x * velocitat, 0f, velocidadActual.z * velocitat);

        //Adició del moviment vertical
        //velocidadActual.y += Physics.gravity.y; //Cau molt ràpid
        velocidadActual.y += Physics.gravity.y * 0.7f;


        velocidadActual = velocidadActual * Time.smoothDeltaTime * 4;

        controlador.Move(velocidadActual);

        /*if (controlador.isGrounded)
            Debug.Log("DETECTIVIE ESTÁ GROUNDED");
        else
            Debug.Log("NO GROUNDED");*/

        //Dubte:
        //El codi del moviment deuria estar ací o en DetectiuStateManager, 
        //i ací simplement s'indicaria que el métode es pot executar mentre
        //s'està en ExplorarState?
        //
        //Possibilitat: definir ací el valor de inputMoure i la velocitat de
        //moviment, i passar els valors a DetectiuStateManager
    }

    /*public void IniciarTempsAccio()
    {
        //Començar contador
        tempsAccio = 0f;
        accioEnMarxa = true;
        
    }

    public void ContarTempsAccio()
    {
        tempsAccio += Time.deltaTime;
        Debug.Log("ExplorarState ContarTempsAccio() tempsAccio = " + tempsAccio);
        if(tempsAccio >= TOP_TEMPS_ACCIO)
        {
            Debug.Log("ExplorarState ContarTempsAccio() Fi accio");
            accioEnMarxa = false;
        }
    }*/

    
}
