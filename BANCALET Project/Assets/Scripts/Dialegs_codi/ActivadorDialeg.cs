using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorDialeg : MonoBehaviour
{
    public Dialeg dialeg;

    //Per a crear un Dialeg a partir d'un arxiu JSON
    public TextAsset textJSON;

    //Per qüestió de proba en l'editor
    public bool llegirJSON = true;
    
    void Start()
    {
        if(textJSON!=null && llegirJSON)
        {
            dialeg = JsonUtility.FromJson<Dialeg>(textJSON.text);
            Debug.Log("JSON llegit i adaptat");
        }
    }
    
    public void ObrirDialeg(FaceCameraPersonajes facecpJugador, FaceCameraPersonajes facecpPNJ)
    {
        if(dialeg!=null)
        {
            //Debug.Log("S'ha ordenat obrir el dialeg");
            FindObjectOfType<ManejaDialeg>().IniciaDialog(dialeg, new FaceCameraPersonajes[]{facecpJugador, facecpPNJ});
        }
        else
        {
            Debug.Log("ActivadorDialeg ObrirDialeg(FaceCameraPersonajes 1, FaceCameraPersonajes 2) el dialeg és null");
            FindObjectOfType<ManejaDialeg>().AcabarDialeg_Explorar();
        }
    }

    public void ObrirDialeg(FaceCameraPersonajes []personatges)
    {
        if (dialeg != null)
        {
            //Debug.Log("S'ha ordenat obrir el dialeg");
            FindObjectOfType<ManejaDialeg>().IniciaDialog(dialeg, personatges);
        }
        else
        {
            Debug.Log("ActivadorDialeg ObrirDialeg(FaceCameraPersonajes[]) el dialeg és null");
            FindObjectOfType<ManejaDialeg>().AcabarDialeg_Explorar();
        }
    }

}
