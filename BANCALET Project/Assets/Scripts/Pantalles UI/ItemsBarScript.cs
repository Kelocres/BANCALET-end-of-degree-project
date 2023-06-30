using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemsBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public StaticInterface itemsbar;
    private bool alreadySetBarSlots = false;

    private KeyCode[] keyCodes = new KeyCode[] { 
        KeyCode.Alpha0, 
        KeyCode.Alpha1, 
        KeyCode.Alpha2, 
        KeyCode.Alpha3, 
        KeyCode.Alpha4, 
        KeyCode.Alpha5, 
        KeyCode.Alpha6, 
        KeyCode.Alpha7, 
        KeyCode.Alpha8, 
        KeyCode.Alpha9 };

    private int numBar;
    private bool inventariActivat = false;

    void Start()
    {
        itemsbar = GetComponent<StaticInterface>();
        SetBarSlots();
    }

    private void SetBarSlots()
    {
        if (alreadySetBarSlots) return;

        if (itemsbar == null)
        {
            Debug.Log("ItemsBarScript SetBarSlots() itemsbar == null");
            return;
        }
        if (itemsbar.slots.Length != 10)
        {
            Debug.Log("ItemsBarScript SetBarSlots() itemsbar.slots.Length != 10");
            return;
        }

        for (int i = 1; i <= 10; i++)
        {
            //Escriure número en cada slot
            GameObject slot = itemsbar.slots[i - 1];
            slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = (i % 10).ToString("n0");
        }

        //Seleccionar el primer slot de la barra
        numBar = itemsbar.SelectSlot(1);

        alreadySetBarSlots = true;

        //Programar delegate per a la comprovació d'Events
        itemsbar.delCheck += CheckEvent;
    }

    public void Update()
    {
        // Pulsar tecles numériques
        for (int i = 0; i < keyCodes.Length; ++i)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                //Debug.Log(i * 3);
                numBar = itemsbar.SelectSlot(i);
            }
        }

        // Roda del ratolí
        if(numBar >= 0 && Input.mouseScrollDelta.y != 0)
        {
            int tempNumBar = numBar;
            if (Input.mouseScrollDelta.y > 0) tempNumBar++;
            else
            { 
                tempNumBar--;
                if (tempNumBar == -1) tempNumBar = 9;
            }
            numBar = itemsbar.SelectSlot(tempNumBar % 10);
        }
    }

    //public void ActionSlot(GameObject obj, StaminaSystem stamina, FeedingSystem feed)
    public void ActionSlot(GameObject obj, PJ_StateManager pj)
    {
        if(itemsbar.selectedSlot == null)
        {
            Debug.Log("ItemsBarScript ActionSlot() selectedSlot == null");
            return;
        }
        if(itemsbar.selectedSlot.item.Id != -1)
        {
            SO_ItemObject item = itemsbar.selectedSlot.ItemObject;
            if (item.CheckObjective(obj, pj) && item.actionConsumption <= itemsbar.selectedSlot.amount)
                //Si l'acció es realitza amb éxit, es consumix la quantitat específica
                if (item.ActivateItem(obj, pj))
                {
                    pj.ActionAnimation(item.accioAnim);
                    //itemsbar.selectedSlot.amount -= item.actionConsumption;
                    itemsbar.selectedSlot.ConsumeItem(item.actionConsumption);
                }

        }
    }

    //El StaticInterface pregunta si pot realitzar la funció asignada segons el event
    public bool CheckEvent(string nameEvent)
    {

        if (inventariActivat != true)
        {
            //Debug.Log("ItemsBarScript CheckEvent() inventariActivat != true");
            if (nameEvent == "OnDrag" || nameEvent == "OnDragEnd" || nameEvent == "OnDragStart")
                return false;
        }

        return true;
    }

    public void SetMenuInventari(UI_InventariJugador inventari)
    {
        inventari.delIsActive += PermissosEvents;
    }

    public void PermissosEvents(bool _inventariActivat)
    {
        inventariActivat = _inventariActivat;
    }

    
}
