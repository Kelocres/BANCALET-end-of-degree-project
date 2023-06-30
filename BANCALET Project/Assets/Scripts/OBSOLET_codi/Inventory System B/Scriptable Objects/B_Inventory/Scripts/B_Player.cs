using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REF: https://www.youtube.com/watch?v=_IqTeruf3-s&t=51s

public class B_Player : MonoBehaviour
{

    public B_InventoryObject inventory;
    public B_InventoryObject equipment;

    //Tutorial 5, referencia de l'objecte que estem arrastrant
    //En Tutorial 6 es canvia de DisplayInventory a ací, per que es manejaran 
    //varios inventaris al mateix temps
    //Deuria estar en una classe apartada (MouseManager, per exemple), però 
    //està ací de manera provisional
    //public B_MouseItem mouseItem = new B_MouseItem();

    public void OnTriggerEnter(Collider other)
    {
        //var item = other.GetComponent<B_ItemProva>();
        var item = other.GetComponent<B_GroundItem>();
        if (item)
        {
            //inventory.AddItem(item.item, 1); //Tutorial 1
            //inventory.AddItem(new B_Item(item.item), 1); // Tutorial 3

            //inventory.AddItem(new B_Item(item.item), 1);
            //Destroy(other.gameObject);

            if (inventory.AddItem(new B_Item(item.item), 1)) //Tutorial 7
            {
                Destroy(other.gameObject);
            }

        }
    }
    //Per a la base de dades (REF: https://www.youtube.com/watch?v=232EqU1k9yQ)
    private void Update()
    {
        //Debug.Log("B_Player Update()");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("B_Player Update() Botó Space per a guardar");
            inventory.Save();
            equipment.Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("B_Player Update() Botó Enter per a carregar");
            //inventory.Load();
            //equipment.Load();
            inventory.LoadTut5();
            equipment.LoadTut5();
        }
    }

    //Per a evitar que el InventoryObject guarde els objectes despres de les proves
    private void OnApplicationQuit()
    {
        //inventory.Container.Items.Clear(); //Tutorial 1
        //inventory.Container.Items = new B_InventorySlot[20];    //Tutorial 5
        inventory.Container.Clear();
        equipment.Container.Clear();
    }
}
