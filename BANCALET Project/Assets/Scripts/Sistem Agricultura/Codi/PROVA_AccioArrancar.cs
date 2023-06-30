using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PROVA_AccioArrancar : MonoBehaviour
{
    //En la versi� futura, aquesta ser� una acci� activable per SO_ItemFerramenta (Aix�)

    public Button boto;
    private bool mostrarBoto;
    public PlantaScript objectePlanta;

    void Start()
    {
        if (boto != null)
        {
            boto.onClick.AddListener(Arrancar);
            boto.enabled = false;
        }
        else
            Debug.Log("PROVA_AccioArrancar Start() Bot� sense valor!!");
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlantaScript>())
        {
            Debug.Log("PROVA_AccioArrancar OnTriggerEnter() PlantaScript entra al trigger!!");
            objectePlanta = other.GetComponent<PlantaScript>();
            
                mostrarBoto = true;

                if (boto != null)
                {
                    boto.enabled = true;
                }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlantaScript>())
        {
            Debug.Log("PROVA_AccioArrancar OnTriggerExit() PlantaScript ix del trigger!!");
            objectePlanta = null;
            mostrarBoto = false;

            if (boto != null)
            {
                boto.enabled = false;
            }
        }
    }

    public void Arrancar()
    {
        objectePlanta.Arrancar();
    }
}
