using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REF: https://www.youtube.com/watch?v=_IqTeruf3-s&t=51s

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System B/Items/Food")]
public class B_FoodObject : B_ItemObject
{
    public int restoreHealthValue;
    public void Awake()
    {
        type = B_ItemType.Food;
    }
}
