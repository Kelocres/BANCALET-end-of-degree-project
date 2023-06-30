using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PROVA_AccioCollir : MonoBehaviour
{
    public Button boto;
    private bool mostrarBoto;
    public PlantaScript objectePlanta;
    private bool collitaDisponible;

    //Inventari on guardarà la collita
    //public SO_Inventari inventari;
    public SO_InventoryObject inventari;

    private void Start()
    {
        collitaDisponible = false;

        if(boto != null)
        {
            boto.onClick.AddListener(Collir);
            boto.enabled = false;
        }
        else
            Debug.Log("PROVA_AccioCollir Start() Botó sense valor!!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlantaScript>())
        {
            Debug.Log("PROVA_AccioCollir OnTriggerEnter() PlantaScript entra al trigger!!");
            objectePlanta = other.GetComponent<PlantaScript>();
            if (objectePlanta.ComprovarQuantitatCollita())
            {
                mostrarBoto = true;

                if (boto != null)
                {
                    boto.enabled = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlantaScript>())
        {
            Debug.Log("PROVA_AccioCollir OnTriggerExit() PlantaScript ix del trigger!!");
            objectePlanta = null;
            mostrarBoto = false;

            if (boto != null)
            {
                boto.enabled = false;
            }
        }
    }

    public void Collir()
    {
        Debug.Log("PROVA_AccioCollir Collir()");
        if(inventari == null)
        {
            Debug.Log("PROVA_AccioCollir Collir() NO HI HA INVENTARI!!");
            return;
        }
        //Comprovar si l'inventari té eixe objecte, o si no, un slot buit
        SO_ItemObject possibleCollita = objectePlanta.ComprovarTipusCollita();
        if (possibleCollita == null) return;

        //if(!inventari.ComprovarItemGuardable(possibleCollita))
        if(!inventari.AddItem(possibleCollita.data, 1))
        {
            //Es mostrarà al jugador un avís de que l'inventari està ple
            Debug.Log("PROVA_AccioCollir Collir() NO HI HA ESPAI EN L'INVENTARI!!");
            return;
        }
        //Obtindre collita de PlantaScript (si en té)
        InventorySlot slotCollita = objectePlanta.Collir();

        //Guardar collita en l'inventari
        //inventari.AfegirItem(slotCollita.item, slotCollita.amount);
        inventari.AddItem(slotCollita.item, slotCollita.amount);
    }
}
