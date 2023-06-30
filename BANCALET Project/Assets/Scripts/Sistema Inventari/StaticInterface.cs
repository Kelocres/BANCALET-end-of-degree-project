using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//REF: https://www.youtube.com/watch?v=0NG_dXsPXg0      // Tutorial 6
public class StaticInterface : UserInteface
{

    public GameObject[] slots;
    // Start is called before the first frame update
    
    
    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for(int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = slots[i];

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

    public int SelectSlot(int posSlot)
    {
        //OnClick(slots[posSlot]);
        if (posSlot == 0) OnClick(slots[9]);
        else OnClick(slots[posSlot - 1]);


        if (selectedGO != null && selectedSlot != null)
            return posSlot;
        else
            return -1;

    }
}
