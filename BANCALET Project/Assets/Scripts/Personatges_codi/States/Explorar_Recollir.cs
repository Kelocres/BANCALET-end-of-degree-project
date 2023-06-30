using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorar_Recollir : Explorar_AccioContextual
{
    //private string tagInteractuable = "item_Recollir";
    //private List<ItemRecollible> recollibles;
    private List<PROVA_GroundItem> recollibles;


    private void Start()
    {
        tagInteractuable = "item_Recollir";
        //recollibles = new List<ItemObject>();
        recollibles = new List<PROVA_GroundItem>();
    }

    public override bool ComprovarAccio(GameObject obj)
    {
        return obj.GetComponent<PROVA_GroundItem>();
    }

    public override void Activar(PJ_StateManager pj)
    {
        //Recollir(pj);
    }

    public override void Activar(PJ_StateManager pj, GameObject obj)
    {
        PROVA_GroundItem item = obj.GetComponent<PROVA_GroundItem>();
        if (item != null)
            Recollir(pj, item);
    }

    public void Recollir(PJ_StateManager pj, PROVA_GroundItem item)
    {
        bool recollit = false;
           
        //Primer es tracta de guardar en la barra d'items
        if (pj.itemsBar != null && item.RecollirItem(pj.itemsBar.itemsbar.inventory))
            recollit = true;
        //Si no ha sigut possible, s'itenta guardar en l'inventari
        else if(pj.inventari != null && item.RecollirItem(pj.inventari))
            recollit = true;

        //Si s'ha pogut guardar, es borra de la llista de recollibles
        if (recollit && recollibles.Contains(item))
            recollibles.Remove(item);
        
    }


    /*public void Recollir(PJ_StateManager pj)
    {
        Debug.Log(" ControlaRecollir Recollir()");
        if(pj.itemsBar != null)
        {
            //foreach (ItemObject item in recollibles)
            foreach (PROVA_GroundItem item in recollibles)
                if (!item.RecollirItem(pj.itemsBar.itemsbar.inventory))
                    item.RecollirItem(pj.inventari);

            recollibles = new List<PROVA_GroundItem>();
            AvisarActivable_False();
        }

    }*/

    protected override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);
        PROVA_GroundItem nouRecollible = other.GetComponent<PROVA_GroundItem>();
        if(nouRecollible != null)
        {
            Debug.Log("Explorar_Recollir OnTriggerEnter() PROVA_GroundItem trobat");
            recollibles.Add(nouRecollible);
            //Debug.Log("Explorar_Recollir OnTriggerEnter() recollibles.Length = " + recollibles.Count);
            //base.OnTriggerEnter(other);
            AvisarActivable_True();
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        //base.OnTriggerExit(other);
        PROVA_GroundItem nouRecollible = other.GetComponent<PROVA_GroundItem>();
        //if (nouRecollible != null && recollibles.Contains(nouRecollible))
        if (nouRecollible != null)
        {
            recollibles.Remove(nouRecollible);
            //Debug.Log("Explorar_Recollir OnTriggerEnter() recollibles.Length = " + recollibles.Count);
            if (recollibles.Count == 0)
                //base.OnTriggerExit(other);
                AvisarActivable_False();
        }
    }


}
