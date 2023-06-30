using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJ_CursorRadius : MonoBehaviour
{
    // Start is called before the first frame update
    //private List<>
    private void OnTriggerEnter(Collider other)
    {
        CursorTarget target = other.GetComponent<CursorTarget>();
        if(target != null)
        {
            target.inRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CursorTarget target = other.GetComponent<CursorTarget>();
        if (target != null)
        {
            target.inRadius = false;
        }
    }
}
