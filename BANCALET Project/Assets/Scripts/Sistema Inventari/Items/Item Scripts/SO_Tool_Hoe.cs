using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_Tool_Hoe : SO_ToolItem
{

    //public override bool CheckObjective(GameObject obj)
    protected override bool CheckToolObjective(GameObject obj)
    {
        // Si l'objecte es TerraPlantable, es pot interatuar
        //return obj.GetComponent<TerraPlantable>();
        //return base.CheckObjective(obj);
        
        
        return false;
    }

    //public override bool ActivateItem(GameObject obj)
    protected override bool ActivateToolItem(GameObject obj)
    {
        //return base.ActivateItem(obj);
        /*if (obj == null || !obj.GetComponent<TerraPlantable>())
        {
            Debug.Log("SO_Tool_WateringCan ActivateItem() gameobject no adequat!!!");
            return false;
        }

        obj.GetComponent<TerraPlantable>().Arruixar();

        return true;*/


        return false;
    }

}
