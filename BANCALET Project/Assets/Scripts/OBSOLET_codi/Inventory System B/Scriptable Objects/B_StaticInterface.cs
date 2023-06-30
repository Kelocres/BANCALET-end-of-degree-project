using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//REF: https://www.youtube.com/watch?v=0NG_dXsPXg0      // Tutorial 6
public class B_StaticInterface : B_UserInteface
{

    public GameObject[] slots;
    // Start is called before the first frame update
    

    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, B_InventorySlot>();
        for(int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = slots[i];

            //Afegir events al slot
            //Event quan el ratol� est� sobre el slot
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });

            //Event quan el ratol� ix del slot
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });

            //Event quan el ratol� comen�a a arrastrar
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });

            //Event quan el ratol� acaba d'arrastrar
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });

            //Event mentre el ratol� arrastra
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            slotsOnInterface.Add(obj, inventory.Container.Items[i]);
        }
    }
}
