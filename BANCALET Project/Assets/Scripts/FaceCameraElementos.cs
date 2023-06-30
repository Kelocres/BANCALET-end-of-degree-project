using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCameraElementos : MonoBehaviour
{
    //Siempre se revertirá para tener todos los sprites bien
    //public bool revertir;
    Vector3 direccionCamara;

    //float lockPos = 0f;
    //La càmara a la qual s'encaren no serà sempre la Camera.main
    private Camera camaraActual;

    //Per a saber si està o no en la llista de ManejaCamares
    public bool started = false;

    private ManejaCamares mc;

    void Start()
    {
        if(!started)
        {
            mc = FindObjectOfType<ManejaCamares>();
            if(mc == null)
            {
                Debug.Log("FaceCameraElementos Start() ManejaCamares == null!!!");
                return;
            }
            mc.GetFCElementos(this);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        //En el cas de que no tinga camaraActual registrada,
        //es canvia a Camera.main
        if (camaraActual == null)
            camaraActual = Camera.main;

        transform.rotation = camaraActual.transform.rotation;
        
    }

    public void CanviarCamara(Camera novaCamara)
    {
        camaraActual = novaCamara;
        transform.rotation = camaraActual.transform.rotation;
    }

    //Es deurà cridar aquesta funció quan es destruixca el gameobject del FaceCameraElementos
    void OnDestroy()
    {
        Debug.Log("FCElementos OnDestroy()");
        if (mc != null) mc.DestroyFCElementos(this);
    }
}
