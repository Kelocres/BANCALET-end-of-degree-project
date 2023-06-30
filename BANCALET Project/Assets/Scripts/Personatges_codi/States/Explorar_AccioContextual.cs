using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorar_AccioContextual : MonoBehaviour
{
    // Start is called before the first frame update
    //bool accionable = false;
    public int posicioLista;
    protected string tagInteractuable;

    //Delegate per senyalar que és activable
    public delegate void SenyalaActivable(int num);
    public event SenyalaActivable senyalaActivable;

    public Sprite senyal;

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("Explorar_AccioContextual OnTriggerEnter()");
        if(other.gameObject.tag == tagInteractuable)
        {

            senyalaActivable(posicioLista);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        Debug.Log("Explorar_AccioContextual OnTriggerExit()");
        if (other.gameObject.tag == tagInteractuable)
        {
            senyalaActivable(-posicioLista);

        }
    }

    //Estas son las funciones que deben llamar los hijos si no dependen de los Ontriggers
    protected virtual void AvisarActivable_True()
    {
        Debug.Log("Explorar_AccioContextual AvisarActivable_True()");
        senyalaActivable(posicioLista);
    }

    protected virtual void AvisarActivable_False()
    {
        Debug.Log("Explorar_AccioContextual AvisarActivable_False()");
        senyalaActivable(- posicioLista);
    }

    //Per activar l'acció si no necessita un element extern
    public virtual void Activar(PJ_StateManager pj)
    {

    }

    //Per activar l'acció quan necessite un element extern
    public virtual void Activar(PJ_StateManager pj, GameObject obj)
    {

    }

    // Per a comprovar si l'objecte és vàlid per activar
    public virtual bool ComprovarAccio(GameObject obj)
    {
        return false;
    }

}
