using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//REF: https://www.youtube.com/watch?v=_IqTeruf3-s&t=51s // Tutorial 1
//REF: https://www.youtube.com/watch?v=LcizwQ7ogGA&t=46s // Tutorial 3
//REF: https://www.youtube.com/watch?v=0NG_dXsPXg0      // Tutorial 6
//REF: https://www.youtube.com/watch?v=Yp5ADg0dFiQ&list=PLJWSdH2kAe_Ij7d7ZFR2NIW8QCJE74CyT&index=7 // Tutorial 7

public abstract class B_UserInteface : MonoBehaviour
{
    //public B_Player player;
    
    public B_InventoryObject inventory;




    //Tutorial 5, es canvia el ordre per a, quan es fa click en el gameObject, ens proporciona la seua data
    //protected Dictionary<GameObject, B_InventorySlot> itemsDisplayed = new Dictionary<GameObject, B_InventorySlot>();
    protected Dictionary<GameObject, B_InventorySlot> slotsOnInterface = new Dictionary<GameObject, B_InventorySlot>();


    // Start is called before the first frame update
    void Start()
    {
        
        // Tutorial 6, per a registrar en cada slot l'interficie al qual pertany
        // (es necessari per a canviar-ho de inventari)
        for(int i=0; i<inventory.Container.Items.Length; i++)
        {
            inventory.Container.Items[i].parent = this;
        }


        //CreateDisplay(); // Tutorial1
        CreateSlots(); //Tutorial 5

        // Tutorial 6 minut 40 Per a evitar que el item es borre al arrastrar-ho al borde
        // entre slots
        //Event quan el ratolí està sobre el slot
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });

        //Event quan el ratolí ix del slot
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    // Update is called once per frame
    void Update()
    {

        //UpdateSlots(); // Tutorial 5
        slotsOnInterface.UpdateSlotDisplay(); //Tutorial 7 minut 42

    }



    public abstract void CreateSlots();
    

    
    /*
    public void UpdateSlots() //Tutorial 5, en Tutorial 7 es substituix per UpdateSlotDisplay()
    {
        foreach (KeyValuePair<GameObject, B_InventorySlot> _slot in slotsOnInterface)
        {
            // Si el slot amb el que estem trevallant té un item
            if (_slot.Value.item.Id >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    //inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                    _slot.Value.ItemObject.uiDisplay;

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
    }*/

    //Adding events on the buttons
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();

        //Crear un nou Trigger per afegir-lo al EventTrigger
        var eventTrigger = new EventTrigger.Entry();

        //Configurar el seu tipus
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnterInterface(GameObject obj)
    {
        //Set up the UI that's on our player's item
        //player.mouseItem.ui = obj.GetComponent<B_UserInteface>();
        B_MouseData.interfaceMouseIsOver = obj.GetComponent<B_UserInteface>();
    }

    public void OnExitInterface(GameObject obj)
    {
        //Disset up the UI
        //player.mouseItem.ui = null;
        B_MouseData.interfaceMouseIsOver = null;
    }

    public void OnEnter(GameObject obj)
    {
        //Quan tenim un slot baix el ratolí, es guardarà
        /*player.mouseItem.hoverObj = obj;

        if (slotsOnInterface.ContainsKey(obj))
            player.mouseItem.hoverItem = slotsOnInterface[obj];*/

        B_MouseData.slotHoveredOver = obj;
    }

    public void OnExit(GameObject obj)
    {
        //Olvidar-se del slot que estava baix el ratolí
        /*player.mouseItem.hoverObj = null;
        player.mouseItem.hoverItem = null;*/

        B_MouseData.slotHoveredOver = null;
    }

    //Quan el slot està arrastrant-se, necessitem una copia del item que estem
    //moguent per a mostrar-lo visualment (les seues funcions no son necessaries)
    public void OnDragStart(GameObject obj)
    {
        
        /*player.mouseItem.obj = mouseObject;
        player.mouseItem.item = slotsOnInterface[obj];*/
        B_MouseData.tempItemBeingDragged = CreateTempItem(obj);

    }

    public GameObject CreateTempItem(GameObject obj) //Tutorial 7 minut 39
    {
        GameObject tempItem = null;
        if(slotsOnInterface[obj].item.Id >= 0)
        {
            //Creem un GameObject buid, que serà inicialitzat automàticament en l'escena
            tempItem = new GameObject();

            var rt = tempItem.AddComponent<RectTransform>();

            //Li donem el mateix tamany que té el slot (l'objecte representat)
            rt.sizeDelta = new Vector2(50, 50);

            //Afegir-li un pare
            tempItem.transform.SetParent(transform.parent);

            //Si el slot té un item, la representació visual també deurà tindre-ho

            var img = tempItem.AddComponent<Image>();
            //img.sprite = inventory.database.GetItem[slotsOnInterface[obj].item.Id].uiDisplay;
            img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;

            //El Raycast de la image deu ser false per a evitar que el seu RectTransform
            //entorpisca la operació d'arrastre
            img.raycastTarget = false;
        }

        return tempItem;
        
    }

    public void OnDragEnd(GameObject obj) //Tutorial 7
    {
        Destroy(B_MouseData.tempItemBeingDragged);
        //Si s'arrastra el item fora de l'interficie, es borra en el slot
        if(B_MouseData.interfaceMouseIsOver == null)
        {
            slotsOnInterface[obj].RemoveItem();
            return;
        }

        if(B_MouseData.slotHoveredOver)
        {
            B_InventorySlot mouseHoverSlotData = 
                B_MouseData.interfaceMouseIsOver.slotsOnInterface[B_MouseData.slotHoveredOver];
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
        }
        
    }
    /*public void OnDragEnd(GameObject obj) //Tutorial 6
    {
        //Per a comprovar si l'item arrastrat pot anar en el slot
        var itemOnMouse = player.mouseItem;
        var mouseHoverItem = itemOnMouse.hoverItem;
        var mouseHoverObj = itemOnMouse.hoverObj;
        var GetItemObject = inventory.database.GetItem;

        

        //Tutorial 6 minut 41 Per a evitar que el item es borre al arrastrar-ho al borde
        // entre slots
        if (itemOnMouse.ui != null)
        {
            //Tenim un slot baix el ratolí
            if (mouseHoverObj)
            {

                //Intercanviar posició del slot arrastrat amb el de baix del ratolí
                // No es farà l'intercanvi si els tipus dels items no s'adequen als dels slots
                // REF: https://www.youtube.com/watch?v=0NG_dXsPXg0&t=2001s // Tutorial 6 minut 36
                //if (mouseHoverItem.CanPlaceInSlot(GetItemObject[itemsDisplayed[obj].ID]))
                if (mouseHoverItem.CanPlaceInSlot(GetItemObject[itemsDisplayed[obj].ID])
                    && (mouseHoverItem.item.Id <= -1
                    || (mouseHoverItem.item.Id >= 0
                    && itemsDisplayed[obj].CanPlaceInSlot(GetItemObject[mouseHoverItem.item.Id]))))
                    inventory.MoveItem(itemsDisplayed[obj], mouseHoverItem.parent.itemsDisplayed[itemOnMouse.hoverObj]); //Tutorial 6
            }
        }
        
        else //No hi ha slot, es elimina el item de l'inventari
        {
            inventory.RemoveItem(itemsDisplayed[obj].item);
        }

        //En els dos casos es necessari borrar l'objecte
        Destroy(itemOnMouse.obj);
        player.mouseItem.item = null;
    }*/

    public void OnDrag(GameObject obj)
    {
        //Si hi ha mouseItem, posicionar baix el ratolí
        /*if (player.mouseItem.obj != null)
            player.mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
    */
        if(B_MouseData.tempItemBeingDragged != null)
            B_MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
    }



}

//Tutorial 5, per a obtindre sempre la referència de l'objecte que estem arrastrant
//public class B_MouseItem //Tutorial 5
public static class B_MouseData // Tutorial 7 min 1
{
    public static B_UserInteface interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;
    public static GameObject slotHoveredOver;
}
/*public class B_MouseItem //Tutorial 5
 * {
    public  B_UserInteface ui;
    public  GameObject obj;
    public  B_InventorySlot item;
    public  B_InventorySlot hoverItem;
    public  GameObject hoverObj;
}
 */

//Métodes que seran afegits a slotsOnInterface
//Els métodes d'extensió son un tipus especial de métodes estàtics, però es criden
//com si foren d'instancia
public static class B_ExtensionMethods //Tutorial 7 41
{
    public static void UpdateSlotDisplay( this Dictionary<GameObject, B_InventorySlot> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, B_InventorySlot> _slot in _slotsOnInterface)
        {
            // Si el slot amb el que estem trevallant té un item
            if (_slot.Value.item.Id >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    //inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                    _slot.Value.ItemObject.uiDisplay;

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
}
