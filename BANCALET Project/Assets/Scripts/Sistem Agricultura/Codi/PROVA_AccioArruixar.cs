using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PROVA_AccioArruixar : MonoBehaviour
{
    //En la versió futura, aquesta serà una acció activable per SO_ItemFerramenta (Arruixadora)
    public Button boto;
    private bool mostrarBoto;

    private TerraPlantable plantable;


    void Start()
    {
        Debug.Log("PROVA_AccioArruixar Start()");
        if (boto != null)
        {
            boto.onClick.AddListener(ArruixarPlanta);
            boto.enabled = false;
        }
        else
            Debug.Log("PROVA_AccioArruixar Start() Botó sense valor!!");

        //El component deu de tindre el gameObjectPlanta, o no funcionarà
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TerraPlantable>())
        {
            Debug.Log("PROVA_AccioArruixar OnTriggerEnter() TerraPlantable entra al trigger!!");
            mostrarBoto = true;
            plantable = other.GetComponent<TerraPlantable>();

            if (boto != null)
            {
                boto.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<TerraPlantable>())
        {
            Debug.Log("PROVA_AccioArruixar OnTriggerExit() TerraPlantable ix del trigger!!");
            mostrarBoto = false;
            plantable = null;

            if (boto != null)
            {
                boto.enabled = false;
            }
        }
    }

    // Update is called once per frame
    public void ArruixarPlanta()
    {
        if(plantable==null)
        {
            Debug.Log("PROVA_AccioArruixar ArruixarPlanta() NO HI HA terraPlantable!!!");
            return;
        }

        plantable.Arruixar();
    }
}
