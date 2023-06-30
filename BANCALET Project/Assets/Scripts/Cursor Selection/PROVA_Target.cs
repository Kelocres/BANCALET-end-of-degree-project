using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REF: https://www.youtube.com/watch?v=fw7h3UBgNW4

public class PROVA_Target : MonoBehaviour
{
    //Lo més segur es que es tinga que reemplaçar per SpriteRenderer
    private Renderer renderer;
    //private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        //renderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnMouseEnter()
    public void SelectObject()
    {
        Debug.Log("PROVA_Target OnMouseEnter()");
        renderer.material.color = Color.red;
    }

    //private void OnMouseExit()
    public void UnselectObject()
    {

        renderer.material.color = Color.white;
    }
}
