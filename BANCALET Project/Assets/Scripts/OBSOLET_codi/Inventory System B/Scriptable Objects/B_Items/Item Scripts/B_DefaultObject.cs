using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REF: https://www.youtube.com/watch?v=_IqTeruf3-s&t=51s

[CreateAssetMenu(fileName = "New Default Object", menuName ="Inventory System B/Items/Default")]
public class B_DefaultObject : B_ItemObject
{
    //Cada vegada que es crea un nou Default Object, es definiran automàticament
    //algunes variables
    private void Awake()
    {
        type = B_ItemType.Default;
    }
}
