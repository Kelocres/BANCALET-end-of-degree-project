using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROVA_SlotAction : MonoBehaviour
{
    public string[] tags;
    public List<GameObject> interactuables;
    ItemsBarScript itemsBar;
    // Start is called before the first frame update
    void Start()
    {
        interactuables = new List<GameObject>();
        itemsBar = FindObjectOfType<ItemsBarScript>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        foreach(string tag in tags)
            if(other.gameObject.tag == tag)
            {
                interactuables.Add(other.gameObject);
                return;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (string tag in tags)
            if (other.gameObject.tag == tag)
            {
                interactuables.Remove(other.gameObject);
                return;
            }
    }

    public void ActivarSlot()
    {
        Debug.Log("PROVA_SlotAction ActivarSlot()");
        if(itemsBar != null)
        {
            //foreach (GameObject obj in interactuables)
            //    itemsBar.ActionSlot(obj);
        }
    }
}
