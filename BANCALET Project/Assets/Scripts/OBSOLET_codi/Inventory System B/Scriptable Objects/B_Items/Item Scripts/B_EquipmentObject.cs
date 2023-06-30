using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REF: https://www.youtube.com/watch?v=_IqTeruf3-s&t=51s // Tutorial 1

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System B/Items/Equipment")]
public class B_EquipmentObject : B_ItemObject
{
    //public float attackBonus;
    //public float defenceBonus;

    public void Awake()
    {
        //type = B_ItemType.Chest; //Si es resetegen els inventaris, es trastoca el tipus de varios items
    }
}
