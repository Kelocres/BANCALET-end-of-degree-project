using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REF: https://www.youtube.com/watch?v=_IqTeruf3-s&t=51s

[CreateAssetMenu(fileName = "Nou item per defecte", menuName = "Sistema Inventari/Items/Defecte")]
public class SO_DefaultItem : SO_ItemObject
{
    //Cada vegada que es crea un nou Default Object, es definiran automàticament
    //algunes variables
    private void Awake()
    {
        //type = ItemType.Defecte;
        type = ItemType.Default;
        
    }
}
