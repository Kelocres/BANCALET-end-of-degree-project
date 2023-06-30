using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamaraLliure : MonoBehaviour
{
    public Transform objectiu; //Personaje jugador
    //Posició de la càmara mentre el jugador està en ExplorarState
    public Transform posCamara;
    
    private Vector3 vectorMirar;

    //Posició respecte al jugador(variable obsoleta, s'utilitzarà si no s'ha
    //trobat a posCamaraExplorar)
    //private Vector3 diferenciaPosicion = new Vector3(-32,9,0); 
    private Vector3 puntoOptimo;//Punto donde se posicionará la cámara

    public float velocitatLerp = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        vectorMirar = objectiu.transform.position - transform.position;

        //Per a la rotació
        Quaternion giroOptimo = Quaternion.LookRotation(vectorMirar);
        transform.rotation = Quaternion.Lerp(transform.rotation, giroOptimo, 0.05f);

        //Per a la posició
        /*if(posCamara==null)
            puntoOptimo = objectiu.transform.position + diferenciaPosicion;
        else */
            puntoOptimo = posCamara.position;
        transform.position = Vector3.Lerp(transform.position, puntoOptimo, velocitatLerp);
    }

   
}
