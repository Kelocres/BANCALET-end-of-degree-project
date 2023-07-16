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

public abstract class UserInteface : MonoBehaviour
{
    //public B_Player player;
    
    public SO_InventoryObject inventory;

    //El slot seleccionat
    protected GameObject selectedGO;
    public InventorySlot selectedSlot;


    //Tutorial 5, es canvia el ordre per a, quan es fa click en el gameObject, ens proporciona la seua data
    //protected Dictionary<GameObject, B_InventorySlot> itemsDisplayed = new Dictionary<GameObject, B_InventorySlot>();
    protected Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    //Delegate per a comunicar el slot seleccionat (al UI_InventariJugador, la barra d'items, etc)
    public delegate void delUI(InventorySlot slot);
    public event delUI delSelectedSlot;

    //Delegate per a verificar un event
    public delegate bool delUI_Check(string nameEvent);
    public event delUI_Check delCheck;


    // Start is called before the first frame update
    void Start()
    {
        
        // Tutorial 6, per a registrar en cada slot l'interficie al qual pertany
        // (es necessari per a canviar-ho de inventari)
        for(int i=0; i<inventory.Container.Items.Length; i++)
        {
            if (inventory == null) Debug.Log("UserInterface Start() inventory == null");
            else if (inventory.Container == null) Debug.Log("UserInterface Start() inventory.Container == null");
            else if (inventory.Container.Items == null) Debug.Log("UserInterface Start() inventory.Container.Items == null");
            else if (inventory.Container.Items[i] == null) Debug.Log("UserInterface Start() inventory.Container.Items["+i+"] == null");
            else if (inventory.Container.Items[i].parent == null) Debug.Log("UserInterface Start() inventory.Container.Items[" + i + "].parent == null");
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
    protected void AddAllEvents(GameObject obj)
    {
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

        AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
    }
    
    

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
        //if (delCheck != null && !delCheck("OnEnterInterface")) return;

        //Set up the UI that's on our player's item
        //player.mouseItem.ui = obj.GetComponent<B_UserInteface>();
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInteface>();
        CursorSelector.BlockByUI();
    }

    public void OnExitInterface(GameObject obj)
    {
        //Disset up the UI
        //player.mouseItem.ui = null;
        MouseData.interfaceMouseIsOver = null;
        CursorSelector.UnblockByUI();
    }

    public void OnEnter(GameObject obj)
    {
        //Quan tenim un slot baix el ratolí, es guardarà
        /*player.mouseItem.hoverObj = obj;

        if (slotsOnInterface.ContainsKey(obj))
            player.mouseItem.hoverItem = slotsOnInterface[obj];*/

        MouseData.slotHoveredOver = obj;
    }

    public void OnExit(GameObject obj)
    {
        //Olvidar-se del slot que estava baix el ratolí
        /*player.mouseItem.hoverObj = null;
        player.mouseItem.hoverItem = null;*/

        MouseData.slotHoveredOver = null;
    }

    //Quan el slot està arrastrant-se, necessitem una copia del item que estem
    //moguent per a mostrar-lo visualment (les seues funcions no son necessaries)
    public void OnDragStart(GameObject obj)
    {
        if (delCheck != null && !delCheck("OnDragStart")) return;

        /*player.mouseItem.obj = mouseObject;
        player.mouseItem.item = slotsOnInterface[obj];*/
        MouseData.tempItemBeingDragged = CreateTempItem(obj);

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
        if (delCheck != null && !delCheck("OnDragEnd")) return;

        Destroy(MouseData.tempItemBeingDragged);
        //Si s'arrastra el item fora de l'interficie, es borra en el slot
        if(MouseData.interfaceMouseIsOver == null)
        {
            slotsOnInterface[obj].RemoveItem();
            return;
        }

        if(MouseData.slotHoveredOver)
        {
            InventorySlot mouseHoverSlotData = 
                MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
        }
        
    }
    
    public void OnDrag(GameObject obj)
    {
        if (delCheck != null && !delCheck("OnDrag")) return;

        if (MouseData.tempItemBeingDragged != null)
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void OnClick(GameObject obj)
    {
        //Si ja hi havia un slot seleccionat, fer que torne a la normalitat
        if(selectedGO != null)
        {
            //Debug.Log("UserInterface OnClick() Canviar color");
            //Color normal: R:36, G:36, B:36, A:197
            selectedGO.GetComponent<Image>().color = new Color32(36, 36, 36, 194);
        }

        selectedGO = obj;
        selectedSlot = slotsOnInterface[obj];

        //Editar Image per a que siga més visible
        //Color selecció: R:204, G:204, B:204, A:197
        selectedGO.GetComponent<Image>().color = new Color32(204, 204, 204, 194);

        //Passar slot seleccionat
        if (delSelectedSlot != null)
            delSelectedSlot(selectedSlot);
    }



}

//Tutorial 5, per a obtindre sempre la referència de l'objecte que estem arrastrant
//public class B_MouseItem //Tutorial 5
public static class MouseData // Tutorial 7 min 1
{
    public static UserInteface interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;
    public static GameObject slotHoveredOver;
}


//Métodes que seran afegits a slotsOnInterface
//Els métodes d'extensió son un tipus especial de métodes estàtics, però es criden
//com si foren d'instancia
public static class ExtensionMethods //Tutorial 7 41
{
    public static void UpdateSlotDisplay( this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
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
                //_slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                _slot.Key.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }

            // Si el slot, pel contrari, està buid
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;

                //Canviar el color
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);

                //Mostrar la quantitat (si es 1, no es mostra texte)
                //_slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                _slot.Key.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
}
