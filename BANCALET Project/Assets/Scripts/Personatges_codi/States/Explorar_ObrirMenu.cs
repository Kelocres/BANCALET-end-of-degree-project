using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorar_ObrirMenu : Explorar_AccioContextual
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override bool ComprovarAccio(GameObject obj)
    {
        MenuTrigger trigger = obj.GetComponent<MenuTrigger>();
        
        return trigger != null;
    }

    public override void Activar(PJ_StateManager pj, GameObject obj)
    {
        MenuTrigger trigger = obj.GetComponent<MenuTrigger>();

        if (trigger != null) trigger.Activar();

    }

    protected override void OnTriggerEnter(Collider other)
    {
        //Buscar en Collider other el component MenuTrigger i la seua senyal
        //Mostrar la senyal amb AvisarActivable_True;
    }

    protected override void OnTriggerExit(Collider other)
    {
        //Buscar en Collider other el component MenuTrigger 
        //Llevar la senyal amb AvisarActivable_False;
    }
}
