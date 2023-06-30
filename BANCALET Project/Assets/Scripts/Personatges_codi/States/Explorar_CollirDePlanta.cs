using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorar_CollirDePlanta : Explorar_AccioContextual
{
    // Mentre no estiga programada la selecció individual amb el cursor, 
    // es guardaran les plantes que entren en el Trigger
    private List<PlantaScript> plantesCollibles;

    void Start()
    {
        plantesCollibles = new List<PlantaScript>();
    }

    public override bool ComprovarAccio(GameObject obj)
    {
        PlantaScript planta = obj.GetComponent<PlantaScript>();

        return planta != null && planta.collitaActual > 0;
    }

    public override void Activar(PJ_StateManager pj)
    {
        //base.Activar(pj);
        //Collir(pj);
    }

    public override void Activar(PJ_StateManager pj, GameObject obj)
    {
        PlantaScript planta = obj.GetComponent<PlantaScript>();
        if (planta != null)
            Collir(pj, planta);
    }

    private void Collir(PJ_StateManager pj, PlantaScript planta)
    {
        if (pj.inventari == null)
        {
            Debug.Log("Explorar_CollirDePlanta Collir() NO HI HA INVENTARI!!");
            return;
        }

        SO_ItemObject possibleCollita = planta.ComprovarTipusCollita();
        if (possibleCollita != null)
        {
            //Obtindre collita de PlantaScript (si en té)
            InventorySlot slotCollita = planta.Collir();
            bool recollit = false;

            if (pj.itemsBar != null && pj.itemsBar.itemsbar.inventory.AddItem(slotCollita.item, slotCollita.amount))
                recollit = true;
            //Si no ha sigut possible, s'itenta guardar en l'inventari
            else if (pj.inventari != null && pj.inventari.AddItem(slotCollita.item, slotCollita.amount))
                recollit = true;

            //Si s'ha pogut guardar, es borra de la llista de recollibles
            if (recollit && plantesCollibles.Contains(planta))
                plantesCollibles.Remove(planta);

            //Guardar collita en l'inventari
            //Si estava en la llista de collibles, es lleva d'ella
            /*if (pj.inventari.AddItem(slotCollita.item, slotCollita.amount))
            {
                if (plantesCollibles.Contains(planta))
                    plantesCollibles.Remove(planta);
            }*/
        }
    }

    /*private void Collir(PJ_StateManager pj)
    {
        if(pj.inventari == null)
        {
            Debug.Log("Explorar_CollirDePlanta Collir() NO HI HA INVENTARI!!");
            return;
        }

        if (plantesCollibles.Count <= 0)
        {
            Debug.Log("Explorar_CollirDePlanta Collir() NO HI HAN PLANTES PER COLLIR!!");
            return;
        }

        //En la versió actual de l'acció, si una planta és arrancada, pot donar problemes
        // però no es necessari comprovar-ho, ja que la versió definitiva no utilitzarà 
        // açò
        foreach(PlantaScript planta in plantesCollibles)
        {
            // Comprobar si este PlantaScript fa referencia a una planta destruida o arrancada
            if(planta == null)
            {
                plantesCollibles.Remove(planta);
                continue;
            }
            SO_ItemObject possibleCollita = planta.ComprovarTipusCollita();
            if(possibleCollita != null)
            {
                //Obtindre collita de PlantaScript (si en té)
                InventorySlot slotCollita = planta.Collir();
                //Guardar collita en l'inventari
                pj.inventari.AddItem(slotCollita.item, slotCollita.amount);
            }
        }
    }*/


    protected override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);
        PlantaScript novaPlanta = other.GetComponent<PlantaScript>();
        if (novaPlanta != null && novaPlanta.collitaActual > 0)
        {
            plantesCollibles.Add(novaPlanta);
            //Debug.Log("Explorar_Recollir OnTriggerEnter() recollibles.Length = " + recollibles.Count);
            //base.OnTriggerEnter(other);
            AvisarActivable_True();
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        //base.OnTriggerExit(other);
        PlantaScript novaPlanta = other.GetComponent<PlantaScript>();
        if (novaPlanta != null)
        {
            plantesCollibles.Remove(novaPlanta);
            //Debug.Log("Explorar_Recollir OnTriggerEnter() recollibles.Length = " + recollibles.Count);
            if (plantesCollibles.Count == 0)
                //base.OnTriggerExit(other);
                AvisarActivable_False();
        }
    }
}
