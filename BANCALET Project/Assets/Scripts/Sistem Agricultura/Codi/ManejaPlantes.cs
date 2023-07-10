using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejaPlantes : MonoBehaviour
{

    public static ManejaPlantes instance;
    private LightingManager lightingManager;

    public List<PlantaScript> plantes;

    //El gameobject que s'utilitzarà per a crear una nova planta
    public static GameObject gameObjectPlanta;
    // Es necessari esta variable no estàtica per a donar valor a la estàtica
    public GameObject _gameObjecPlanta;

    private void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        plantes = new List<PlantaScript>();
        lightingManager = FindObjectOfType<LightingManager>();
        if(lightingManager!=null)
        {
            //lightingManager.nouDia += NouDia;
            //lightingManager.eventHora += EventHora;
            lightingManager.dormir += CreixerDeNit;
        }

        gameObjectPlanta = _gameObjecPlanta;
    }

    //Per arreplegar el gameObject
    public static GameObject GetPlanta()
    {
        return gameObjectPlanta;
    }

    // Update is called once per frame
    public void NovaPlanta(PlantaScript novaPlanta)
    {
        novaPlanta.horaPlantacio = (int)lightingManager.GetTimeOfDay();
        plantes.Add(novaPlanta);


    }

    public void EliminarPlanta(PlantaScript plantaPerEliminar)
    {
        plantes.Remove(plantaPerEliminar);
    }

    /*
    public void NouDia()
    {
        if(plantes.Count>0)
        {
            foreach (PlantaScript planta in plantes)
                planta.NouDia();
        }
    }*/


    /*public void EventHora(int hora)
    {
        //Debug.Log("ManejaPlantes EventHora() Hora:" + hora);
        foreach(PlantaScript planta in plantes)
        {
            if (planta.horaPlantacio == hora)
                planta.NouDia();
        }
    }*/

    public void CreixerDeNit(int horaInici, int horaFinal)
    {
        Debug.Log("ManejaPlantes CreixerDeNit(horaInici = " + horaInici + "; horaFinal = " + horaFinal);
        if (horaInici > horaFinal) horaFinal += 24;

        /*for (int i = horaInici; i <= horaFinal; i++)
        {
            Debug.Log("ManejaPlantes CreixerDeNit() cridar a EventHora(" + i % 24 + ")");
            EventHora(i % 24);
        }*/
        foreach (PlantaScript planta in plantes)
        {
            planta.NouDia();
        }

    }


}
