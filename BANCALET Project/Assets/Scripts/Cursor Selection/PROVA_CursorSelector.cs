using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROVA_CursorSelector : MonoBehaviour
{

    //Objecte seleccionat
    PROVA_Target target;
    private bool somethingSelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
        somethingSelected = false;

        foreach (RaycastHit hit in hits)
        {
            PROVA_Target newTarget = hit.collider.GetComponent<PROVA_Target>();
            if (newTarget != null)
            {
                if (target == null || target != newTarget)
                {
                    target = newTarget;
                    target.SelectObject();
                    //break;
                }
                somethingSelected = true;
                break;
            }
        }
        if(!somethingSelected)
        {
            target.UnselectObject();
            target = null;
        }
    }
}
