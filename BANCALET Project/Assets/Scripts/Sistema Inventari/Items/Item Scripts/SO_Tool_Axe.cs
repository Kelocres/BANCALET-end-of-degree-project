using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item destral", menuName = "Sistema Inventari/Items/Ferramentes/Destral")]
public class SO_Tool_Axe : SO_ToolItem
{


    //public override bool CheckObjective(GameObject obj)
    protected override bool CheckToolObjective(GameObject obj)
    {
        // Si l'objecte es TerraPlantable, es pot interatuar
        return obj.GetComponent<PlantaScript>();
        //return base.CheckObjective(obj);
    }

    //public override bool ActivateItem(GameObject obj)
    protected override bool ActivateToolItem(GameObject obj)
    {

        //return base.ActivateItem(obj);
        if (obj == null || !obj.GetComponent<PlantaScript>())
        {
            Debug.Log("SO_Tool_Axe ActivateItem() gameobject no adequat!!!");
            return false;
        }

        obj.GetComponent<PlantaScript>().Arrancar();

        return true;
    }
}
