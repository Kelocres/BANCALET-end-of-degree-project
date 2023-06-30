using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova llavor", menuName = "Sistema Inventari/Items/Llavor")]

public class SO_SeedItem : SO_ItemObject
{
    // Start is called before the first frame update
    public SO_Planta planta;
    public SO_PlantaTex plantaTex;
    private void Awake()
    {
        //type = ItemType.Defecte;
        type = ItemType.Seed;

        actionConsumption = 1;
        stackable = true;
    }

    //public override bool CheckObjective(GameObject obj, StaminaSystem stamina, FeedingSystem feed)
    public override bool CheckObjective(GameObject obj, PJ_StateManager pj)
    {
        // Si l'objecte es TerraPlantable, es pot interatuar
        return obj.GetComponent<TerraPlantable>();
        //return base.CheckObjective(obj);
    }

    //public override bool ActivateItem(GameObject obj, StaminaSystem stamina, FeedingSystem feed)
    public override bool ActivateItem(GameObject obj, PJ_StateManager pj)
    {
        //return base.ActivateItem(obj);
        if(obj == null || !obj.GetComponent<TerraPlantable>())
        {
            Debug.Log("SO_SeedItem ActivateItem() gameobject no adequat!!!");
            return false;
        }
        if(planta == null)
        {
            Debug.Log("SO_SeedItem ActivateItem() no hi ha planta!!!");
            return false;
        }

        TerraPlantable plantable = obj.GetComponent<TerraPlantable>();
        if (!plantable.ComprovarPosCultiu())
        {
            Debug.Log("SO_SeedItem ActivateItem() TerraPlantable NO TÉ posCultiu!!!");
            return false;
        }

        GameObject gameobjectPlanta = ManejaPlantes.GetPlanta();
        if(gameobjectPlanta == null)
        {
            Debug.Log("SO_SeedItem ActivateItem() gameobjectPlanta == null!!!");
            return false;
        }
        plantable.PlantarEnTerra(gameobjectPlanta, this);
        
        return true;
    }
}
