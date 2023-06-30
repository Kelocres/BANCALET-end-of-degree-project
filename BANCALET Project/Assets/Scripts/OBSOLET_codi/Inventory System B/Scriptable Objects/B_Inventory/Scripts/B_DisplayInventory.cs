using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//REF: https://www.youtube.com/watch?v=_IqTeruf3-s&t=51s // Tutorial 1
//REF: https://www.youtube.com/watch?v=LcizwQ7ogGA&t=46s // Tutorial 3

public class B_DisplayInventory : MonoBehaviour
{/*  //En Tutorial 6 es fa innecessari, i en Tutorial 7 es borra
    //Inventari que volem mostrar
    public GameObject inventoryPrefab; //Tutorial 3
    public B_InventoryObject inventory;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPECE_BETWEEN_ITEMS;

    //Dictionary<B_InventorySlot, GameObject> itemsDisplayed = new Dictionary<B_InventorySlot, GameObject>();

    //Tutorial 5, es canvia el ordre per a, quan es fa click en el gameObject, ens proporciona la seua data
    Dictionary<GameObject, B_InventorySlot> itemsDisplayed = new Dictionary<GameObject, B_InventorySlot>();

    //Tutorial 5, referencia de l'objecte que estem arrastrant
    public B_MouseItem mouseItem = new B_MouseItem();
    // Start is called before the first frame update
    void Start()
    {
        //CreateDisplay(); // Tutorial1
        CreateSlots(); //Tutorial 5
    }

    // Update is called once per frame
    void Update()
    {
        //Seria millor cridar esta funció amb un Event, quan el jugador rebra un objecte
        //UpdateDisplay();  //Tutorial1
        UpdateSlots(); // Tutorial 5

    }

    public void CreateDisplay()
    {/*
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            B_InventorySlot slot = inventory.Container.Items[i];  // Tutorial 3

            //Crear slot y fixar la seua posició i rotació en el mon, i el seu pare
            //var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform); //Tutorial 1
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform); //Tutorial 3

            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;

            //Colocar slot en la posició local desitjada
            obj.GetComponent<RectTransform>().localPosition = GetSlotPosition(i);

            //Mostrar la quantitat de item en el text
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            /*TextMeshProUGUI slotText = obj.GetComponentInChildren<TextMeshProUGUI>();
            if(slotText==null)
            {
                Debug.Log("B_DisplayInventory CreateDisplay() slotText del obj["+i+"] no trobat, es para la mostra");
                return;
            }
            string slotAmount = inventory.Container[i].amount.ToString("n0");
            if (slotAmount == null || slotAmount == "")
            {
                Debug.Log("B_DisplayInventory CreateDisplay() slotAmount del obj[" + i + "] no trobat, es para la mostra");
                return;
            }
            slotText.text = slotAmount;

            //Afegir el item creat al diccionari
            itemsDisplayed.Add(slot, obj);
        }//*
    }

    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, B_InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetSlotPosition(i);

            //Afegir events al slot
            //Event quan el ratolí està sobre el slot
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });

            //Event quan el ratolí ix del slot
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });

            //Event quan el ratolí comença a arrastrar
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });

            //Event quan el ratolí acaba d'arrastrar
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });

            //Event mentre el ratolí arrastra
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }

    public Vector3 GetSlotPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPECE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)), 0f);
    }

    public void UpdateDisplay()
    {
        /*for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            B_InventorySlot slot = inventory.Container.Items[i];  // Tutorial 3

            //Si ja hi ha un slot d'eixe item, es suma a la quantitat
            //if(itemsDisplayed.ContainsKey(inventory.Container.Items[i]))
            if(itemsDisplayed.ContainsKey(slot))
            {
                //itemsDisplayed[inventory.Container.Items[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container.Items[i].amount.ToString("n0");
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container.Items[i].amount.ToString("n0");
            }
            // Si no, es crea el slot del item
            else
            {
                //Crear slot y fixar la seua posició i rotació en el mon, i el seu pare
                //var obj = Instantiate(inventory.Container.Items[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
                //Colocar slot en la posició local desitjada
                obj.GetComponent<RectTransform>().localPosition = GetSlotPosition(i);

                //Mostrar la quantitat de item en el text
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                //Afegir el item creat al diccionari
                itemsDisplayed.Add(slot, obj);
            }
        }//*
    }

    public void UpdateSlots() //Tutorial 5
    {
        foreach (KeyValuePair<GameObject, B_InventorySlot> _slot in itemsDisplayed)
        {
            // Si el slot amb el que estem trevallant té un item
            if(_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;

                //Canviar el color
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);

                //Mostrar la quantitat (si es 1, no es mostra texte)
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }

            // Si el slot, pel contrari, està buid
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;

                //Canviar el color
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);

                //Mostrar la quantitat (si es 1, no es mostra texte)
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    //Adding events on the buttons
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();

        //Crear un nou Trigger per afegir-lo al EventTrigger
        var eventTrigger = new EventTrigger.Entry();

        //Configurar el seu tipus
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {
        //Quan tenim un slot baix el ratolí, es guardarà
        mouseItem.hoverObj = obj;

        if (itemsDisplayed.ContainsKey(obj))
            mouseItem.hoverItem = itemsDisplayed[obj];
    }

    public void OnExit(GameObject obj)
    {
        //Olvidar-se del slot que estava baix el ratolí
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
    }

    //Quan el slot està arrastrant-se, necessitem una copia del item que estem
    //moguent per a mostrar-lo visualment (les seues funcions no son necessaries)
    public void OnDragStart(GameObject obj)
    {
        //Creem un GameObject buid, que serà inicialitzat automàticament en l'escena
        var mouseObject = new GameObject();

        var rt = mouseObject.AddComponent<RectTransform>();

        //Li donem el mateix tamany que té el slot (l'objecte representat)
        rt.sizeDelta = new Vector2(50, 50);

        //Afegir-li un pare
        mouseObject.transform.SetParent(transform.parent);

        //Si el slot té un item, la representació visual també deurà tindre-ho
        if(itemsDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;

            //El Raycast de la image deu ser false per a evitar que el seu RectTransform
            //entorpisca la operació d'arrastre
            img.raycastTarget = false;
        }

        mouseItem.obj = mouseObject;
        mouseItem.item = itemsDisplayed[obj];

    }

    public void OnDragEnd(GameObject obj)
    {
        //Tenim un slot baix el ratolí
        if (mouseItem.hoverObj)
        {
            //Intercanviar posició del slot arrastrat amb el de baix del ratolí
            inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
        }
        else //No hi ha slot, es elimina el item de l'inventari
        {
            inventory.RemoveItem(itemsDisplayed[obj].item);
        }

        //En els dos casos es necessari borrar l'objecte
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }

    public void OnDrag(GameObject obj)
    {
        //Si hi ha mouseItem, posicionar baix el ratolí
        if (mouseItem.obj != null)
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    */

}

//Tutorial 5, per a obtindre sempre la referència de l'objecte que estem arrastrant
// En Tutorial 6 es canvia a B_UserInteface

public class B_MouseItem
{
    public GameObject obj;
    public B_InventorySlot item;
    public B_InventorySlot hoverItem;
    public GameObject hoverObj;
}
