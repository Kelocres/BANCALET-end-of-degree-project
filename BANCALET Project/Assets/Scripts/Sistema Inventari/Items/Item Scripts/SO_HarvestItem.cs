using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova collita", menuName = "Sistema Inventari/Items/Collita")]
public class SO_HarvestItem : SO_ConsumableItem
{
    // Start is called before the first frame update
    public SO_SeedItem seed;
    private void Awake()
    {
        //tipusItem = TipusItem.Consumible;
        type = ItemType.Harvest;

        actionConsumption = 1;
        stackable = true;
    }
}
