using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RendererType
{
    r3d,
    r2d,
    none
}

public class CursorTarget : MonoBehaviour
{
    //Lo més segur es que es tinga que reemplaçar per SpriteRenderer
    public Renderer renderer3d;
    //public SpriteRenderer renderer2d;

    //public RendererType rendererType;

    // Necessitaran estar a una distancia determinada del PJ per a ser interactuables
    public bool inRadius;

    //private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        if (renderer3d == null)
        {
            renderer3d = GetComponent<Renderer>();
            if (renderer3d == null)
            {
                renderer3d = GetComponentInChildren<Renderer>();
                if (renderer3d == null)
                {
                    //renderer2d = GetComponent<SpriteRenderer>();
                    //if (renderer2d == null)
                    //{
                    //    renderer2d = GetComponentInChildren<SpriteRenderer>();
                    //    if (renderer2d == null)
                    //    {
                            Debug.Log("CursorTarget Start() Element with no renderer!!!");
                     //       rendererType = RendererType.none;
                     //   }
                    //}
                }
            }
        }

        /*if (renderer3d != null)
            rendererType = RendererType.r3d;

        else if (renderer2d != null)
            rendererType = RendererType.r2d;
        */
        //renderer = GetComponentInChildren<SpriteRenderer>();

        inRadius = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnMouseEnter()
    public void SelectObject()
    {
        // Si no estan dins del triger de CursorRadius, no es mostren com interactuables
        if (!inRadius) return;

        //Debug.Log("PROVA_Target OnMouseEnter()");
        //if(rendererType == RendererType.r3d)
            renderer3d.material.color = Color.red;
        //else if (rendererType == RendererType.r2d)
        //    renderer2d.material.color = Color.red;
    }

    //private void OnMouseExit()
    public void UnselectObject()
    {

       // if (rendererType == RendererType.r3d)
            renderer3d.material.color = Color.white;
       // else if (rendererType == RendererType.r2d)
        //    renderer2d.material.color = Color.white;
    }
}
