using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSelector : MonoBehaviour
{
    public static CursorSelector instance;
    //Objecte seleccionat
    public static CursorTarget target;
    private bool somethingSelected;

    //Quan es bloquetja per estar sobre un element d'interfície (slots d'ítems, per exemple)
    public static bool blockedByUI = false;
    //Quan es bloquetja perque està obert un menú
    public static bool blockedByMenu = false;
    //Per a mostrar en l'Inspector el valor de bloquedByUI
    public bool isBlockedByUI = false;

    private ItemsBarScript itemsBar;

    public bool onlyInExplorarState = false;
    public PJ_StateManager pj;


    //Inputs del ratolí
    /*bool GetInputLeft() { return Input.GetMouseButtonDown(0); }
    bool GetInputRight() { return Input.GetMouseButtonDown(1); }*/

    // Start is called before the first frame update
    void Awake()
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

        itemsBar = FindObjectOfType<ItemsBarScript>();
        if (itemsBar == null) Debug.Log("CursorSelector Awake() no ItemsBarScript found!!");

        pj = FindObjectOfType<PJ_StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        somethingSelected = false;

        isBlockedByUI = blockedByUI;

        if (!blockedByUI && !blockedByMenu)
        {
            if (pj != null)
            {
                if ( pj.esExplorarState())
                    SearchForTargets();
            }
            //else
            //    SearchForTargets();
            
        }
        if (!somethingSelected && target != null)
        {
            target.UnselectObject();
            target = null;
        }
    }

    private void SearchForTargets()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);

        foreach (RaycastHit hit in hits)
        {
            CursorTarget newTarget = hit.collider.GetComponent<CursorTarget>();
            if (newTarget != null && newTarget.inRadius)
            {
                /*if (target == null || target != newTarget)
                {
                    
                    target = newTarget;
                    target.SelectObject();
                    //break;
                }*/
                if (target == null)
                {

                    target = newTarget;
                    target.SelectObject();
                    //break;
                }
                else if(target != newTarget)
                {
                    //Primer es deselecciona el anterior target
                    target.UnselectObject();
                    target = newTarget;
                    target.SelectObject();
                }
                somethingSelected = true;
                break;
            }
            /*else
            {
                target.UnselectObject();
                target = null;
            }*/
        }
    }

    public static void BlockByUI()
    {
        Debug.Log("CursorSelector BlockByUI()");
        blockedByUI = true;
    }

    public static void UnblockByUI()
    {
        Debug.Log("CursorSelector UnblockByUI()");
        blockedByUI = false;
    }

    public static void BlockByMenu()
    {
        Debug.Log("CursorSelector BlockByUI()");
        blockedByMenu = true;
    }

    public static void UnblockByMenu()
    {
        Debug.Log("CursorSelector UnblockByUI()");
        blockedByMenu = false;
    }

    public static GameObject GetSelectedObject()
    {
        if(target != null && target.inRadius)
            return target.gameObject;

        return null;
    }
}
