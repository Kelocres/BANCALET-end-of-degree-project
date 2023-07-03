using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    /*
    public SO_Inventari inventari;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPECE_BETWEEN_ITEMS;

    Dictionary<SlotInventari, GameObject> itemsMostrats = new Dictionary<SlotInventari, GameObject>();

    private bool alreadyCreatedInventory = false;
    private RectTransform inventoryWindow; 

    //


    public void CreateDisplay(SO_Inventari introInventari)
    {
        if (alreadyCreatedInventory) return;

        if (introInventari == null)
        {
            Debug.Log("DisplayInventory CreateInventory() NO HI HA INVENTARI");
            return;
        }

        inventari = introInventari;

        //Si es troba el inventoryWindow, els valors de les constants es adaptaran
        //SearchInventoryWindow();

        GameObject slotPrefab = DisplayInventoryManager.slotPrefab;
        if (slotPrefab == null)
        {
            Debug.Log("DisplayInventory CreateInventory() NO HI HA slotPrefab IN DisplayInventoryManager !!!");
            return;
        }

        for (int i = 0; i < inventari.Contenedor.Length; i++)
        {
            //Instanciar prefab
            var obj = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transform);

            //Asignar valor al slot (si no està buit, li afegim la icona i quantitat; si ho està, el buidem)
            SlotObject slotObject = obj.GetComponent<SlotObject>();
            slotObject.AfegirSlot(inventari.Contenedor[i]);

            //Colocar slot en la posició local desitjada
            obj.GetComponent<RectTransform>().localPosition = GetSlotPosition(i);

            //Afegir el item creat al diccionari
            itemsMostrats.Add(inventari.Contenedor[i], obj);

        }


        alreadyCreatedInventory = true;
    }

    public Vector3 GetSlotPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS)),
                            Y_START + (-Y_SPECE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)),
                            0f);
    }

    private void SearchInventoryWindow()
    {
        /*
        //Arreplegar totes les Images en els fills
        Image[] totalImages = GetComponentsInChildren<Image>();

        //Trobar la image adequada segons el tag del gameobject
        foreach(Image cur_image in totalImages)
        {
            if(cur_image.gameObject.tag == "inventoryWindow")
            {
                inventoryWindow = cur_image.rectTransform;
                cur_image.enabled = false;
                break;
            }
        }
        
        GetComponent<Image>().enabled = false;
        inventoryWindow = GetComponent<RectTransform>();

        if (inventoryWindow == null)
        {
            Debug.Log("DisplayInventory SearchInventoryWindow() inventoryWindow not found");
            return;
        }
           
        Debug.Log("DisplayInventory SearchInventoryWindow() inventoryWindow found, let's set the values!!");
        X_START = (int)inventoryWindow.anchorMin.x;
        Y_START = (int)inventoryWindow.anchorMin.y;

    }

    public void UpdateDisplay()
    {
        if (!alreadyCreatedInventory) return;


        for (int i = 0; i < inventari.Contenedor.Length; i++)
        {
            //ESTA PART DEL CODI POT SER TOTALMENT INNECESSÀRIA
            //Si ja hi ha un slot d'eixe item, es suma a la quantitat
            if (itemsMostrats.ContainsKey(inventari.Contenedor[i]))
            {
                itemsMostrats[inventari.Contenedor[i]].GetComponentInChildren<TextMeshProUGUI>().text =
                    inventari.Contenedor[i].quantitat.ToString("n0");
            }
            // Si no, es crea el slot del item
            else
            {
                if (inventari.Contenedor[i] == null)
                {
                    Debug.Log("UI_InventariJugador UpdateDisplay() inventari.Contenedor[" + i + "] == null");
                    return;
                }
                if (inventari.Contenedor[i].item == null)
                {
                    Debug.Log("UI_InventariJugador UpdateDisplay() inventari.Contenedor[" + i + "].item == null");
                    return;
                }
                if (inventari.Contenedor[i].item.icona == null)
                {
                    Debug.Log("UI_InventariJugador UpdateDisplay() inventari.Contenedor[" + i + "].item.icona == null");
                    return;
                }
                //Crear slot y fixar la seua posició i rotació en el mon, i el seu pare
                //var obj = Instantiate(inventari.Contenedor[i].item.icona, Vector3.zero, Quaternion.identity, transform);
                var obj = Instantiate(DisplayInventoryManager.slotPrefab, Vector3.zero, Quaternion.identity, transform);

                SlotObject slotObject = obj.GetComponent<SlotObject>();
                slotObject.AfegirSlot(inventari.Contenedor[i]);
                //Colocar slot en la posició local desitjada
                obj.GetComponent<RectTransform>().localPosition = GetSlotPosition(i);

                //Mostrar la quantitat de item en el text
                //obj.GetComponentInChildren<TextMeshProUGUI>().text = 
                //    inventari.Contenedor[i].quantitat.ToString("n0");
                //Afegir el item creat al diccionari
                itemsMostrats.Add(inventari.Contenedor[i], obj);
            }
        }


    }*/
}

[System.Serializable]
public struct TipusItem_Sprite
{
    //public TipusItem tipusItem;
    public ItemType tipusItem;
    public Sprite sprite;
}
