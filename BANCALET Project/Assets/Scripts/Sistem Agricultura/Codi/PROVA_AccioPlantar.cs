using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PROVA_AccioPlantar : MonoBehaviour
{
    public Button boto;
    private bool mostrarBoto;
    public GameObject gameObjectPlanta;

    //La variable llavor, en les versions futures, es canviarà per un slot
    //public SO_ItemLlavor llavors;
    public SO_SeedItem llavors;
    private TerraPlantable plantable;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PROVA_AccioPlantar Start()");
        if (boto != null)
        {
            boto.onClick.AddListener(PlantarLlavor);
            boto.enabled = false;
        }
        else
            Debug.Log("PROVA_AccioPlantar Start() Botó sense valor!!");

        //El component deu de tindre el gameObjectPlanta, o no funcionarà
        if (gameObjectPlanta == null)
            Debug.Log("PROVA_AccioPlantar Start() NO HI HA gameObjectPlanta!!!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TerraPlantable>())
        {
            Debug.Log("PROVA_AccioPlantar OnTriggerEnter() TerraPlantable entra al trigger!!");
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
            Debug.Log("PROVA_AccioPlantar OnTriggerExit() TerraPlantable ix del trigger!!");
            mostrarBoto = false;
            plantable = null;

            if (boto != null)
            {
                boto.enabled = false;
            }
        }
    }

    public void PlantarLlavor()
    {
        Debug.Log("PROVA_AccioPlantar PlantarLlavor()");
        if (gameObjectPlanta == null)
        {
            Debug.Log("PROVA_AccioPlantar PlantarLlavor() NO HI HA gameObjectPlanta!!!");
            return;
        }
        if (llavors == null)
        {
            Debug.Log("PROVA_AccioPlantar PlantarLlavor() NO HI HAN SO_ItemLlavor!!!");
            return;
        }
        if (llavors.planta == null)
        {
            Debug.Log("PROVA_AccioPlantar PlantarLlavor() El SO_ItemLlavor no té un SO_Planta associat!!!");
            return;
        }
        if(plantable == null)
        {
            Debug.Log("PROVA_AccioPlantar PlantarLlavor() NO HI HA terraPlantable!!!");
            return;
        }
        if (!plantable.ComprovarPosCultiu())
        {
            Debug.Log("PROVA_AccioPlantar PlantarLlavor() terraPlantable NO TÉ posCultiu!!!");
            return;
        }

        //Instanciar gameObject en el fill de TerraPlantable
        plantable.PlantarEnTerra(gameObjectPlanta, llavors);


    }

}
