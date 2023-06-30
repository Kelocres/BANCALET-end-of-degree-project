using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//REF: https://www.youtube.com/watch?v=0NG_dXsPXg0      // Tutorial 6
public class DynamicInterface : UserInteface
{
    //Inventari que volem mostrar
    public GameObject inventoryPrefab; //Tutorial 3

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPECE_BETWEEN_ITEMS;

    
    
    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetSlotPosition(i);

            AddAllEvents(obj);

            /*
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
            */

            slotsOnInterface.Add(obj, inventory.Container.Items[i]);
        }
    }

    private Vector3 GetSlotPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPECE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)), 0f);
    }
}
