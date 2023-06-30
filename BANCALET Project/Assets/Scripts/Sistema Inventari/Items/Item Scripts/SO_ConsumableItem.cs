using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nou consumible", menuName = "Sistema Inventari/Items/Consumible")]
public class SO_ConsumableItem : SO_ItemObject
{
    public int costVenta;
    public int quanti_Alimentar;

    //IMPORTANT!! PER A QUE ES CONSUMA UNA QUANTITAT DEL SLOT, HI HA QUE
    //MARCAR EL NÚMERO EN EL SO_ItemObject.actionConsumption

    private void Awake()
    {
        //tipusItem = TipusItem.Consumible;
        type = ItemType.Consumable;
        actionConsumption = 1;
        stackable = true;
    }

    //public override bool CheckObjective(GameObject obj, StaminaSystem stamina, FeedingSystem feed)
    public override bool CheckObjective(GameObject obj, PJ_StateManager pj)
    {
        Debug.Log("SO_ConsumableItem CheckObjective()");

        if(quanti_Alimentar <= 0)
        {
            Debug.Log("SO_ConsumableItem CheckObjective() quanti_Alimantar <= 0!!!");
            return false;
        }

        if (obj!= null && obj.GetComponent<PJ_StateManager>() && pj.feed != null)
            return pj.feed.CheckFeeding();

        Debug.Log("SO_ConsumableItem CheckObjective() retorna false");
        return false;
    }

    //public override bool ActivateItem(GameObject obj, StaminaSystem stamina, FeedingSystem feed)
    public override bool ActivateItem(GameObject obj, PJ_StateManager pj)
    {
        Debug.Log("SO_ConsumableItem ActivateItem()");
        if (pj.feed != null)
        {
            Debug.Log("SO_ConsumableItem ActivateItem() cridar a fee.Feed()");
            //Cridar al slot per a consumir
            //Enviar el valor al FeedingSystem
            pj.feed.Feed(quanti_Alimentar);
            return true;
        }

        return false;
    }
}
