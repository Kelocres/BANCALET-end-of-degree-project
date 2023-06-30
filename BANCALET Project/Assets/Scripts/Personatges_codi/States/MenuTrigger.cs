using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuTrigger : MonoBehaviour
{
    public UnityEvent activate;
    // Start is called before the first frame update
    

    // Update is called once per frame
    public void Activar()
    {
        if (activate != null)
        {
            Debug.Log("MenuTrigger Activar() accionar events!!");
            activate.Invoke();
        }
        else
            Debug.Log("MenuTrigger Activar() UnityEvent = null!!");
    }
}
